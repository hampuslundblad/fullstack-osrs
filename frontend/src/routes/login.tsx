import { authGithub } from "@/api/auth";
import { GithubIcon } from "@/components/icons/GithubIcon";
import Layout from "@/components/Layout";
import { useTheme } from "@/components/ThemeProvider";
import { Button } from "@/components/ui/button";
import { Card, CardDescription, CardHeader } from "@/components/ui/card";
import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/login")({
  validateSearch: (search: Record<string, unknown>): { redirect: string } => {
    return { redirect: (search.redirect as string) ?? "/" };
  },
  component: RouteComponent,
});

function RouteComponent() {
  const theme = useTheme();
  const { redirect } = Route.useSearch();

  return (
    <Layout>
      <div className="flex justify-center items-center h-full">
        <Card className="w-1/4">
          <CardHeader className="text-center">
            Welcome
            <CardDescription>Log with one of the methods below</CardDescription>
          </CardHeader>
          <div className="flex flex-col gap-4 p-6">
            <Button onClick={() => authGithub(redirect)}>
              <GithubIcon
                height={20}
                width={20}
                theme={theme.theme === "dark" ? "dark" : "light"}
              />
              Github
            </Button>
          </div>
        </Card>
      </div>
    </Layout>
  );
}
