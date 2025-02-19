import { createRouter } from "@tanstack/react-router";

import { routeTree } from "./routeTree.gen";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import PageNotFound from "./components/PageNotFound";
import PageError from "./components/PageError";
// Register the router instance for type safety
declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

const queryClient = new QueryClient();

export const router = createRouter({
  routeTree,
  defaultNotFoundComponent: PageNotFound,
  defaultErrorComponent: PageError,
  context: {
    queryClient,
  },

  // Optionally, we can use `Wrap` to wrap our router in the loader client provider
  Wrap: ({ children }) => {
    return (
      <QueryClientProvider client={queryClient}>{children}</QueryClientProvider>
    );
  },
});
