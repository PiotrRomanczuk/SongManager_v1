import { useState } from 'react'
import { Button } from './components/ui/button'
import { Dialog, DialogContent, DialogHeader, DialogTitle } from './components/ui/dialog'
import { useAuth } from './contexts/AuthContext'
import { LoginForm } from './components/auth/LoginForm'
import { RegisterForm } from './components/auth/RegisterForm'
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'
import SongsPage from './pages/SongsPage'
import { Container, Box, Typography, Button as MuiButton } from '@mui/material'

function App() {
  const [showRegister, setShowRegister] = useState(false)
  const { isAuthenticated, user, logout } = useAuth()

  if (!isAuthenticated) {
    return (
      <Container maxWidth="sm">
        <Box sx={{ my: 4 }}>
          <Typography variant="h4" component="h1" gutterBottom>
            Welcome to Songs API
          </Typography>
          <LoginForm />
          <Box sx={{ mt: 2 }}>
            <Button onClick={() => setShowRegister(true)}>Register</Button>
          </Box>
          <Dialog open={showRegister} onOpenChange={setShowRegister}>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>Register</DialogTitle>
              </DialogHeader>
              <RegisterForm onSuccess={() => setShowRegister(false)} />
            </DialogContent>
          </Dialog>
        </Box>
      </Container>
    )
  }

  return (
    <Router>
      <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
        <Box component="nav" sx={{ py: 2, px: 3, bgcolor: 'primary.main', color: 'white' }}>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Typography variant="h6">Songs API</Typography>
            <Box>
              <Typography component="span" sx={{ mr: 2 }}>
                Welcome, {user?.name}
              </Typography>
              <MuiButton onClick={logout} variant="contained" color="secondary">
                Logout
              </MuiButton>
            </Box>
          </Box>
        </Box>

        <Box component="main" sx={{ flex: 1, py: 3 }}>
          <Routes>
            <Route path="/songs" element={<SongsPage />} />
            <Route path="/" element={<Navigate to="/songs" replace />} />
          </Routes>
        </Box>
      </Box>
    </Router>
  )
}

export default App
