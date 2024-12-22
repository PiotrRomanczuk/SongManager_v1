import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import { Song } from 'types/Song';
import { LoadingComponent } from './songPageComp/LoadingComponent';
import { ErrorComponent } from './songPageComp/ErrorComponent';
import { SongDetails } from './songPageComp/SongDetails';
import { useLoadSongs } from '@/hooks/useLoadSongs';

export function SongDetailsPage() {
	const { shortTitle } = useParams<{ shortTitle: string }>();
	const { loading, songs, error } = useLoadSongs();
	const [song, setSong] = useState<Song | null>(null);

	useEffect(() => {
		const foundSong = songs.find((song) => song.shortTitle === shortTitle);
		setSong(foundSong || null);
	}, [songs, shortTitle]);

	if (loading) {
		return <LoadingComponent message='Loading song data...' />;
	}

	if (error) {
		return <ErrorComponent error={error} />;
	}

	if (!song) {
		return <ErrorComponent error='Song not found.' />;
	}

	return <SongDetails song={song} />;
}
