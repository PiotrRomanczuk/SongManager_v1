import { Container } from '../../components/ui/container';
import { Button } from '../../components/ui/button';
import { useAuth } from '../../contexts/AuthContext';
import LoadingComponent from './components/LoadingComponent';
import ErrorComponent from './components/ErrorComponent';
import { SongTable } from './components/SongTable';
import useLoadSongs from '../../hooks/useLoadSongs';

export function SongsPage() {
	const { isAdmin } = useAuth();
	const { loading, songs, error } = useLoadSongs();

	if (loading) {
		return <LoadingComponent message='Loading songs...' />;
	}

	if (error) {
		return (
			<ErrorComponent
				error='Something wrong happened...'
				loadSongs={() => window.location.reload()}
			/>
		);
	}

	return (
		<Container className='max-w-4xl'>
			<div className='my-8'>
				Songs
				{isAdmin && (
					<div className='mb-4'>
						<Button>Import Songs</Button>
					</div>
				)}
				<SongTable songs={songs} isAdmin={isAdmin} />
			</div>
		</Container>
	);
}
