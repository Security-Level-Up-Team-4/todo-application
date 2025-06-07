import { mockTeams, type Team } from "../models/team";

async function getTeams(): Promise<Team[]> {
  const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo/timeline?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  return mockTeams;

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function postTeam(
    teamName:string
): Promise<Team[]> {
  const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  const response = await fetch(`${apiBaseUrl}/api/teams`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ name: teamName }),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }
  return await response.json();
}

export { getTeams, postTeam };
