# SimpleDocker

Build the image:

```
docker build --rm -f "Dockerfile" -t simpledocker:latest .
```

Run the container:

```
docker run -d -p 8080:80 simpledocker
```
