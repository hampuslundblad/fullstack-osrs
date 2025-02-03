export async function authGithub(returnUrl?: string) {
  window.location.assign(
    `${import.meta.env.VITE_API_URL}/auth/github?returnUrl=${window.location.origin}/${returnUrl ?? "/"}`
  );
}
