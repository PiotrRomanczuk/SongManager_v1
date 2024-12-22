import { useState } from 'react';
import { useAuth } from './contexts/AuthContext';
import StudentMainPage from './pages/SongsPage/StudentMainPage';
import AuthPage from './pages/RegisterPage/AuthPage';

function App() {
	const [showRegister, setShowRegister] = useState(false);
	const { isAuthenticated, user, logout } = useAuth();

	if (!isAuthenticated) {
		return (
			<AuthPage showRegister={showRegister} setShowRegister={setShowRegister} />
		);
	}

	return <StudentMainPage logout={logout} user={user} />;
}

export default App;
