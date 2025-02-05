# Setup

```bash
sudo apt update
sudo apt install cerbot
```

# Obtain the SSL certificate

```bash
sudo certbot certonly --webroot --webroot-path /home/hampus/fullstack-osrs  -d hampuslundblad.com
```

- y
- 2

```bash
sudo mkdir -p /etc/letsencrypt/ssl
sudo cp -r -L /etc/letsencrypt/live/hampuslundblad.com/fullchain.pem /etc/letsencrypt/ssl/
sudo cp -r -L /etc/letsencrypt/live/hampuslundblad.com/privkey.pem /etc/letsencrypt/ssl/
```

certonly: This option tells Certbot only to obtain the certificate, and you will do the manual installation.

— webroot: The webroot plugin requires that you specify a directory on your server where Certbot can place a temporary file to prove that you have control over the domain you request a certificate for.

-— webroot-path: This specifies the directory where Certbot should place the temporary file.

-d: This option specifies the domain or subdomain you want to obtain a certificate.
