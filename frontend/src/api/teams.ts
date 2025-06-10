import { type Team } from "../models/team";
import { apiAuthedFetch } from "./apiUtils";

// Both team lead and todo user can do
async function getTeams(): Promise<Team[]> {
  const response = await apiAuthedFetch({path: "/api/teams", method: "GET"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Both todo user and team lead can do
async function getTeam(teamId: string): Promise<Team> {
  const response = await apiAuthedFetch({path: `/api/teams/${teamId}`, method: "GET"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Only a team lead can do
async function addTeam(teamName: string): Promise<Team> {
  const response = await apiAuthedFetch({path: "/api/teams", method: "POST", body: JSON.stringify({ name: teamName })});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Only a team lead can do
async function addUser(username: string, team: string) {
  const response = await apiAuthedFetch({path: "/api/teammembers", method: "POST", body: JSON.stringify({ Username: username, TeamId: team })});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Only a team lead can do
async function removeUsers(users: number[], team: string) {
  const response = await apiAuthedFetch({path: "/api/teammembers/users", method: "PUT", body: JSON.stringify({ userIds: users, TeamId: team })});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
}

export { getTeams, getTeam, addTeam, addUser, removeUsers };
