import { apiBaseUrl } from "../config";

type Props = {
  method: string;
  path: string;
  body?: string;
};

export const apiAuthedFetch = async ({
  path,
  method,
  body,
}:
Props): Promise<Response> => {
  const res = await fetch(apiBaseUrl + path, {
    method: method,
    headers: {
      Authorization: `Bearer ${sessionStorage.getItem("token")}`,
      "Content-Type": "application/json",
    },
    body: body,
  });
  return res;
};
