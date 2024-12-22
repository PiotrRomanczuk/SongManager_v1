import { NavBarProps } from '../../components/NavBar';
import {
	Navigate,
	Route,
	BrowserRouter as Router,
	Routes,
} from 'react-router-dom';
import NavBar from '../../components/NavBar';
import { SongsPage } from './SongsPage';
import { SongDetailsPage } from '../SongPage/SongDetailsPage';

const StudentMainPage: React.FC<NavBarProps> = ({ logout, user }) => {
	return (
		<Router>
			<div className='flex flex-col min-h-screen w-screen'>
				<NavBar logout={logout} user={user} />
				<main className='flex-1 py-3'>
					<Routes>
						<Route path='/songs' element={<SongsPage />} />
						<Route path='/' element={<Navigate to='/songs' replace />} />
						<Route path='/songs/:shortTitle' element={<SongDetailsPage />} />
					</Routes>
				</main>
			</div>
		</Router>
	);
};

export default StudentMainPage;
