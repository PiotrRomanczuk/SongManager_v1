'use client';

import { useState } from 'react';
import { Input } from '../../../components/ui/input';
import { Button } from '../../../components/ui/button';

interface SearchComponentProps {
	onSearch: (query: string) => void;
	placeholder?: string;
}

export function SearchComponent({
	onSearch,
	placeholder = 'Search...',
}: SearchComponentProps) {
	const [searchQuery, setSearchQuery] = useState('');

	const handleSearch = (e: React.FormEvent) => {
		e.preventDefault();
		onSearch(searchQuery);
	};

	return (
		<form
			onSubmit={handleSearch}
			className='flex w-full max-w-sm items-center space-x- my-4'
		>
			<Input
				type='text'
				placeholder={placeholder}
				value={searchQuery}
				onChange={(e) => setSearchQuery(e.target.value)}
				className='flex-grow'
			/>
			<Button type='submit'>Search</Button>
		</form>
	);
}
