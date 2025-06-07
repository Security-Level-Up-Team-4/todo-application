import { mockUsers, type User } from "../models/user";

// const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

async function getUsers(): Promise<User[]> {
  // const response = await fetch(`${apiBaseUrl}/api/users`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(mockUsers);
  return mockUsers;
}

async function updateUserRole(userId: number, newRole: string) {
  // const response = await fetch(`${apiBaseUrl}/api/users/${userId}/role`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ role: newRole }),
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }

  console.log(userId, newRole);
}

export { updateUserRole, getUsers };
