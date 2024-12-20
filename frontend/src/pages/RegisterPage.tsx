import {
	Box,
	Container,
	Dialog,
	DialogContent,
	DialogTitle,
	Typography,
} from '@mui/material';
import { LoginForm } from '../components/auth/LoginForm';
import { Button } from '../components/ui/button';
import { DialogHeader } from '../components/ui/dialog';
import { RegisterForm } from '../components/auth/RegisterForm';

interface RegisterPageProps {
	showRegister: boolean;
	setShowRegister: (show: boolean) => void;
}

export default function RegisterPage({
	showRegister,
	setShowRegister,
}: RegisterPageProps) {
	return (
		<Container maxWidth='sm'>
			<Box sx={{ my: 4 }}>
				<Typography variant='h4' component='h1' gutterBottom>
					Welcome to Songs API
				</Typography>
				<LoginForm />
				<Box sx={{ mt: 2 }}>
					<Button onClick={() => setShowRegister(true)}>Register</Button>
				</Box>
				<Dialog open={showRegister} onClose={() => setShowRegister(false)}>
					<DialogContent>
						<DialogHeader>
							<DialogTitle>Register</DialogTitle>
						</DialogHeader>
						{showRegister && (
							<RegisterForm onSuccess={() => setShowRegister(false)} />
						)}
					</DialogContent>
				</Dialog>
			</Box>
		</Container>
	);
}
