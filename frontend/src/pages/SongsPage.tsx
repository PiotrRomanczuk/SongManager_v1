import { useEffect, useState } from 'react';
import {
	Box,
	Container,
	Typography,
	Button,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Paper,
} from '@mui/material';
import { useAuth } from '../contexts/AuthContext';
import { Song, songsApi } from '../lib/api';

export default function SongsPage() {
	const { isAdmin } = useAuth();
	const [songs, setSongs] = useState<Song[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);

	useEffect(() => {
		loadSongs();
	}, []);

	const loadSongs = async () => {
		try {
			setLoading(true);
			console.log('Fetching songs...');
			const songs = await songsApi.getAllSongs();
			console.log('Fetched songs:', songs);
			setSongs(songs);
			setError(null);
		} catch (error) {
			console.error('Error loading songs:', error);
			setError('Failed to load songs');
		} finally {
			setLoading(false);
		}
	};

	if (loading) {
		return (
			<Container maxWidth='lg'>
				<Box sx={{ my: 4, textAlign: 'center' }}>
					<Typography>Loading...</Typography>
				</Box>
			</Container>
		);
	}

	if (error) {
		return (
			<Container maxWidth='lg'>
				<Box sx={{ my: 4, textAlign: 'center' }}>
					<Typography color='error' gutterBottom>
						{error}
					</Typography>
					<Button variant='contained' onClick={loadSongs}>
						Try Again
					</Button>
				</Box>
			</Container>
		);
	}

	return (
		<Container maxWidth='lg'>
			<Box sx={{ my: 4 }}>
				<Typography variant='h4' component='h1' gutterBottom>
					Songs
				</Typography>

				{isAdmin && (
					<Box sx={{ mb: 2 }}>
						<Button variant='contained' color='primary'>
							Import Songs
						</Button>
					</Box>
				)}

				{songs.length === 0 ? (
					<Box sx={{ textAlign: 'center', my: 4 }}>
						<Typography color='textSecondary'>No songs found.</Typography>
					</Box>
				) : (
					<TableContainer component={Paper}>
						<Table>
							<TableHead>
								<TableRow>
									<TableCell>Title</TableCell>
									<TableCell>Author</TableCell>
									<TableCell>Level</TableCell>
									<TableCell>Key</TableCell>
									<TableCell>Actions</TableCell>
								</TableRow>
							</TableHead>
							<TableBody>
								{songs.map((song) => (
									<TableRow key={song.id}>
										<TableCell>{song.title}</TableCell>
										<TableCell>{song.author}</TableCell>
										<TableCell>{song.level}</TableCell>
										<TableCell>{song.songKey}</TableCell>
										<TableCell>
											{isAdmin && (
												<Button color='primary' size='small'>
													Edit
												</Button>
											)}
										</TableCell>
									</TableRow>
								))}
							</TableBody>
						</Table>
					</TableContainer>
				)}
			</Box>
		</Container>
	);
}
