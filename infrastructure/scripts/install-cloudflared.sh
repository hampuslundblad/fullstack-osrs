#!/bin/bash

# Exit if any errors.
set -e

# Function to handle errors
error_exit() {
    echo "Error: $1"
    exit 1
}

# Create the directory for keyrings
sudo mkdir -p --mode=0755 /usr/share/keyrings || error_exit "Failed to create /usr/share/keyrings directory"

# Download the Cloudflare GPG key
curl -fsSL https://pkg.cloudflare.com/cloudflare-main.gpg | sudo tee /usr/share/keyrings/cloudflare-main.gpg >/dev/null || error_exit "Failed to download Cloudflare GPG key"

# Add the Cloudflare repository to the sources list
echo "deb [signed-by=/usr/share/keyrings/cloudflare-main.gpg] https://pkg.cloudflare.com/cloudflared any main" | sudo tee /etc/apt/sources.list.d/cloudflared.list || error_exit "Failed to add Cloudflare repository to sources list"

# Update package lists
sudo apt-get update || error_exit "Failed to update package lists"

# Install cloudflared
sudo apt-get install -y cloudflared || error_exit "Failed to install cloudflared"

echo "Cloudflared installation completed successfully."