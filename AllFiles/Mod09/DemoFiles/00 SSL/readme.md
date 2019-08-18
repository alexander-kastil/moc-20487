# Create trusted Cert

Install [Git Bash bundle](https://git-scm.com/download/win)

```
git clone https://github.com/RubenVermeulen/generate-trusted-ssl-certificate.git
cd generate-trusted-ssl-certificate
bash generate.sh
```

Copy `server.crt` & `server.key` to folder `/ssl`

Update angular.json

```
"serve": {
    "builder": "@angular-devkit/build-angular:dev-server",
    "options": {
        "browserTarget": "smartSammler:build",
        "ssl": true,
        "sslCert": "./ssl/server.crt",
        "sslKey": "./ssl/server.key"
}
```

[Guide to setup a Cert](https://medium.com/@rubenvermeulen/running-angular-cli-over-https-with-a-trusted-certificate-4a0d5f92747a)