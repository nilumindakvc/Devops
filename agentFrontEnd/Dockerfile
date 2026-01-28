
# Use Node.js official image as base
FROM node:18-alpine as build

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json (if available)
COPY package*.json ./

# Remove package-lock.json to avoid optional dependency issues
RUN rm -f package-lock.json

# Install dependencies (fresh install to handle native binaries properly)
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the application for production
RUN npm run build

# Use nginx to serve the built application
FROM nginx:alpine

# Copy the built application from the previous stage
COPY --from=build /app/dist /usr/share/nginx/html

# Copy the startup script
COPY start-nginx.sh /usr/local/bin/start-nginx.sh

# Make the script executable
RUN chmod +x /usr/local/bin/start-nginx.sh

# Set default environment variable
ENV REACT_APP_API_BASE_URL=http://localhost:5823

# Expose port 80
EXPOSE 80

# Use custom startup script instead of direct nginx
CMD ["/usr/local/bin/start-nginx.sh"]