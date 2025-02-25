import { useState } from 'react';
import { Link } from 'react-router-dom';
import {
	Table,
	TableBody,
	TableCell,
	TableHead,
	TableHeader,
	TableRow,
} from '@/components/ui/table';
import { Button } from '@/components/ui/button';
import {
	Pagination,
	PaginationContent,
	PaginationItem,
	PaginationLink,
	PaginationNext,
	PaginationPrevious,
} from '@/components/ui/pagination';
import { Song } from '@/types/Song';

interface SongsTableProps {
	songs: Song[];
	isAdmin: boolean;
	totalSongs: number;
}

export function SongsTable({ songs, isAdmin, totalSongs }: SongsTableProps) {
	const [currentPage, setCurrentPage] = useState(1);
	const itemsPerPage = 12;
	const totalPages = Math.ceil(totalSongs / itemsPerPage);

	const startIndex = (currentPage - 1) * itemsPerPage;
	const endIndex = startIndex + itemsPerPage;
	const currentSongs = songs.slice(startIndex, endIndex);

	const handlePageChange = (page: number) => {
		setCurrentPage(page);
	};

	return (
		<div className='space-y-4'>
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
						{currentSongs.map((song) => (
							<TableRow key={song.id}>
								<TableCell>{song.title}</TableCell>
								<TableCell>{song.author}</TableCell>
								<TableCell>{song.level}</TableCell>
								<TableCell>{song.songKey}</TableCell>
								<TableCell>
									<div className='space-x-2'>
										<Link to={`/songs/${song.shortTitle}`}>
											<Button variant='secondary' size='sm'>
												View Details
											</Button>
										</Link>
										{isAdmin && (
											<Button variant='outline' size='sm'>
												Edit
											</Button>
										)}
									</div>
								</TableCell>
							</TableRow>
						))}
					</TableBody>
				</Table>
			</div>
			{totalPages > 1 && (
				<Pagination>
					<PaginationContent>
						<PaginationItem>
							<PaginationPrevious
								onClick={() => handlePageChange(Math.max(1, currentPage - 1))}
								className={
									currentPage === 1 ? 'pointer-events-none opacity-50' : ''
								}
								size={undefined}
							/>
						</PaginationItem>
						{[...Array(totalPages)].map((_, index) => (
							<PaginationItem key={index} className='px-1'>
								<PaginationLink
									onClick={() => handlePageChange(index + 1)}
									isActive={currentPage === index + 1}
									size={undefined}
								>
									{index + 1}
								</PaginationLink>
							</PaginationItem>
						))}
						<PaginationItem>
							<PaginationNext
								onClick={() =>
									handlePageChange(Math.min(totalPages, currentPage + 1))
								}
								className={
									currentPage === totalPages
										? 'pointer-events-none opacity-50'
										: ''
								}
								size={undefined}
							/>
						</PaginationItem>
					</PaginationContent>
				</Pagination>
			)}
		</div>
	);
}
