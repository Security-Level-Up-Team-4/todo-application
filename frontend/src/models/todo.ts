export enum TodoStatus {
  OPEN = "Open",
  INPROGRESS = "In Progress",
  CLOSED = "Closed",
}

export interface Todo {
  id: number;
  name: string;
  status: string;
  description: string;
  priority: string;
  createdBy: string;
  assignedTo?: string;
  createdAt: Date;
  updatedAt?: Date;
  closedAt?: Date;
}

export const mockTodos: Todo[] = [
  {
    id: 1,
    name: "Set up project structure",
    status: TodoStatus.CLOSED,
    description: "Initialize repo and base folder layout",
    priority: "low",
    createdBy: "alice",
    assignedTo: "bob",
    createdAt: new Date("2025-06-01T09:00:00Z"),
    updatedAt: new Date("2025-06-01T10:00:00Z"),
    closedAt: new Date("2025-06-01T11:00:00Z"),
  },
  {
    id: 2,
    name: "Design login screen",
    status: TodoStatus.INPROGRESS,
    description: "Create wireframe and UI for login",
    priority: "medium",
    createdBy: "carol",
    assignedTo: "dave",
    createdAt: new Date("2025-06-02T14:30:00Z"),
    updatedAt: new Date("2025-06-03T10:00:00Z"),
  },
  {
    id: 3,
    name: "Implement API endpoints",
    status: TodoStatus.OPEN,
    description: "Create Express routes for authentication",
    priority: "high",
    createdBy: "eve",
    assignedTo: "frank",
    createdAt: new Date("2025-06-03T08:00:00Z"),
  },
  {
    id: 4,
    name: "Test user registration",
    status: TodoStatus.OPEN,
    description: "Write unit tests for signup flow",
    priority: "medium",
    createdBy: "alice",
    assignedTo: "dave",
    createdAt: new Date("2025-06-04T12:00:00Z"),
  },
  {
    id: 5,
    name: "Deploy to staging",
    status: TodoStatus.INPROGRESS,
    description: "Push build to AWS and verify deployment",
    priority: "high",
    createdBy: "carol",
    assignedTo: "bob",
    createdAt: new Date("2025-06-04T15:45:00Z"),
    updatedAt: new Date("2025-06-05T09:20:00Z"),
  },
];

export interface TodoTimeline {
  id: number;
  name: string;
  events: TodoTimelineEvent[];
}

export interface TodoTimelineEvent {
  event: string;
  date: string;
  time: string;
}

export const mockTimeline: TodoTimeline = {
  id: 5,
  name: "Tackle login",
  events: [
    { time: "14:23", date: "02/06/2025", event: "Created" },
    { time: "14:26", date: "02/06/2025", event: "Assigned to User 1" },
    { time: "18:45", date: "03/06/2025", event: "Unassigned from user 2" },
    { time: "19:23", date: "08/07/2025", event: "Assigned to user 3" },
    { time: "19:23", date: "12/07/2025", event: "Closed" },
  ],
};
