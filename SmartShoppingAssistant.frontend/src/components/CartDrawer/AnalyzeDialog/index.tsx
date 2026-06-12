import { Alert, Box, Button, CircularProgress, Dialog, DialogContent, DialogTitle, Divider, LinearProgress, Stack, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { cartApi } from '../../../api/clients/CartApiClient';
import { type Analysis, type Suggestion } from '../../shared/types/Analysis';
import { useCart } from '../../../context/CartContext/cart-context';
import AutoAwesomeIcon from '@mui/icons-material/AutoAwesome';
import CheckIcon from '@mui/icons-material/Check';

export type Decision = 'accept' | 'reject' | 'undecided'

interface AnalyzeDialogProps {
    open: boolean;
    onClose: () => void;
}

const loadingMessages = [
    '🤖 Reading your cart...',
    '🔍 Checking promotions...',
    '✨ Finding the best deals...',
    '🛒 Composing suggestions...',
]

function AnalyzeDialog({ open, onClose }: AnalyzeDialogProps) {
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')
    const [analysis, setAnalysis] = useState<Analysis | null>(null)
    const [decisions, setDecisions] = useState<Record<number, Decision>>({})
    const [messageIndex, setMessageIndex] = useState(0)
    const [progress, setProgress] = useState(0)

    const { addItem } = useCart();

    useEffect(() => {
        if (!open) return
        setLoading(true)
        setProgress(0)
        setMessageIndex(0)
        setAnalysis(null)
        setError('')
        cartApi.analyze().then((data) => {
            setAnalysis(data);
        }).catch((err: any) => {
            setError(err.message);
        }).finally(() => {
            setLoading(false);
        })
    }, [open])

    useEffect(() => {
        if (!loading) return
        const progressInterval = setInterval(() => {
            setProgress((prev) => prev >= 90 ? 90 : prev + 0.4)
        }, 80)
        const messageInterval = setInterval(() => {
            setMessageIndex((prev) => (prev + 1) % loadingMessages.length)
        }, 2000)
        return () => {
            clearInterval(progressInterval)
            clearInterval(messageInterval)
        }
    }, [loading])

    async function handleApprove(suggestion: Suggestion) {
        await addItem(suggestion.productId, suggestion.quantity)
        setDecisions((currentDecisions) => ({ ...currentDecisions, [suggestion.productId]: 'accept' }))
    }

    async function handleDecline(suggestion: Suggestion) {
        setDecisions((currentDecisions) => ({ ...currentDecisions, [suggestion.productId]: 'reject' }))
    }

    return <Dialog open={open} onClose={onClose}>
        <DialogTitle sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <AutoAwesomeIcon color='primary' />
            AI Cart Analysis
        </DialogTitle>
        <DialogContent>
            {loading && <Box sx={{ py: 2 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1.5 }}>
                    <CircularProgress size={20} thickness={5} />
                    <Typography>{loadingMessages[messageIndex]}</Typography>
                </Box>
                <LinearProgress
                    variant='determinate'
                    value={progress}
                    sx={{ borderRadius: 999 }}
                />
            </Box>}
            {
                error !== "" && (<Alert severity='error' sx={{ mb: 2 }}>{error}</Alert>)
            }

            {
                analysis !== null && !loading && (
                    <Stack spacing={2}>
                        <Typography>{analysis.summary}</Typography>
                        <Divider />

                        {analysis.suggestions.length === 0 && (
                            <Typography color='text.secondary'>
                                No suggestions for this cart
                            </Typography>
                        )}

                        {analysis.suggestions.map((suggestion) => {
                            const decision = decisions[suggestion.productId] ?? 'undecided'
                            return <Box key={suggestion.productId} sx={{ border: '1px solid', borderColor: 'divider' }}>
                                <Box sx={{
                                    display: 'flex',
                                    justifyContent: 'space-between',
                                }}>
                                    <Typography variant='subtitle1'>{suggestion.name} x {suggestion.quantity}</Typography>
                                    <Typography variant='subtitle1'>{suggestion.priceLabel}</Typography>
                                </Box>
                                <Typography variant='body2' color='text.secondary' sx={{ mt: 0.5 }}>{suggestion.reason}</Typography>
                                {suggestion.savingsLabel !== null && (
                                    <Typography variant='body2' color='success' sx={{ mt: 0.5 }}>
                                        You save {suggestion.savingsLabel}
                                    </Typography>
                                )}

                                <Box sx={{ mt: 2 }}>
                                    {decision === 'undecided' ? (
                                        <Stack direction='row' sx={{ gap: 1 }}>
                                            <Button size='small' variant='contained' startIcon={<CheckIcon />} onClick={() => handleApprove(suggestion)}>
                                                Accept
                                            </Button>
                                            <Button size='small' variant='outlined' onClick={() => handleDecline(suggestion)}>
                                                Reject
                                            </Button>
                                        </Stack>
                                    ) : (
                                        <Typography variant='caption' color={decision === 'accept' ? 'success.main' : 'error.main'}>
                                            {decision === 'accept' ? 'Accepted' : 'Rejected'}
                                        </Typography>
                                    )}
                                </Box>
                            </Box>
                        })}
                    </Stack>
                )
            }
        </DialogContent>
    </Dialog>
}

export default AnalyzeDialog;