#!/bin/bash

# Function to display usage
usage() {
    echo "Usage: $0 --token <access_token> --username <username>"
    exit 1
}

# Check if the correct number of arguments is provided
if [ "$#" -ne 4 ]; then
    usage
fi

# Parse the arguments
while [ "$#" -gt 0 ]; do
    case "$1" in
        --token)
            ACCESS_TOKEN="$2"
            shift 2
            ;;
        --username)
            USERNAME="$2"
            shift 2
            ;;
        *)
            usage
            ;;
    esac
done

# Check if the access token and username are provided
if [ -z "$ACCESS_TOKEN" ] || [ -z "$USERNAME" ]; then
    usage
fi

# Set the access token as an environment variable
export GHCR_TOKEN="$ACCESS_TOKEN"

# Login to ghcr.io using the access token and username
docker login ghcr.io -u "$USERNAME" --password-stdin

# Unset the environment variable for security reasons accroding to github copilot
unset GHCR_TOKEN

