import { mockTeams, type Team } from "../models/team";

// const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080"

async function getTeams(): Promise<Team[]> {
  // const response = await fetch(`${apiBaseUrl}/api/teams`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
  return mockTeams;
}

async function addTeam(teamName: string): Promise<Team> {
  // const response = await fetch(`${apiBaseUrl}/api/teams`, {
  //   method: "POST",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ name: teamName }),
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(teamName);
  return mockTeams[0];
}

async function addUser(username: string, team: string) {
  // const response = await fetch(`${apiBaseUrl}/api/teams/users`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ username }),
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
  console.log(username, team);
}

async function removeUsers(users: number[], team: string) {
  // const response = await fetch(`${apiBaseUrl}/api/teams/users`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ username }),
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
  console.log(users, team);
}

export { getTeams, addTeam, addUser, removeUsers };
