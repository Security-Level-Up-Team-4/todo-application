import {
  mockTimeline,
  mockTodos,
  type Todo,
  type TodoTimeline,
} from "../models/todo";
// import { apiAuthedFetch } from "../utils/api";

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in)
async function getTodo(todoId: string): Promise<Todo> {
  // const response = await apiAuthedFetch({path: `api/todos?id=${todoId}`, method: "GET"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos.find(todo => todo.id === Number(todoId)) ?? mockTodos[0];
;
}

// Both todo user and team lead can do
async function getTodos(teamId: string): Promise<Todo[]> {
  // const response = await apiAuthedFetch({path: `api/todos?id=${teamId}`, method: "GET"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(teamId);
  return mockTodos;
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
  // const response = await apiAuthedFetch({path: `/api/todo/assign?id=${todoId}`, method: "PUT"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in), todo has to be assigned to you
async function unassignTodo(todoId: string): Promise<Todo> {
  // const response = await apiAuthedFetch({path: `/api/todo/unassign?id=${todoId}`, method: "PUT"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

// Both todo user and team lead can do (Have to be a member of the team that the todo belongs in), todo has to be assigned to you
async function closeTodo(todoId: string): Promise<Todo> {
  // const response = await apiAuthedFetch({path: `/api/todo/close?id=${todoId}`, method: "PUT"});
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
  // }
  // return await response.json();

  console.log(todoId);
  return mockTodos[todoId ? Number(todoId) : 0];
}

// Both todo user and team lead can do
async function createTodo(
  todoName: string,
  todoDescription: string,
  todoPriority: number,
  team: string
): Promise<Todo> {
  // const response = await apiAuthedFetch({
  //   path: `/api/teams/todo?teamId={team}`,
  //   method: "POST",
  //   body: JSON.stringify({
  //     name: todoName,
  //     description: todoDescription,
  //     priority: todoPriority,
  //   }),
  // });
  // if (!response.ok) {
  //   const errorText = await response
  //     .json()
  //     .then((data) => data.message)
  //     .catch(() => response.status.toString());
  //   throw new Error(`Error: ${errorText}`);
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
