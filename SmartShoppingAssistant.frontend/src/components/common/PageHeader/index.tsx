import { Box, Button, Typography } from "@mui/material"
import AddIcon from "@mui/icons-material/Add"

interface PageHeaderProps {
    title: string
    actionLabel?: string
    onAction?: () => void
}

function PageHeader({ title, actionLabel, onAction }: PageHeaderProps) {
    return (
        <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 3 }}>
            <Typography variant='h4'>{title}</Typography>
            {actionLabel && onAction && (
                <Button variant='contained' onClick={onAction} startIcon={<AddIcon />}>
                    {actionLabel}
                </Button>
            )}
        </Box>
    )
}

export default PageHeader
