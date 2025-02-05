export async function authGithub(returnUrl?: string) {
  const baseUrl = import.meta.env.PROD
    ? window.location.origin + "/.api"
    : import.meta.env.VITE_API_URL;

  window.location.assign(
    `${baseUrl}/auth/github?returnUrl=${window.location.origin}/${returnUrl ?? "/"}`
  );
}
