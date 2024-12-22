import { Song } from '@/types/Song';
import { SongsTable } from './SongsTable';
import { Button } from '@/components/ui/button';
import { useAuth } from '@/contexts/AuthContext';
import { ErrorComponent } from './ErrorComponent';

interface SongsListProps {
	songs: Song[];
}

export function SongsOverview({ songs }: SongsListProps) {
	const { isAdmin } = useAuth();
	console.log(isAdmin);

	return (
		<div className='container mx-auto py-10'>
			<h1 className='text-3xl font-bold mb-6'>Songs</h1>

			{isAdmin && (
				<div className='mb-4'>
					<Button>Import Songs</Button>
				</div>
			)}

			{songs.length === 0 ? (
				<ErrorComponent error='No songs found' />
			) : (
				<SongsTable songs={songs} isAdmin={isAdmin} totalSongs={songs.length} />
			)}
		</div>
	);
}
