const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

async function register(username: string, email: string, password: string) {
  const response = await fetch(`${apiBaseUrl}/api/auth/signup`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ username, email, password }),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }
  return;
}

export { register };
