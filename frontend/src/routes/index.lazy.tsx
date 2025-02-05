import Layout from "@/components/Layout";
import { Button } from "@/components/ui/button";
import { Separator } from "@radix-ui/react-separator";
import { createLazyFileRoute } from "@tanstack/react-router";
import { useState } from "react";

export const Route = createLazyFileRoute("/")({
  component: RouteComponent,
});

function RouteComponent() {
  const [isLoading, setIsLoading] = useState(false);
  console.log(import.meta.env);

  return (
    <Layout title="Home">
      <div className="flex flex-col gap-4 mt-8">
        <div className="flex gap-4">
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
