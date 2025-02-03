import axios from "axios";

console.log(window.location.origin);
export const apiClient = axios.create({
  //baseURL: ".api/",
  baseURL: `${window.location.origin}/.api/`,
});
