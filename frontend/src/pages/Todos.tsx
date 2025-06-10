import { useLocation, useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import AddUserDialog from "../components/dialogs/AddUserDialog";
import { useEffect, useState } from "react";
import { TodoStatus, type Todo } from "../models/todo";
import CreateTodoDialog from "../components/dialogs/CreateTodoDialog";
import RemoveUsersDialog from "../components/dialogs/RemoveUsersDialog";
import { type User } from "../models/user";
import { addUser, getTeam, removeUsers } from "../api/teams";
import { createTodo } from "../api/todos";
import Loader from "../components/Loader";
import ErrorDialog from "../components/dialogs/ErrorDialog";
import ErrorPage from "../components/ErrorPage";

function Todos() {
  const navigate = useNavigate();

  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const team = queryParams.get("team");
  const [isAddUserDialogOpen, setIsAddUserDialogOpen] = useState(false);
  const [isCreateTodoDialogOpen, setIsCreateTodoDialogOpen] = useState(false);
  const [isRemoveUsersDialogOpen, setIsRemoveUsersDialogOpen] = useState(false);

  const [statusFilter, setStatusFilter] = useState("all");

  const [teamName, setTeamName] = useState<string>("");
  const [todos, setTodos] = useState<Todo[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [teamCreator, setTeamCreator] = useState<string | undefined>(undefined);

  const [loading, setLoading] = useState(true);
  const [errorPageMessage, setErrorPageMessage] = useState("");
  const [errorDialogMessage, setErrorDialogMessage] = useState("");

  useEffect(() => {
    setLoading(true);
    const fetchTeam = async () => {
      try {
        const data = await getTeam(team ?? "0");
        setTodos(data?.todos ?? []);
        setUsers(data?.users ?? []);
        setTeamName(data.teamName);
        setTeamCreator(data?.creator);
      } catch (error) {
        setErrorPageMessage(
          error instanceof Error ? error.message : "An unknown error occurred"
        );
      } finally {
        setLoading(false);
      }
    };
    fetchTeam();
  }, [team]);

  const handleAddUser = async (username: string) => {
    try {
      await addUser(username, team ?? "0");
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };

  const handleCreateTodo = async (
    todoName: string,
    todoDescription: string,
    todoPriority: number
  ) => {
    try {
      const data = await createTodo(
        todoName,
        todoDescription,
        todoPriority,
        team ?? "0"
      );
      setTodos([...todos, data]);
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };

  const handleRemoveUsers = async (users: number[]) => {
    try {
      await removeUsers(users, team ?? "0");
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };
  return (
    <>
      <Navbar />
      {loading ? (
        <Loader />
      ) : errorPageMessage ? (
        <ErrorPage
          errorMessage={errorPageMessage}
          errorTitle="An error has occurred"
        />
      ) : (
        <main className="flex flex-col gap-8 p-4 w-full">
          <h1 className="w-full pt-4 text-center font-bold text-2xl">
            {teamName}
          </h1>
          <section className="flex flex-wrap gap-2">
            <select
              className="border border-gray-300 rounded px-4 pr-4 py-2 max-w-36 w-full cursor-pointer"
              defaultValue="in-progress"
              onChange={(e) => setStatusFilter(e.target.value)}
            >
              <option value="all">All</option>
              <option value={TodoStatus.OPEN}>Open</option>
              <option value={TodoStatus.INPROGRESS}>In Progress</option>
              <option value={TodoStatus.CLOSED}>Closed</option>
            </select>
            <button
              className="px-4 py-2 border max-w-36 w-full hover:bg-gray-200 cursor-pointer"
              onClick={() => setIsCreateTodoDialogOpen(true)}
            >
              New todo
            </button>
            {sessionStorage.getItem("username") === teamCreator && (
              <>
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
              </>
            )}
          </section>
          <section className="flex flex-col gap-2">
            {todos
              .filter((todo) =>
                statusFilter === "all" ? true : todo.status === statusFilter
              )
              .map((todo, index) => (
                <button
                  key={index}
                  onClick={() => navigate(`/todo?id=${todo.id}`)}
                  className="w-full p-4 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer"
                >
                  {todo.title} - {todo.status}
                </button>
              ))}
          </section>
        </main>
      )}
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
        users={users}
        onRemoveUsers={handleRemoveUsers}
      />
      <ErrorDialog
        errorMessage={errorDialogMessage}
        isOpen={errorDialogMessage !== ""}
        onClose={() => setErrorDialogMessage("")}
      />
    </>
  );
}

export default Todos;
