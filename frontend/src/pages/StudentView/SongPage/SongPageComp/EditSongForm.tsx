import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Song } from '@/types/Song';
import { songsApi } from '@/lib/api';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Textarea } from '@/components/ui/textarea';
import { Label } from '@/components/ui/label';
import {
	Select,
	SelectContent,
	SelectItem,
	SelectTrigger,
	SelectValue,
} from '@/components/ui/select';

interface EditSongFormProps {
	song: Song;
	onCancel: () => void;
}

const EditSongForm = ({ song, onCancel }: EditSongFormProps) => {
	const [formData, setFormData] = useState(song);
	// const navigate = useNavigate();

	const refreshPage = () => {
		window.location.reload();
	};

	const handleChange = (
		e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
	) => {
		const { name, value } = e.target;
		setFormData((prev) => ({ ...prev, [name]: value }));
	};

	const handleSelectChange = (value: string) => {
		setFormData((prev) => ({ ...prev, level: value }));
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		await songsApi.updateSong(song.id, formData);
		console.log(formData);
		refreshPage();
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
				<Select onValueChange={handleSelectChange} value={formData.level}>
					<SelectTrigger className='w-[180px]'>
						<SelectValue placeholder='Song level' />
					</SelectTrigger>
					<SelectContent>
						<SelectItem value='beginner'>beginner</SelectItem>
						<SelectItem value='intermediate'>intermediate</SelectItem>
						<SelectItem value='advanced'>advanced</SelectItem>
					</SelectContent>
				</Select>
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
