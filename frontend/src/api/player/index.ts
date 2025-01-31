import { apiClient } from "../axios";

type Player = {
  name: string;
  experienceGainedLast24H: number;
  experienceGainedLastWeek: number;
  totalExperience: number;
};

export async function fetchPlayer(name: string): Promise<Player> {
  const response = await apiClient.get(`player/${name}`);
  return response.data;
}
