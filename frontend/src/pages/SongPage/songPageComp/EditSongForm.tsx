import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Song } from '@/types/Song';
import { songsApi } from '@/lib/api';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Textarea } from '@/components/ui/textarea';
import { Label } from '@/components/ui/label';

interface EditSongFormProps {
	song: Song;
	onCancel: () => void;
}

const EditSongForm = ({ song, onCancel }: EditSongFormProps) => {
	const [formData, setFormData] = useState(song);
	const navigate = useNavigate();

	const handleChange = (
		e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
	) => {
		const { name, value } = e.target;
		setFormData((prev) => ({ ...prev, [name]: value }));
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		await songsApi.updateSong(song.id, formData);
		navigate(`/songs/${song.id}`);
	};

	return (
		<form onSubmit={handleSubmit} className='space-y-4'>
			<div>
				<Label htmlFor='title'>Title</Label>
				<Input
					id='title'
					name='title'
					value={formData.title}
					onChange={handleChange}
					required
				/>
			</div>
			<div>
				<Label htmlFor='author'>Author</Label>
				<Input
					id='author'
					name='author'
					value={formData.author}
					onChange={handleChange}
					required
				/>
			</div>
			<div>
				<Label htmlFor='level'>Level</Label>
				<Input
					id='level'
					name='level'
					value={formData.level}
					onChange={handleChange}
					required
				/>
			</div>
			<div>
				<Label htmlFor='songKey'>Key</Label>
				<Input
					id='songKey'
					name='songKey'
					value={formData.songKey}
					onChange={handleChange}
					required
				/>
			</div>
			{/* <div>
				<Label htmlFor='lyrics'>Lyrics</Label>
				<Textarea
					id='lyrics'
					name='lyrics'
					value={formData.lyrics}
					onChange={handleChange}
					rows={10}
				/>
			</div> */}
			<div>
				<Label htmlFor='chords'>Chords</Label>
				<Textarea
					id='chords'
					name='chords'
					value={formData.chords}
					onChange={handleChange}
					rows={10}
				/>
			</div>
			{/* <div>
				<Label htmlFor='notes'>Notes</Label>
				<Textarea
					id='notes'
					name='notes'
					value={formData.notes}
					onChange={handleChange}
					rows={5}
				/>
			</div> */}
			<div className='space-x-4'>
				<Button type='submit'>Save Changes</Button>
				<Button type='button' variant='outline' onClick={onCancel}>
					Cancel
				</Button>
			</div>
		</form>
	);
};

export default EditSongForm;
