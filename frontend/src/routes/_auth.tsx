import { fetchUser } from "@/api/user";
import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";
import { AxiosError } from "axios";
import { LockIcon } from "lucide-react";

export const Route = createFileRoute("/_auth")({
  component: RouteComponent,
  loader: async () => {
    try {
      const userData = await fetchUser();
      return userData;
    } catch (error) {
      console.error(error);
      if (error instanceof AxiosError) {
        if (error.status === 401) {
          throw redirect({
            to: "/login",
            search: {
              redirect: location.pathname,
            },
          });
        }
        // Error other than 401
        throw redirect({
          to: "/error",
        });
      }
      throw new Error("An error occurred");
    }
  },
});

function RouteComponent() {
  return (
    <>
      <Outlet />
      <LockIcon />
    </>
  );
}
