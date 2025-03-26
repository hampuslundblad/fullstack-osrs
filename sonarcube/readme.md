# Prerequisites

- Docker installed
- Deno installed [See installation](https://docs.deno.com/runtime/getting_started/installation/)

# Start
```bash
docker-compose up
```

and then start the cli

```bash
deno task sonarcube
```


# Building the dockerfile

You'll need to build it from the parent directory in order to properly include the csproj file.

# Troubleshooting

If the sonarcube server exits due to elasticsearch error 137 you most likely need to increase the docker memory size.

```bash
colima start --cpu 2 --memory 8 --disk 10 
```

This start colima with a ram size of 8gb and disk space of 10gb. 
