import { createTheme } from '@mui/material/styles'
import type { PaletteMode } from '@mui/material'

/** Per-mode design tokens. Light values are the original editorial olive/gold palette;
 *  dark values are a "stadium night" variant that keeps the gold accent. */
function getTokens(mode: PaletteMode) {
  if (mode === 'dark') {
    return {
      primaryMain: '#C8C000',
      primaryDark: '#9A9400',
      primaryContrast: '#1C1C14',
      bgDefault: '#14140E',
      bgPaper: '#1F1F16',
      textPrimary: '#F4F3EA',
      textSecondary: '#A3A38E',
      border: '#36362A',
      borderHover: '#55554A',
      tableHead: '#A3A38E',
      emptyIcon: '#3A3A28',
      rowHover: 'rgba(249,248,242,0.04)',
      paperShadow:
        '0 2px 2px rgba(0,0,0,0.2), 0 4px 8px rgba(0,0,0,0.24), 0 0 0 1px rgba(255,255,255,0.04)',
    }
  }
  return {
    primaryMain: '#C8C000',
    primaryDark: '#3F3F2E',
    primaryContrast: '#2C2C1F',
    bgDefault: '#F9F8F2',
    bgPaper: '#FFFFFF',
    textPrimary: '#2C2C1F',
    textSecondary: '#787868',
    border: '#E4E4D0',
    borderHover: '#AEAE98',
    tableHead: '#787868',
    emptyIcon: '#D8D8C4',
    rowHover: 'rgba(44,44,31,0.025)',
    paperShadow:
      '0 2px 2px rgba(44,44,31,0.04), 0 4px 4px rgba(44,44,31,0.04), 0 8px 8px rgba(44,44,31,0.04), 0 0 0 1px rgba(44,44,31,0.06)',
  }
}

export function createAppTheme(mode: PaletteMode) {
  const t = getTokens(mode)

  return createTheme({
    palette: {
      mode,
      primary: {
        main: t.primaryMain,
        dark: t.primaryDark,
        contrastText: t.primaryContrast,
      },
      background: {
        default: t.bgDefault,
        paper: t.bgPaper,
      },
      text: {
        primary: t.textPrimary,
        secondary: t.textSecondary,
      },
      divider: t.border,
    },
    typography: {
      fontFamily: '"Inter", system-ui, sans-serif',
      // display → weight 300 (editorial signature)
      h1: { fontWeight: 300, fontSize: '6rem', lineHeight: 1.0, letterSpacing: '2.4px' },
      h2: { fontWeight: 300, fontSize: '4.375rem', lineHeight: 1.0 },
      h3: { fontWeight: 300, fontSize: '3.4375rem', lineHeight: 1.16 },
      // headings → weight 600
      h4: { fontWeight: 600, fontSize: '1.75rem', lineHeight: 1.28, letterSpacing: '0.42px' },
      h5: { fontWeight: 600, fontSize: '1.25rem', lineHeight: 1.4, letterSpacing: '0.3px' },
      h6: { fontWeight: 600, fontSize: '1.125rem', lineHeight: 1.25, letterSpacing: '0.72px' },
      body1: { fontWeight: 420, fontSize: '1rem', lineHeight: 1.5 },
      body2: { fontWeight: 500, fontSize: '0.875rem', lineHeight: 1.49, letterSpacing: '0.28px' },
      caption: { fontWeight: 400, fontSize: '0.75rem', lineHeight: 1.2, letterSpacing: '0.72px' },
    },
    shape: {
      borderRadius: 8,
    },
    components: {
      MuiButton: {
        styleOverrides: {
          root: {
            textTransform: 'none',
            fontWeight: 420,
            borderRadius: 9999,
            padding: '10px 24px',
            lineHeight: 1.5,
            boxShadow: 'none',
            '&:hover': { boxShadow: 'none' },
            '&:active': { boxShadow: 'none' },
            '&.MuiButton-containedPrimary': {
              backgroundColor: t.primaryMain,
              color: '#2C2C1F',
              '&:hover': { backgroundColor: '#b8b000' },
              '&:active': { backgroundColor: '#3F3F2E', color: '#F9F8F2' },
            },
          },
          sizeSmall: {
            padding: '6px 16px',
          },
        },
      },
      MuiAppBar: {
        styleOverrides: {
          root: {
            backgroundColor: '#1C1C14',
            color: '#F9F8F2',
            boxShadow: 'none',
            borderBottom: '1px solid #3A3A28',
            borderRadius: 0,
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            borderRadius: 12,
            backgroundImage: 'none',
          },
          elevation1: {
            boxShadow: t.paperShadow,
          },
        },
      },
      MuiCard: {
        styleOverrides: {
          root: {
            borderRadius: 12,
            boxShadow: t.paperShadow,
          },
        },
      },
      MuiTableHead: {
        styleOverrides: {
          root: {
            '& .MuiTableCell-root': {
              fontWeight: 500,
              fontSize: '0.75rem',
              letterSpacing: '0.72px',
              textTransform: 'uppercase',
              color: t.tableHead,
              borderBottom: `1px solid ${t.border}`,
              paddingTop: 14,
              paddingBottom: 14,
            },
          },
        },
      },
      MuiTableCell: {
        styleOverrides: {
          root: {
            borderBottom: `1px solid ${t.border}`,
            color: t.textPrimary,
            fontSize: '0.9375rem',
          },
        },
      },
      MuiTableRow: {
        styleOverrides: {
          root: {
            '&:last-child .MuiTableCell-root': {
              borderBottom: 0,
            },
            '&.MuiTableRow-hover:hover': {
              backgroundColor: t.rowHover,
            },
          },
        },
      },
      MuiChip: {
        styleOverrides: {
          root: {
            borderRadius: 9999,
            fontWeight: 400,
            fontSize: '0.75rem',
            letterSpacing: '0.72px',
            height: 24,
          },
        },
      },
      MuiDialog: {
        styleOverrides: {
          paper: {
            borderRadius: 12,
            boxShadow:
              mode === 'dark'
                ? '0 25px 50px -12px rgba(0,0,0,0.6)'
                : '0 25px 50px -12px rgba(44,44,31,0.2)',
          },
        },
      },
      MuiOutlinedInput: {
        styleOverrides: {
          root: {
            borderRadius: 8,
            '& .MuiOutlinedInput-notchedOutline': {
              borderColor: t.border,
            },
            '&:hover .MuiOutlinedInput-notchedOutline': {
              borderColor: t.borderHover,
            },
          },
        },
      },
    },
  })
}

/** Default light theme — kept for any non mode-aware imports. */
const theme = createAppTheme('light')

export default theme
