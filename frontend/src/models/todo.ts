export interface Todo {
  id: number;
  name: string;
  status: string;
}

export const mockTodos: Todo[] = [
  { name: "Do the other dialog", id: 0, status: "open" },
  { name: "Finish this page", id: 1, status: "in progress" },
  { name: "Make a PR", id: 2, status: "closed" },
  { name: "Merge the PR", id: 3, status: "open" },
];
