export enum TodoStatus {
  OPEN = "Open",
  INPROGRESS = "In Progress",
  CLOSED = "Closed",
}

export interface Todo {
  id: number;
  title: string;
  status: string;
  description: string;
  priority: string;
  priorityName?: string;
  createdBy: string;
  assignedTo?: string;
  createdAt: Date;
  updatedAt?: Date;
  closedAt?: Date;
}

export const mockTodos: Todo[] = [
  {
    id: 1,
    title: "Set up project structure",
    status: TodoStatus.OPEN,
    description: "Initialize repo and base folder layout",
    priority: "1",
    priorityName: "Low",
    createdBy: "alice",
    createdAt: new Date("2025-06-01T09:00:00Z"),
    updatedAt: new Date("2025-06-01T10:00:00Z"),
    closedAt: new Date("2025-06-01T11:00:00Z"),
  },
  {
    id: 2,
    title: "Design login screen",
    status: TodoStatus.INPROGRESS,
    description: "Create wireframe and UI for login",
    priority: "2",
    priorityName: "Medium",
    createdBy: "carol",
    assignedTo: "dave",
    createdAt: new Date("2025-06-02T14:30:00Z"),
    updatedAt: new Date("2025-06-03T10:00:00Z"),
  },
  {
    id: 3,
    title: "Implement API endpoints",
    status: TodoStatus.OPEN,
    description: "Create Express routes for authentication",
    priority: "3",
    priorityName: "High",
    createdBy: "eve",
    createdAt: new Date("2025-06-03T08:00:00Z"),
  },
  {
    id: 4,
    title: "Test user registration",
    status: TodoStatus.CLOSED,
    description: "Write unit tests for signup flow",
    priority: "4",
    priorityName: "Critical",
    createdBy: "alice",
    assignedTo: "bob",
    createdAt: new Date("2025-06-04T12:00:00Z"),
    closedAt: new Date("2025-06-05T12:00:00Z"),

  },
  {
    id: 5,
    title: "Deploy to staging",
    status: TodoStatus.INPROGRESS,
    description: "Push build to AWS and verify deployment",
    priority: "1",
    priorityName: "Low",
    createdBy: "carol",
    assignedTo: "bob",
    createdAt: new Date("2025-06-04T15:45:00Z"),
    updatedAt: new Date("2025-06-05T09:20:00Z"),
  },
];

export interface TodoTimeline {
  id: number;
  title: string;
  timeline: TodoTimelineEvent[];
}

export interface TodoTimelineEvent {
  event: string;
  createdAt: Date;
}
