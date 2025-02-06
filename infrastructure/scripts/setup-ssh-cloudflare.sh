#!/bin/bash

if [ "$1" != "-token" ] || [ -z "$2" ]; then
  echo "Usage: $0 -token <api_token>"
  exit 1
fi

API_TOKEN=$2

apt update && apt install -y certbot python3-certbot-dns-cloudflare

mkdir -p /etc/cloudflare
chmod 700 /etc/cloudflare
touch /etc/cloudflare/hampuslundblad.com.ini
chmod 600 /etc/cloudflare/hampuslundblad.com.ini

echo "dns_cloudflare_api_token = \"$API_TOKEN\"" > /etc/cloudflare/hampuslundblad.com.ini


echo "API token successfully written to /etc/cloudflare/hampuslundblad.com.ini"