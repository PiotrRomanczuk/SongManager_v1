import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from '../../../components/ui/table';
import { Button } from '../../../components/ui/button';
import { Song } from '../../../types/Song';

interface SongsTableProps {
	songs: Song[];
	isAdmin: boolean;
}

export function SongTable({ songs, isAdmin }: SongsTableProps) {
	return (
		<div className='rounded-md border'>
			<Table>
				<TableHeader>
					<TableRow>
						<TableHead>Title</TableHead>
						<TableHead>Author</TableHead>
						<TableHead>Level</TableHead>
						<TableHead>Key</TableHead>
						<TableHead>Actions</TableHead>
					</TableRow>
				</TableHeader>
				<TableBody>
					{songs.map((song) => (
						<TableRow key={song.id}>
							<TableCell>{song.title}</TableCell>
							<TableCell>{song.author}</TableCell>
							<TableCell>{song.level}</TableCell>
							<TableCell>{song.songKey}</TableCell>
							<TableCell>
								{isAdmin && (
									<Button variant='outline' size='sm'>
										Edit
									</Button>
								)}
							</TableCell>
						</TableRow>
					))}
				</TableBody>
			</Table>
		</div>
	);
}
