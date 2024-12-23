import React from 'react';

const Dashboard = () => {
	return (
		<div className='fixed top-0 left-0 h-full w-1/4 p-4 bg-gray-100 rounded-lg shadow-md'>
			<h1 className='text-2xl font-bold mb-4'>Dashboard</h1>
			<ul className='space-y-2'>
				<li className='p-2 bg-white rounded-lg shadow-sm hover:bg-gray-200'>
					Songs
				</li>
				<li className='p-2 bg-white rounded-lg shadow-sm hover:bg-gray-200'>
					Lessons
				</li>
				<li className='p-2 bg-white rounded-lg shadow-sm hover:bg-gray-200'>
					Students
				</li>
			</ul>
		</div>
	);
};

export default Dashboard;
