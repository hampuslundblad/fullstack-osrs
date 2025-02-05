import { authGithub } from "@/api/auth";
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
          authGithub();
          throw redirect({
            to: "/login",
            search: {
              redirect: location.pathname,
            },
          });
        }
        throw redirect({
          to: "/",
        });
      }
      throw new Error("An error occurred");
    }
  },
  errorComponent: (error) => <div>Error {`${error.error}`} </div>,
});

function RouteComponent() {
  return (
    <>
      <Outlet />
      <LockIcon />
    </>
  );
}
