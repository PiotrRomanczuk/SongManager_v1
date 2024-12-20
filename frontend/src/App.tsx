import { useState } from 'react';
import { useAuth } from './contexts/AuthContext';
import RegisterPage from './pages/RegisterPage';
import StudentMainPage from './pages/SongsPage/StudentMainPage';

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

	return <StudentMainPage logout={logout} user={user} />;
}

export default App;
