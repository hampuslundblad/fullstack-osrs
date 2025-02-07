import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/error")({
  component: RouteComponent,
});

function RouteComponent() {
  return "Ops! Something went wrong";
}
