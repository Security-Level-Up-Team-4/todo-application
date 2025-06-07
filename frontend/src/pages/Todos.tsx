import { useLocation, useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import AddUserDialog from "../components/dialogs/AddUserDialog";
import { useState } from "react";
import { mockTodos } from "../models/todo";
import CreateTodoDialog from "../components/dialogs/CreateTodoDialog";
import RemoveUsersDialog from "../components/dialogs/RemoveUsersDialog";
import { mockUsers } from "../models/user";

function Todos() {
  const navigate = useNavigate();

  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const team = queryParams.get("team");
  const [isAddUserDialogOpen, setIsAddUserDialogOpen] = useState(false);
  const [isCreateTodoDialogOpen, setIsCreateTodoDialogOpen] = useState(false);
  const [isRemoveUsersDialogOpen, setIsRemoveUsersDialogOpen] = useState(false);

  const [statusFilter, setStatusFilter] = useState("all");

  const handleAddUser = (username: string) => {
    console.log("Adding user:", username);
  };

  const handleCreateTodo = (todoName: string) => {
    console.log("Created TOdo:", todoName);
  };

  const handleRemoveUsers = (users: number[]) => {
    console.log("Remove users:", users);
  };
  return (
    <>
      <Navbar />
      <main className="flex flex-col gap-8 p-4 w-full">
        <h1 className="w-full pt-4 text-center font-bold text-2xl">
          Vodacom's To-Do list
        </h1>
        <section className="flex flex-wrap gap-2">
          <select
            className="border border-gray-300 rounded px-4 pr-4 py-2 max-w-36 w-full cursor-pointer"
            defaultValue="in-progress"
            onChange={(e) => setStatusFilter(e.target.value)}
          >
            <option value="all">All</option>

            <option value="open">Open</option>
            <option value="in progress">In Progress</option>
            <option value="closed">Closed</option>
          </select>
          <button
            className="px-4 py-2 border max-w-36 w-full hover:bg-gray-200 cursor-pointer"
            onClick={() => setIsCreateTodoDialogOpen(true)}
          >
            New todo
          </button>
          <button
            className="px-4 py-2 border max-w-36 w-full hover:bg-gray-200 cursor-pointer"
            onClick={() => setIsAddUserDialogOpen(true)}
          >
            Add user
          </button>
          <button
            className="px-4 py-2 border max-w-36 w-full hover:bg-gray-200 cursor-pointer"
            onClick={() => setIsRemoveUsersDialogOpen(true)}
          >
            Remove users
          </button>
        </section>
        <section className="flex flex-col gap-2">
          {mockTodos
            .filter((todo) =>
              statusFilter === "all" ? true : todo.status === statusFilter
            )
            .map((todo, index) => (
              <button
                key={index}
                onClick={() => navigate(`/todo?id=${todo.id}`)}
                className="w-full p-4 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer"
              >
                {todo.name} - {todo.status}
              </button>
            ))}
        </section>
      </main>
      <AddUserDialog
        isOpen={isAddUserDialogOpen}
        onClose={() => setIsAddUserDialogOpen(false)}
        onAddUser={handleAddUser}
      />
      <CreateTodoDialog
        isOpen={isCreateTodoDialogOpen}
        onClose={() => setIsCreateTodoDialogOpen(false)}
        onCreateTodo={handleCreateTodo}
      />
      <RemoveUsersDialog
        isOpen={isRemoveUsersDialogOpen}
        onClose={() => setIsRemoveUsersDialogOpen(false)}
        users={mockUsers}
        onRemoveUsers={handleRemoveUsers}
      />
    </>
  );
}

export default Todos;
