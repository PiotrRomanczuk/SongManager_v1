# Use the official Node.js image as a build environment
FROM node:20 AS build-env
WORKDIR /app

# Copy package.json and package-lock.json
COPY frontend/package.json frontend/package-lock.json ./

# Clear npm cache
RUN npm cache clean --force

# Install dependencies
RUN npm install

# Copy the rest of the application
COPY frontend/ ./

# Build the application
RUN npm run build

# Use the official Nginx image to serve the frontend
FROM nginx:alpine
COPY --from=build-env /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]