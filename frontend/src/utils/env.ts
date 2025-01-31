export const isLocalHost = (): boolean => {
  return window.location.hostname.includes("localhost");
};
