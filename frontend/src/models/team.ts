import type { Todo } from "./todo";
import type { User } from "./user";

export interface Team {
  teamId: number;
  teamName: string;
  todos?: Todo[];
  users?: User[];
}

export const mockTeams: Team[] = [
  { teamName: "SPW", teamId: 0 },
  { teamName: "Vodacom", teamId: 1 },
  { teamName: "MTN", teamId: 2 },
  { teamName: "CellC", teamId: 3 },
];