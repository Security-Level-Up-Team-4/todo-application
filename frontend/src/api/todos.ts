import {
  mockTimeline,
  type Todo,
  type TodoTimeline,
} from "../models/todo";
import { apiAuthedFetch } from "./apiUtils";

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in)
async function getTodo(todoId: string): Promise<Todo> {
  const response = await apiAuthedFetch({path: `/api/todos/${todoId}`, method: "GET"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in)
async function getTodoTimeline(todoId: string): Promise<TodoTimeline> {
  // const response = await apiAuthedFetch({path: `/api/todo/timeline?id=${todoId}`, method: "GET"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTimeline;
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in), todo has to be open
async function assignTodo(todoId: string): Promise<Todo> {
  const response = await apiAuthedFetch({path: `/api/todos/assign/${todoId}`, method: "PUT"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in), todo has to be assigned to you
async function unassignTodo(todoId: string): Promise<Todo> {
  const response = await apiAuthedFetch({path: `/api/todos/unassign/${todoId}`, method: "PUT"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in), todo has to be assigned to you
async function closeTodo(todoId: string): Promise<Todo> {
  const response = await apiAuthedFetch({path: `/api/todos/close/${todoId}`, method: "PUT"});
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

// Both todo user and team lead can do
async function createTodo(
  todoName: string,
  todoDescription: string,
  todoPriority: number,
  team: string
): Promise<Todo> {
  const response = await apiAuthedFetch({
    path: `/api/todos/${team}`,
    method: "POST",
    body: JSON.stringify({
      title: todoName,
      description: todoDescription,
      priority: todoPriority,
    }),
  });
  if (!response.ok) {
    const errorText = await response
      .json()
      .then((data) => data.message)
      .catch(() => response.status.toString());
    throw new Error(`Error: ${errorText}`);
  }
  return await response.json();
}

export {
  getTodo,
  getTodoTimeline,
  assignTodo,
  unassignTodo,
  closeTodo,
  createTodo,
};
