#!/bin/bash

# Constants
DOMAIN="hampuslundblad.com"
CERTBOT_DIR="/etc/certbot"
CREDENTIALS_FILE="$CERTBOT_DIR/credentials"

# Check if the script is run as root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  exit 1
fi

# Check if the correct arguments are provided
if [ "$1" != "-token" ] || [ -z "$2" ]; then
  echo "Usage: $0 -token <api_token>"
  exit 1
fi

API_TOKEN=$2


apt update && apt install -y certbot python3-certbot-dns-cloudflare
mkdir -p $CERTBOT_DIR
touch $CREDENTIALS_FILE
echo "dns_cloudflare_api_token = \"$API_TOKEN\"" > $CREDENTIALS_FILE
chmod 600 $CREDENTIALS_FILE

echo "DNS Cloudflare credentials set up successfully at $CREDENTIALS_FILE"


sudo certbot certonly --dns-cloudflare   --dns-cloudflare-credentials /etc/certbot/credentials   -d hampuslundblad.co m
