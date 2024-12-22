import { useState } from 'react';
import { songsApi } from '@/lib/api';
import { Button } from '@/components/ui/button';

interface SongFormProps {
	onSuccess: () => void;
	initialData?: Partial<SongFormData>;
	mode: 'create' | 'edit';
	songId?: number;
}

interface SongFormData {
	title: string;
	artist: string;
	classGroup: string;
}

export function SongForm({
	onSuccess,
	initialData,
	mode,
	songId,
}: SongFormProps) {
	const [formData, setFormData] = useState<SongFormData>({
		title: initialData?.title || '',
		artist: initialData?.artist || '',
		classGroup: initialData?.classGroup || '',
	});

	const [error, setError] = useState<string | null>(null);
	const [loading, setLoading] = useState(false);

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		setLoading(true);
		setError(null);

		try {
			if (mode === 'create') {
				await songsApi.createSong(formData);
			} else if (mode === 'edit' && songId) {
				await songsApi.updateSong(songId.toString(), formData);
			}
			onSuccess();
		} catch (err) {
			setError('Failed to save song');
			console.error(err);
		} finally {
			setLoading(false);
		}
	};

	return (
		<form onSubmit={handleSubmit} className='space-y-4'>
			<div>
				<label htmlFor='title' className='block text-sm font-medium mb-1'>
					Title *
				</label>
				<input
					type='text'
					id='title'
					value={formData.title}
					onChange={(e) => setFormData({ ...formData, title: e.target.value })}
					className='w-full p-2 border rounded-md'
					required
				/>
			</div>

			<div>
				<label htmlFor='artist' className='block text-sm font-medium mb-1'>
					Artist
				</label>
				<input
					type='text'
					id='artist'
					value={formData.artist}
					onChange={(e) => setFormData({ ...formData, artist: e.target.value })}
					className='w-full p-2 border rounded-md'
				/>
			</div>

			<div>
				<label htmlFor='classGroup' className='block text-sm font-medium mb-1'>
					Class Group *
				</label>
				<input
					type='text'
					id='classGroup'
					value={formData.classGroup}
					onChange={(e) =>
						setFormData({ ...formData, classGroup: e.target.value })
					}
					className='w-full p-2 border rounded-md'
					required
				/>
			</div>

			{error && <p className='text-red-500 text-sm'>{error}</p>}

			<div className='flex justify-end space-x-2'>
				<Button type='submit' disabled={loading}>
					{loading
						? 'Saving...'
						: mode === 'create'
						? 'Create Song'
						: 'Update Song'}
				</Button>
			</div>
		</form>
	);
}
