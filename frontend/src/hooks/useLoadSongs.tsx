import { useState, useEffect } from 'react';
import { songsApi } from '../lib/api';
import { Song } from '../lib/api';

const useLoadSongs = () => {
	const [loading, setLoading] = useState(false);
	const [songs, setSongs] = useState<Song[]>([]);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		const fetchSongs = async () => {
			try {
				setLoading(true);
				console.log('Fetching songs...');
				const songs = await songsApi.getAllSongs();
				console.log('Fetched songs:', songs);
				setSongs(
					songs.map((song: Song) => ({ ...song, author: song.author || '' }))
				);
				setError(null);
			} catch (error) {
				console.error('Error loading songs:', error);
				setError('Failed to load songs');
			} finally {
				setLoading(false);
			}
		};

		fetchSongs();
	}, []);

	return { loading, songs, error };
};

export default useLoadSongs;
