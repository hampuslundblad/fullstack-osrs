#!/bin/bash

set -e

# Function to handle errors
error_exit() {
    echo "Error: $1"
    exit 1
}

# Update package lists
sudo apt update || error_exit "Failed to update package lists"

# Install certbot
sudo apt install -y certbot || error_exit "Failed to install certbot"

# Obtain the SSL certificate
sudo certbot certonly --webroot --webroot-path /home/hampus/fullstack-osrs -d hampuslundblad.com --agree-tos -m hampus.lundblad@hotmail.se -non-interactive|| error_exit "Failed to obtain SSL certificate"

# Create the directory for SSL certificates
sudo mkdir -p /etc/letsencrypt/ssl || error_exit "Failed to create /etc/letsencrypt/ssl directory"

# Copy the SSL certificates
sudo cp -r -L /etc/letsencrypt/live/hampuslundblad.com/fullchain.pem /etc/letsencrypt/ssl/ || error_exit "Failed to copy fullchain.pem"
sudo cp -r -L /etc/letsencrypt/live/hampuslundblad.com/privkey.pem /etc/letsencrypt/ssl/ || error_exit "Failed to copy privkey.pem"

echo "SSL certificate setup completed successfully."