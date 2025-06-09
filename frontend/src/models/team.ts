import type { Todo } from "./todo";
import type { User } from "./user";

export interface Team {
  id: number;
  name: string;
  todos?: Todo[];
  users?: User[];
}

export const mockTeams: Team[] = [
  { name: "SPW", id: 0 },
  { name: "Vodacom", id: 1 },
  { name: "MTN", id: 2 },
  { name: "CellC", id: 3 },
];