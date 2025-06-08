export interface User {
  id: number;
  username: string;
  email?: string;
  roleName?: string;
  roleId?: number;
}

export enum UserRoles {
  ADMIN = "access_admin",
  TEAMLEAD = "team_lead",
  TODO = "todo_user",
}

export const mockUsers: User[] = [
  { id: 0, username: "Ryan", email:"ryan@bbd.co.za", roleName: UserRoles.ADMIN, roleId: 1 },
  { id: 1, username: "Tshepo", email:"tshepo@bbd.co.za", roleName: UserRoles.TEAMLEAD, roleId: 2 },
  { id: 2, username: "Alfred", email:"alfred@bbd.co.za", roleName: UserRoles.TODO, roleId: 3 },
  { id: 3, username: "Vuyo", email:"vuyo@bbd.co.za", roleName: UserRoles.TODO, roleId: 3 },
  { id: 4, username: "Rorisang", email:"rorisang@bbd.co.za", roleName: UserRoles.TEAMLEAD, roleId: 2 },
];
