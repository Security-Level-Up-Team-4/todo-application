import { mockTeams, type Team } from "../models/team";
// import { apiAuthedFetch } from "./apiUtils";

// Both team lead and todo user can do
async function getTeams(): Promise<Team[]> {
  // const response = await apiAuthedFetch({path: "/api/teams", method: "GET"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();
  return mockTeams;
}

// Only a team lead can do
async function addTeam(teamName: string): Promise<Team> {
  // const response = await apiAuthedFetch({path: "/api/teams", method: "POST", body: JSON.stringify({ name: teamName })});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();
  console.log(teamName);
  return mockTeams[0];
}

// Only a team lead can do
async function addUser(username: string, team: string) {
  // const response = await apiAuthedFetch({path: "/api/teams/users", method: "PUT", body: JSON.stringify({ username, teamId: team })});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();
  console.log(username, team);
}

// Only a team lead can do
async function removeUsers(users: number[], team: string) {
  // const response = await apiAuthedFetch({path: "/api/teams/users", method: "PUT", body: JSON.stringify({ users, teamId: team })});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();
  console.log(users, team);
}

export { getTeams, addTeam, addUser, removeUsers };
