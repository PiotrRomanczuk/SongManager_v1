import { NavBarProps } from '../../../components/NavBar';
import {
	Navigate,
	Route,
	BrowserRouter as Router,
	Routes,
} from 'react-router-dom';
import { SongsPage } from './SongsPage';
import { SongInfoPage } from '../SongPage/SongInfoPage';

const StudentMainPage: React.FC<NavBarProps> = () => {
	return (
		<Router>
			<div className='flex flex-col min-h-screen w-screen'>
				<main className='flex-1 py-3'>
					<Routes>
						<Route path='/songs' element={<SongsPage />} />
						<Route path='/' element={<Navigate to='/songs' replace />} />
						<Route path='/songs/:shortTitle' element={<SongInfoPage />} />
					</Routes>
				</main>
			</div>
		</Router>
	);
};

export default StudentMainPage;
