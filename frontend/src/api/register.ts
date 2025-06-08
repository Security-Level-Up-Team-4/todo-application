import { apiBaseUrl } from "../config";

async function register(username: string, email: string, password: string) {

  const response = await fetch(`${apiBaseUrl}/api/auth/signup`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username, email, password }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error: ${errorText ? errorText : response.status}`);
  }
  return await response.json();
}

export { register };
