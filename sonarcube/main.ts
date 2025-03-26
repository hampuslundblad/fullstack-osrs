import { Select } from "https://deno.land/x/cliffy@v0.25.7/prompt/mod.ts";

const SONARCUBE_DOCKER_SERVER = "http://sonarqube:9000";

const projects = [
  {
    displayName: "Frontend (Node based)",
    path: "/frontend",
    projectName: "osrs-app",
  },
  {
    displayName: "Backend (C# based)",
    path: "/backend/DotnetComp",
    projectName: "osrs-api",
  },
];

if (!Deno.cwd().endsWith("sonarcube")) {
  console.log("This command must be run from the 'sonarcube' directory.");
  Deno.exit(1);
}

// Choose project
const selectedProject = await Select.prompt({
  message: "Choose a project",
  options: projects.map((project) => project.displayName),
});

const project = projects.find((p) => p.displayName === selectedProject);

// Prompt for token
console.log("Remember that the token must match the chosen project.");
const token = prompt("Input token: ");

token?.trim();

if (project && token) {
  // Absolute path to /fullstack-osrs/sonarcube
  const sonarcubeDir = await Deno.realPath(Deno.cwd()); // Resolve absolute path

  // Absolute path to the project directory
  const projectPath = `${sonarcubeDir.replace(/\/sonarcube$/, "")}/${
    project.path
  }`;

  // Check if project directory exists
  try {
    await Deno.stat(projectPath);
  } catch {
    console.log(`The project directory '${projectPath}' does not exist.`);
    Deno.exit(1);
  }

  // Setup the scanner commands, looks different for node and dotnet
  // since they're using two different scanners
  const baseCommand = [
    "docker",
    "run",
    "--rm",
    "--network",
    "sonarcube_default",
  ];

  const nodeScannerCommand = baseCommand.concat([
    "-e",
    `SONAR_HOST_URL=http://sonarqube:9000`,
    "-e",
    `SONAR_TOKEN=${token}`,
    "-v",
    `${sonarcubeDir}/osrs-app:/usr/src`, // Mount the config file
    "-v",
    `${projectPath}:/usr/src/frontend`,
    "sonarsource/sonar-scanner-cli",
  ]);

  const dotnetScanner = baseCommand.concat([
    "-v",
    `${projectPath}:/usr/src/backend`,
    "sonarcube_backend",
    "sh",
    "-c", // Use a shell to execute multiple commands
    `
      dotnet sonarscanner begin /k:${project.projectName} /d:sonar.host.url=${SONARCUBE_DOCKER_SERVER} /d:sonar.login=${token} &&
      dotnet build &&
      dotnet sonarscanner end /d:sonar.login=${token}
    `,
  ]);

  const scannerCommand =
    project.displayName === "Frontend (Node based)"
      ? nodeScannerCommand
      : dotnetScanner;

  new Deno.Command(scannerCommand[0], {
    args: scannerCommand.slice(1),
  }).spawn();
} else {
  console.log("Invalid project or token");
  Deno.exit(1);
}
