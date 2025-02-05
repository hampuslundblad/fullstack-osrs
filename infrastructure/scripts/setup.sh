#!/bin/bash

# Define the variables
ClientId="123"
ClientSecret="123"


# Create the main directory
mkdir -p fullstack-osrs/nginx

# Create the secrets.env file
touch fullstack-osrs/secrets.env

# Write the variables to secrets.env
echo "github__ClientId=$ClientId" > fullstack-osrs/secrets.env
echo "github__ClientSecret=$ClientSecret" >> fullstack-osrs/secrets.env


# URLs of the files to download
nginx_conf_url="https://raw.githubusercontent.com/hampuslundblad/fullstack-osrs/refs/heads/main/infrastructure/nginx/default.conf"
dockerfile_url="https://raw.githubusercontent.com/hampuslundblad/fullstack-osrs/refs/heads/main/infrastructure/Dockerfile"
docker_compose_url="https://raw.githubusercontent.com/hampuslundblad/fullstack-osrs/refs/heads/main/infrastructure/docker-compose-prod.yml"

# Destination paths
nginx_conf_dest="fullstack-osrs/nginx/default.conf"
dockerfile_dest="fullstack-osrs/Dockerfile"
docker_compose_dest="fullstack-osrs/docker-compose-prod.yml"

# Function to download a file with error handling
download_file() {
    local url=$1
    local dest=$2
    curl -fSL "$url" -o "$dest"
    if [ $? -ne 0 ]; then
        echo "Error: Failed to download $url"
        exit 1
    fi
}

# Download the files
download_file "$nginx_conf_url" "$nginx_conf_dest"
download_file "$dockerfile_url" "$dockerfile_dest"
download_file "$docker_compose_url" "$docker_compose_dest"


echo "Folder structure, secrets.env, and configuration files created successfully."