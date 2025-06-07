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
