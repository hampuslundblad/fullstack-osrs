#!/bin/bash

if [ "$1" != "-token" ] || [ -z "$2" ]; then
  echo "Usage: $0 -token <api_token>"
  exit 1
fi

API_TOKEN=$2

# Update package list and install required packages
if ! apt update && apt install -y certbot python3-certbot-dns-cloudflare; then
  echo "Error: Failed to install required packages."
  exit 1
fi

# Create the directory and set permissions
if ! mkdir -p /etc/cloudflare; then
  echo "Error: Failed to create /etc/cloudflare directory."
  exit 1
fi

if ! chmod 700 /etc/cloudflare; then
  echo "Error: Failed to set permissions for /etc/cloudflare directory."
  exit 1
fi

# Create the file and set permissions
if ! touch /etc/cloudflare/hampuslundblad.com.ini; then
  echo "Error: Failed to create /etc/cloudflare/hampuslundblad.com.ini file."
  exit 1
fi

if ! chmod 600 /etc/cloudflare/hampuslundblad.com.ini; then
  echo "Error: Failed to set permissions for /etc/cloudflare/hampuslundblad.com.ini file."
  exit 1
fi

# Write the API token to the file
if ! echo "dns_cloudflare_api_token = \"$API_TOKEN\"" > /etc/cloudflare/hampuslundblad.com.ini; then
  echo "Error: Failed to write to /etc/cloudflare/hampuslundblad.com.ini file."
  exit 1
fi

echo "API token successfully written to /etc/cloudflare/hampuslundblad.com.ini"