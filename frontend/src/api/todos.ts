import {
  mockTimeline,
  mockTodos,
  type Todo,
  type TodoTimeline,
} from "../models/todo";

// const apiBaseUrl = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

async function getTodo(todoId: string): Promise<Todo> {
  // const response = await fetch(`${apiBaseUrl}/api/todos?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

async function getTodos(teamId: string): Promise<Todo[]> {
  // const response = await fetch(`${apiBaseUrl}/api/todos?id=${teamId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(teamId);
  return mockTodos;
}

async function getTodoTimeline(todoId: string): Promise<TodoTimeline> {
  // const response = await fetch(`${apiBaseUrl}/api/todo/timeline?id=${todoId}`, {
  //   method: "GET",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTimeline;
}

async function assignTodo(todoId: string): Promise<Todo> {
  // const response = await fetch(`${apiBaseUrl}/api/todo/assign?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

async function unassignTodo(todoId: string): Promise<Todo> {
  // const response = await fetch(`${apiBaseUrl}/api/todo/unassign?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

async function closeTodo(todoId: string): Promise<Todo> {
  // const response = await fetch(`${apiBaseUrl}/api/todo/close?id=${todoId}`, {
  //   method: "PUT",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // });

  // if (!response.ok) {
  //   throw new Error(`HTTP error! Status: ${response.status}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

async function createTodo(
  todoName: string,
  todoDescription: string,
  todoPriority: number,
  team: string
): Promise<Todo> {
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
  return mockTodos[0];
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
