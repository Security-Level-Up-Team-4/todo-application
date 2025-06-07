let accessToken: string | null = null;
const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

type Props = {
  method: string;
  path: string;
  body?: string;
  // retry?: boolean;
};

export const apiAuthedFetch = async ({
  path,
  method,
  body,
  // retry = true,
}: Props): Promise<Response> => {
  const res = await fetch(apiBaseUrl + path, {
    method: method,
    headers: {
      Authorization: `Bearer ${accessToken}`,
      "Content-Type": "application/json",
    },
    // credentials: "include",
    body: body,
  });

  // if (res.status === 401 && retry) {
  //   const refreshed = await refreshAccessToken();
  //   if (refreshed) {
  //     return apiAuthedFetch({
  //       path: path,
  //       method: method,
  //       retry: false,
  //       body: body,
  //     });
  //   }
  // }

  return res;
};

export const login = async(username: string, password: string) => {
  const response = await fetch(`${apiBaseUrl}/api/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    });
  
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const responseAccessToken = await response.json();
    localStorage.setItem("username", username)
    localStorage.setItem("token", responseAccessToken?.token)
    // set role as well
    accessToken = responseAccessToken?.token;
}

// const refreshAccessToken = async (): Promise<boolean> => {
//   try {
//     const res = await fetch(`${apiBaseUrl}/api/auth/refresh`, {
//       method: "POST",
//       credentials: "include",
//     });

//     if (!res.ok) return false;

//     const data = await res.json();
//     setAccessToken(data.accessToken);
//     return true;
//   } catch {
//     return false;
//   }
// };
