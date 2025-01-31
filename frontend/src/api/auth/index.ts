export async function authGithub(returnUrl?: string) {
  window.location.assign(
    `${import.meta.env.VITE_API_URL}/auth/github?returnUrl=${import.meta.env.VITE_BASE_URL}/${returnUrl ?? "/"}`
  );
}
