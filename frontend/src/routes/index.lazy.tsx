import PageError from "@/components/PageError";
import Layout from "@/components/Layout";
import { Button } from "@/components/ui/button";
import Title from "@/components/ui/title";
import { Separator } from "@radix-ui/react-separator";
import { createLazyFileRoute } from "@tanstack/react-router";
import { useState } from "react";

export const Route = createLazyFileRoute("/")({
  component: RouteComponent,
  errorComponent: PageError,
});

function RouteComponent() {
  const [isLoading, setIsLoading] = useState(false);
  throw new Error();
  return (
    <Layout title="Home">
      <div className="flex flex-col gap-4 mt-8">
        <div className="flex gap-4">
          <Title> Test 5</Title>
          <Button
            className="self-start"
            variant={"default"}
            isLoading={isLoading}
          >
            {isLoading ? "loading" : "not loading"}
          </Button>
          <Button
            className="self-start"
            onClick={() => setIsLoading(!isLoading)}
          >
            Toggle loading
          </Button>
        </div>
        <Separator />
      </div>
    </Layout>
  );
}
