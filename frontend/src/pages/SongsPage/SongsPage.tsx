'use client';

import { useState, useMemo } from 'react';
import { Container } from '../../components/ui/container';
import { Button } from '../../components/ui/button';
import { useAuth } from '../../contexts/AuthContext';
import { LoadingComponent } from './components/LoadingComponent';
import { ErrorComponent } from './components/ErrorComponent';
import { SongsTable } from './components/SongsTable';
import { SearchComponent } from './components/SearchComponent';
import { useLoadSongs } from '../../hooks/useLoadSongs';

export function SongsPage() {
	const { isAdmin } = useAuth();
	const { loading, songs, error } = useLoadSongs();
	const [searchQuery, setSearchQuery] = useState('');

	const filteredSongs = useMemo(() => {
		return songs.filter(
			(song) =>
				song.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
				(song.author ?? '').toLowerCase().includes(searchQuery.toLowerCase())
		);
	}, [songs, searchQuery]);

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
				<h1 className='text-2xl font-bold mb-4'>Songs</h1>
				<SearchComponent onSearch={setSearchQuery} />
				{isAdmin && (
					<div className='mb-4'>
						<Button>Import Songs</Button>
					</div>
				)}
				<SongsTable
					songs={filteredSongs}
					isAdmin={isAdmin}
					totalSongs={filteredSongs.length}
				/>
			</div>
		</Container>
	);
}
