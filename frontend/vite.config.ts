import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import * as path from 'path';

export default defineConfig({
	resolve: {
		alias: {
			'@': path.resolve(__dirname, 'src'), // Ensure this matches the base directory in tsconfig.json
			'@server': path.resolve(__dirname, '../server/src'),
		},
	},
	plugins: [react()],
});
