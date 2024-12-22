import { Button } from '../../components/ui/button';
import {
	Dialog,
	DialogContent,
	DialogHeader,
	DialogTitle,
} from '../../components/ui/dialog';
import LoginForm from '../../components/auth/LoginForm';
import { RegisterForm } from '../../components/auth/RegisterForm';

export default function AuthPage({
	showRegister,
	setShowRegister,
}: {
	showRegister: boolean;
	setShowRegister: (open: boolean) => void;
}) {
	return (
		<div className='container max-w-sm mx-auto my-8'>
			<h1 className='text-3xl font-bold mb-6'>Welcome to Songs API</h1>
			<LoginForm />
			<div className='mt-4'>
				<Button onClick={() => setShowRegister(true)}>Register</Button>
			</div>
			<Dialog open={showRegister} onOpenChange={setShowRegister}>
				<DialogContent>
					<DialogHeader>
						<DialogTitle>Register</DialogTitle>
					</DialogHeader>
					<RegisterForm onSuccess={() => setShowRegister(false)} />
				</DialogContent>
			</Dialog>
		</div>
	);
}
