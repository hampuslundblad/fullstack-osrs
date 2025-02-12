# How tf do i start this.

### Building

```bash
dotnet build
```

### Running

```bash
dotnet run
```

### Docker

Build

```bash
docker build . -t <container name>
```

Run

```bash
docker run -it -p <your port>:8080 <container name>
```

### VS Code settings

If there's any issues with the C# dev kit languager server then add this in `.vscode/settings.json`

```bash
{
  "dotnet.dotnetPath": "pathtodotnet"
}

```

### Notes

Initial db setup

```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
