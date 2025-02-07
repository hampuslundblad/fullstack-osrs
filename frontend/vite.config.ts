import path from "path";
import { defineConfig, loadEnv } from "vite";
import react from "@vitejs/plugin-react-swc";
import svgr from "vite-plugin-svgr";

import { TanStackRouterVite } from "@tanstack/router-plugin/vite";

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd());
  return {
    base: "/",
    plugins: [react(), TanStackRouterVite(), svgr()],
    resolve: {
      alias: {
        "@": path.resolve(__dirname, "./src"),
      },
    },
    server: {
      proxy: {
        "/.api": {
          target: env.VITE_API_URL,
          // rewrite: (path) => path.replace(/^\/\.api/, ""),
          changeOrigin: false,
          secure: false,
          configure: (proxy) => {
            proxy.on("error", (err) => {
              console.log("proxy error", err);
            });
            proxy.on("proxyReq", (req) => {
              console.log(
                "Sending Request to the Target:",
                req.method,
                req.path
              );
            });
            proxy.on("proxyRes", (proxyRes, req) => {
              console.log(
                "Received Response from the Target:",
                proxyRes.statusCode,
                proxyRes.statusMessage,
                req.url
              );
            });
          },
        },
      },
    },
  };
});
