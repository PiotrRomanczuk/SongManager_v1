import { useState } from 'react';
import { Link } from 'react-router-dom';

import { Song } from '@/types/Song';
import EditSongForm from './EditSongForm';

import { SongDetailCard } from './SongDetailCard';
import { ArrowLeft } from 'lucide-react';

interface SongInfoProps {
	song: Song;
}

export const SongInfo = ({ song }: SongInfoProps) => {
	const [isEditing, setIsEditing] = useState(false);

	return (
		<div className='container mx-auto px-4 py-8'>
			<Link
				to='/songs'
				className='inline-flex items-center mb-6 text-blue-500 hover:text-blue-600'
			>
				<ArrowLeft className='mr-2' size={20} />
				Back to Songs
			</Link>
			<h1 className='text-3xl font-bold mb-6'>{song.title}</h1>
			{isEditing ? (
				<EditSongForm song={song} onCancel={() => setIsEditing(false)} />
			) : (
				<>
					<SongDetailCard song={song} setIsEditing={setIsEditing} />
				</>
			)}
		</div>
	);
};

export default SongInfo;
