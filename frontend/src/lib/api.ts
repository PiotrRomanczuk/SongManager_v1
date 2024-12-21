import axios from 'axios';
import { Song } from '../types/Song';

const api = axios.create({
	baseURL: 'http://localhost:5000',
	headers: {
		'Content-Type': 'application/json',
	},
});

// Add request interceptor to add auth token
api.interceptors.request.use((config) => {
	const token = localStorage.getItem('token');
	if (token) {
		config.headers.Authorization = `Bearer ${token}`;
	}
	return config;
});

export interface AuthResponse {
	name: string;
	token: string;
	email: string;
	userName: string;
	roles: string[];
}

export const authApi = {
	login: async (email: string, password: string) => {
		const response = await api.post<AuthResponse>('/api/auth/login', {
			email,
			password,
		});
		return response.data;
	},

	register: async (name: string, email: string, password: string) => {
		const response = await api.post<AuthResponse>('/api/auth/register', {
			name,
			email,
			password,
			confirmPassword: password,
		});
		return response.data;
	},
};

export const songsApi = {
	getAllSongs: async () => {
		const response = await api.get<Song[]>('/api/songs');
		return response.data;
	},

	getSong: async (id: string) => {
		const response = await api.get<Song>(`/api/songs/${id}`);
		return response.data;
	},

	createSong: async (song: Omit<Song, 'id' | 'createdAt'>) => {
		const response = await api.post<Song>('/api/songs', song);
		return response.data;
	},

	updateSong: async (id: string, song: Omit<Song, 'id' | 'createdAt'>) => {
		const response = await api.put<Song>(`/api/songs/${id}`, song);
		return response.data;
	},

	deleteSong: async (id: string) => {
		await api.delete(`/api/songs/${id}`);
	},

	importSongs: async (file: File) => {
		const formData = new FormData();
		formData.append('file', file);
		const response = await api.post<void>('/api/songs/import', formData, {
			headers: {
				'Content-Type': 'multipart/form-data',
			},
		});
		return response.data;
	},
};
