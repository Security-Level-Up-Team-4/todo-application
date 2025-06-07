export interface User {
  id: number;
  username: string;
  email?: string;
  role?: string;
}

export enum UserRoles {
  ADMIN = "Admin",
  TEAMLEAD = "Teamlead",
  TODO = "Todo",
}

export const mockUsers: User[] = [
  { id: 0, username: "Ryan", email:"ryan@bbd.co.za", role: UserRoles.ADMIN },
  { id: 1, username: "Tshepo", email:"tshepo@bbd.co.za", role: UserRoles.TEAMLEAD },
  { id: 2, username: "Alfred", email:"alfred@bbd.co.za", role: UserRoles.TODO },
  { id: 3, username: "Vuyo", email:"vuyo@bbd.co.za", role: UserRoles.TODO },
  { id: 4, username: "Rorisang", email:"rorisang@bbd.co.za", role: UserRoles.TEAMLEAD },
];
