import { apiClient } from "../axios";

type User = {
  name: string;
  groups: Group[];
};

export type Group = {
  groupName: string;
  players: Player[];
};

export type PlayerExperience = {
  experience: number,
  dateTime: Date
}

export type Player = {
  playerName: string;
  totalExperience: number;
  experienceGainedLast24H: number;
  experienceGainedLastWeek: number;
  totalLevel: number;
  experienceOverTime?: PlayerExperience[];
};

export async function fetchUser(): Promise<User> {
  const response = await apiClient.get(`user`);
  return response.data;
}

export async function fetchGroup(groupName: string): Promise<Group> {
  const response = await apiClient.get(`user/group/${groupName}`);
  return response.data;
}

export async function createGroupOnUser(groupName: string): Promise<void> {
  await apiClient.put(`user/group/${groupName}`, {
    groupName,
  });
}

export async function addPlayerToGroup(
  groupName: string,
  playerName: string
): Promise<void> {
  await apiClient.put(`user/group/${groupName}/player/${playerName}`);
}

export async function removePlayerFromGroup(
  groupName: string,
  playerName: string
): Promise<void> {
  await apiClient.delete(`user/group/${groupName}/player/${playerName}`);
}

export async function syncGroup(groupName: string): Promise<void> {
  await apiClient.put(`user/group/${groupName}/sync`);
}