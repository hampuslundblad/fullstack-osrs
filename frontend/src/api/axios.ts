import axios from "axios";

console.log(window.location.origin);
export const apiClient = axios.create({
  baseURL: `${window.location.origin}/.api/`,
});
