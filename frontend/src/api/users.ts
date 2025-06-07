import { mockUsers, type User } from "../models/user";

async function getUsers(): Promise<User[]> {
  const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/users`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  console.log(mockUsers)
  return mockUsers;

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function updateUserRole(
  userId: number,
  newRole: string
): Promise<User[]> {
  const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  const response = await fetch(`${apiBaseUrl}/api/users/${userId}/role`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ role: newRole }),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }
  return await response.json();
}

export { updateUserRole };
export { getUsers };
