import { createTheme } from '@mui/material/styles'

const theme = createTheme({
  palette: {
    primary: {
      main: '#C8C000',
      dark: '#3F3F2E',
      contrastText: '#2C2C1F',
    },
    background: {
      default: '#F9F8F2',
      paper: '#FFFFFF',
    },
    text: {
      primary: '#2C2C1F',
      secondary: '#787868',
    },
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
            backgroundColor: '#C8C000',
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
        },
        elevation1: {
          boxShadow: '0 2px 2px rgba(44,44,31,0.04), 0 4px 4px rgba(44,44,31,0.04), 0 8px 8px rgba(44,44,31,0.04), 0 0 0 1px rgba(44,44,31,0.06)',
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 12,
          boxShadow: '0 2px 2px rgba(44,44,31,0.04), 0 4px 4px rgba(44,44,31,0.04), 0 8px 8px rgba(44,44,31,0.04), 0 0 0 1px rgba(44,44,31,0.06)',
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
            color: '#787868',
            borderBottom: '1px solid #E4E4D0',
            paddingTop: 14,
            paddingBottom: 14,
          },
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          borderBottom: '1px solid #E4E4D0',
          color: '#2C2C1F',
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
            backgroundColor: 'rgba(44,44,31,0.025)',
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
          boxShadow: '0 25px 50px -12px rgba(44,44,31,0.2)',
        },
      },
    },
    MuiOutlinedInput: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          '& .MuiOutlinedInput-notchedOutline': {
            borderColor: '#E4E4D0',
          },
          '&:hover .MuiOutlinedInput-notchedOutline': {
            borderColor: '#AEAE98',
          },
        },
      },
    },
  },
})

export default theme
