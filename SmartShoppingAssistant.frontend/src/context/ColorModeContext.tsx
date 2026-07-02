import { createContext, useContext, useMemo, useState, useEffect, type ReactNode } from 'react'
import { ThemeProvider } from '@mui/material/styles'
import CssBaseline from '@mui/material/CssBaseline'
import type { PaletteMode } from '@mui/material'
import { createAppTheme } from '../theme'

interface ColorModeContextType {
    mode: PaletteMode
    toggleColorMode: () => void
}

const ColorModeContext = createContext<ColorModeContextType | null>(null)

const STORAGE_KEY = 'ssa_color_mode'

export function ColorModeProvider({ children }: { children: ReactNode }) {
    const [mode, setMode] = useState<PaletteMode>(() => {
        const stored = localStorage.getItem(STORAGE_KEY)
        return stored === 'dark' ? 'dark' : 'light'
    })

    useEffect(() => {
        localStorage.setItem(STORAGE_KEY, mode)
    }, [mode])

    const theme = useMemo(() => createAppTheme(mode), [mode])

    function toggleColorMode() {
        setMode((prev) => (prev === 'light' ? 'dark' : 'light'))
    }

    return (
        <ColorModeContext.Provider value={{ mode, toggleColorMode }}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                {children}
            </ThemeProvider>
        </ColorModeContext.Provider>
    )
}

export function useColorMode() {
    const ctx = useContext(ColorModeContext)
    if (!ctx) throw new Error('useColorMode must be used inside ColorModeProvider')
    return ctx
}
