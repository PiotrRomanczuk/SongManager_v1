import { useState } from 'react';
import { useAuth } from '@/contexts/AuthContext';
import StudentMainPage from '@/pages/StudentView/SongsPage/StudentMainPage';
import AuthPage from '@/pages/RegisterPage/AuthPage';
import Dashboard from '@/components/Dashboard';
import NavBar from '@/components/NavBar';

function App() {
	const [showRegister, setShowRegister] = useState(false);
	const { isAuthenticated, user, logout } = useAuth();

	if (!isAuthenticated) {
		return (
			<AuthPage showRegister={showRegister} setShowRegister={setShowRegister} />
		);
	}

	return (
		<>
			<NavBar logout={logout} user={user} />
			{/* <Dashboard /> */}
			<StudentMainPage logout={logout} user={user} />
		</>
	);
}

export default App;
