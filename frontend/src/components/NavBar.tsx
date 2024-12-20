import { Button } from './ui/button';

export interface NavBarProps {
	logout: () => void;
	user: { name: string } | null;
}

const NavBar: React.FC<NavBarProps> = ({ logout, user }) => {
	return (
		<nav className='py-2 px-3 mx-0 bg-primary text-white '>
			<div className='flex justify-between items-center'>
				<h6 className='text-lg'>Songs API</h6>
				<div>
					<span className='mr-2'>Welcome, {user?.name}</span>
					<Button onClick={logout} color='secondary'>
						Logout
					</Button>
				</div>
			</div>
		</nav>
	);
};

export default NavBar;
