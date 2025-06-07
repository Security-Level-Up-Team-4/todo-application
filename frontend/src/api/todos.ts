import {
  mockTimeline,
  mockTodos,
  type Todo,
  type TodoTimeline,
} from "../models/todo";

async function getTodo(todoId: string): Promise<Todo> {
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  const idNumber = todoId ? Number(todoId) : 0;
  return mockTodos[idNumber];

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function getTodos(teamId: string): Promise<Todo[]> {
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });
  console.log(teamId);
  return mockTodos;

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function getTodoTimeline(todoId: string): Promise<TodoTimeline> {
  console.log(todoId);
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

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

async function assignTodo(todoId: string): Promise<Todo> {
  console.log(todoId);
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo/unassign?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  const idNumber = todoId ? Number(todoId) : 0;
  return mockTodos[idNumber];

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function unassignTodo(todoId: string): Promise<Todo> {
  console.log(todoId);
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo/unassign?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  const idNumber = todoId ? Number(todoId) : 0;
  return mockTodos[idNumber];

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function closeTodo(todoId: string): Promise<Todo> {
  console.log(todoId);
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

  // const response = await fetch(`${apiBaseUrl}/api/todo/close?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  const idNumber = todoId ? Number(todoId) : 0;
  return mockTodos[idNumber];

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();
}

async function createTodo(
  todoName: string,
  todoDescription: string,
  todoPriority: number,
  team: string
): Promise<Todo[]> {
  // const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

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
  console.log("Todo Name:", todoName);
  console.log("Description:", todoDescription);
  console.log("Priority:", todoPriority);
  console.log("Team:", team);
  return mockTodos;
}

export {
  getTodo,
  getTodos,
  getTodoTimeline,
  assignTodo,
  unassignTodo,
  closeTodo,
  createTodo,
};
