import { type User } from "../models/user";
import { apiAuthedFetch } from "./apiUtils";

// Only admin and team lead can do (Maybe return less fields for team leads?)
async function getUsers(): Promise<User[]> {
  const response = await apiAuthedFetch({path: `/api/users`, method: "GET"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Only admins can do
async function updateUserRole(userId: number, newRole: string) {
  const response = await apiAuthedFetch({path: `/api/users/${userId}`, method: "PUT", body: JSON.stringify({ RoleName: newRole })});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  console.log(userId, newRole);
}

export { updateUserRole, getUsers };
