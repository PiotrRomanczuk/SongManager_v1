import { useState } from 'react';
import { useAuth } from './contexts/AuthContext';
import {
	BrowserRouter as Router,
	Routes,
	Route,
	Navigate,
} from 'react-router-dom';
import SongsPage from './pages/SongsPage';
import { Box, Typography, Button as MuiButton } from '@mui/material';
import RegisterPage from './pages/RegisterPage';

function App() {
	const [showRegister, setShowRegister] = useState(false);
	const { isAuthenticated, user, logout } = useAuth();

	if (!isAuthenticated) {
		return (
			<RegisterPage
				showRegister={showRegister}
				setShowRegister={setShowRegister}
			/>
		);
	}

	return (
		<Router>
			<Box
				sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}
			>
				<Box
					component='nav'
					sx={{ py: 2, px: 3, bgcolor: 'primary.main', color: 'white' }}
				>
					<Box
						sx={{
							display: 'flex',
							justifyContent: 'space-between',
							alignItems: 'center',
						}}
					>
						<Typography variant='h6'>Songs API</Typography>
						<Box>
							<Typography component='span' sx={{ mr: 2 }}>
								Welcome, {user?.name}
							</Typography>
							<MuiButton onClick={logout} variant='contained' color='secondary'>
								Logout
							</MuiButton>
						</Box>
					</Box>
				</Box>

				<Box component='main' sx={{ flex: 1, py: 3 }}>
					<Routes>
						<Route path='/songs' element={<SongsPage />} />
						<Route path='/' element={<Navigate to='/songs' replace />} />
					</Routes>
				</Box>
			</Box>
		</Router>
	);
}

export default App;
