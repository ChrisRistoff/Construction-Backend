#!/bin/bash

set -e

echo "Setting environment variables..."
CONTAINER_NAME="construction-back"
IMAGE_NAME="krahristov/construction-backend:latest"
APP_DIR="$HOME/construction-backend"

echo "Starting deployment..."

echo "Pulling the latest Docker image..."
sudo docker pull $IMAGE_NAME

echo "Stopping the existing Docker container..."
sudo docker stop $CONTAINER_NAME || true
sudo docker rm $CONTAINER_NAME || true

echo "Running the new Docker container..."
sudo docker run -p 8080:8080 -d --name $CONTAINER_NAME $IMAGE_NAME

echo "Removing unused Docker images..."
sudo docker image prune -af

echo "Deployment successful!"