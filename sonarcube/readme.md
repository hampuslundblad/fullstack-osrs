# Prerequisites

- Docker installed
- Deno installed [See installation](https://docs.deno.com/runtime/getting_started/installation/)

# Start 

Start the sonarcube server

```bash
docker-compose up
```

Go to  `localhost:9000` and grab a token for the chosen project. 


and then start the cli, follow the instructions.

```bash
deno task sonarcube
```


# Building the .NET dockerfile.

You'll need to build it from the parent (fullstack-osrs) directory in order to properly include the csproj file.

```bash
docker build . -t sonarcube_backend -f /sonarcube/dockerfile
```

# Troubleshooting

If the sonarcube server exits due to elasticsearch error 137 you most likely need to increase the docker memory size.

```bash
colima start --cpu 2 --memory 8 --disk 10 
```

This starts colima with a ram size of 8gb and disk space of 10gb. 
