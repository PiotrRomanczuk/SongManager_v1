import { Music, User, BarChart, Key } from 'lucide-react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { songsApi } from '@/lib/api';
import { useNavigate } from 'react-router-dom';
import { Song } from '@/types/Song';

interface SongDetailCardProps {
	song: Song;
	setIsEditing: (isEditing: boolean) => void;
}

export const SongDetailCard = ({ song, setIsEditing }: SongDetailCardProps) => {
	const navigate = useNavigate();

	const handleDelete = async () => {
		if (window.confirm('Are you sure you want to delete this song?')) {
			await songsApi.deleteSong(song.id);
			navigate('/songs');
		}
	};

	return (
		<>
			<div className='grid grid-cols-1 md:grid-cols-2 gap-6'>
				<Card>
					<CardHeader>
						<CardTitle className='flex items-center'>
							<Music className='mr-2' size={20} />
							Song Details
						</CardTitle>
					</CardHeader>
					<CardContent>
						<p>
							<strong>Author:</strong> {song.author}
						</p>
						<p>
							<strong>Level:</strong> {song.level}
						</p>
						<p>
							<strong>Key:</strong> {song.songKey}
						</p>
					</CardContent>
				</Card>

				<Card>
					<CardHeader>
						<CardTitle className='flex items-center'>
							<User className='mr-2' size={20} />
							Lyrics
						</CardTitle>
					</CardHeader>
					{/* <CardContent>
								<pre className='whitespace-pre-wrap'>{song.lyrics}</pre>
							</CardContent> */}
				</Card>

				<Card>
					<CardHeader>
						<CardTitle className='flex items-center'>
							<BarChart className='mr-2' size={20} />
							Chords
						</CardTitle>
					</CardHeader>
					<CardContent>
						<pre className='whitespace-pre-wrap'>{song.chords}</pre>
					</CardContent>
				</Card>

				<Card>
					<CardHeader>
						<CardTitle className='flex items-center'>
							<Key className='mr-2' size={20} />
							Notes
						</CardTitle>
					</CardHeader>
					{/* <CardContent>
								<p>{song.notes}</p>
							</CardContent> */}
				</Card>
			</div>
			<div className='mt-8 space-x-4'>
				<Button variant='outline' onClick={() => setIsEditing(true)}>
					Edit Song
				</Button>
				<Button variant='destructive' onClick={handleDelete}>
					Delete Song
				</Button>
			</div>
		</>
	);
};
