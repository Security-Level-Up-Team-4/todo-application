import { mockTimeline, type TodoTimeline } from "../models/todo";

async function getTodoTimeline(todoId: string): Promise<TodoTimeline> {
  const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo/timeline?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  return mockTimeline;

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}
export { getTodoTimeline };
