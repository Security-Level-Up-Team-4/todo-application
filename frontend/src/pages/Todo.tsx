import { useLocation, useNavigate } from "react-router-dom";
import Navbar from "../components/Navbar";
import { TodoStatus, type Todo } from "../models/todo";
import { useEffect, useState } from "react";
import Loader from "../components/Loader";
import { getTodo, assignTodo, closeTodo, unassignTodo } from "../api/todos";
import ErrorDialog from "../components/dialogs/ErrorDialog";
import ErrorPage from "../components/ErrorPage";

const TodoDetails = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const queryParams = new URLSearchParams(location.search);
  const todoId = queryParams.get("id");

  const assignedToMe = false; // TODO: Replace with real logic if needed

  const [todo, setTodo] = useState<Todo | undefined>(undefined);
  const [loading, setLoading] = useState(true);
  const [errorPageMessage, setErrorPageMessage] = useState("");
  const [errorDialogMessage, setErrorDialogMessage] = useState("");

  useEffect(() => {
    setLoading(true);
    const fetchTodo = async () => {
      try {
        const data = await getTodo(todoId ?? "0");
        setTodo(data);
      } catch (error) {
        setErrorPageMessage(
          error instanceof Error ? error.message : "An unknown error occurred"
        );
      } finally {
        setLoading(false);
      }
    };
    fetchTodo();
  }, [todoId]);

  const handleAssignTodo = async () => {
    try {
      const data = await assignTodo(todoId ?? "0");
      setTodo(data);
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };

  const handleUnassignTodo = async () => {
    try {
      const data = await unassignTodo(todoId ?? "0");
      setTodo(data);
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };

  const handleCloseTodo = async () => {
    try {
      const data = await closeTodo(todoId ?? "0");
      setTodo(data);
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
        <section className="min-h-screen flex items-center justify-center bg-gray-100 py-10">
          <section className="bg-white shadow-lg rounded-lg w-full max-w-3xl p-6 relative">
            <button
              className="absolute top-6 right-6 py-2 px-4 rounded bg-purple-500 text-white font-semibold hover:bg-purple-600 transition-colors"
              onClick={() => todo && navigate(`/todo/timeline?id=${todo.id}`)}
            >
              View Timeline
            </button>
            <section className="flex items-center justify-between border-b pb-4 mb-4">
              <section>
                <section className="text-xl font-semibold text-gray-800">
                  {todo?.title}
                </section>
                <section className="flex items-center mt-1 text-gray-500 text-sm">
                  <span className="mr-2">
                    {todo?.assignedTo ? (
                      <span>
                        Assigned to{" "}
                        <span className="font-medium">{todo.assignedTo}</span>
                      </span>
                    ) : (
                      <span>Unassigned</span>
                    )}
                  </span>
                </section>
              </section>
            </section>
            {/* Details Table */}
            <section className="grid grid-cols-2 gap-4 border-b pb-4 mb-4">
              <section>
                <section className="text-xs text-gray-400">State</section>
                <section className="text-sm font-medium">{todo?.status}</section>
              </section>
              <section>
                <section className="text-xs text-gray-400">Priority</section>
                <section className="text-sm font-medium">{todo?.priority}</section>
              </section>
              <section>
                <section className="text-xs text-gray-400">Created</section>
                <section className="text-sm font-medium">
                  {todo?.createdAt
                    ? new Date(todo.createdAt).toLocaleString()
                    : "-"}{" "}
                  by {todo?.createdBy}
                </section>
              </section>
              <section>
                <section className="text-xs text-gray-400">Last updated</section>
                <section className="text-sm font-medium">
                  {todo?.updatedAt
                    ? new Date(todo.updatedAt).toLocaleString()
                    : todo?.createdAt
                    ? new Date(todo.createdAt).toLocaleString()
                    : "-"}
                </section>
              </section>
              {todo?.status === TodoStatus.CLOSED && (
                <section>
                  <section className="text-xs text-gray-400">Closed</section>
                  <section className="text-sm font-medium">
                    {todo?.closedAt
                      ? new Date(todo.closedAt).toLocaleString()
                      : "-"}
                  </section>
                </section>
              )}
            </section>
            {/* Description and Actions */}
            <section className="grid grid-cols-3 gap-4">
              <section className="col-span-2">
                <section className="text-xs text-gray-400 mb-1">Description</section>
                {todo?.description ? (
                  <section className="text-base text-gray-800 bg-gray-50 border border-gray-200 rounded p-3">
                    {todo.description}
                  </section>
                ) : (
                  <section className="text-base text-gray-400 italic bg-gray-50 border border-gray-200 rounded p-3">
                    No description provided
                  </section>
                )}
              </section>
              <section>
                <br />
                <section className="flex flex-col gap-2">
                  {todo?.status === TodoStatus.OPEN && (
                    <button
                      className="w-full py-2 px-4 rounded bg-blue-500 text-white font-semibold hover:bg-blue-600 transition-colors"
                      onClick={handleAssignTodo}
                    >
                      Assign to myself
                    </button>
                  )}
                  {todo?.status === TodoStatus.INPROGRESS && assignedToMe && (
                    <>
                      <button
                        className="w-full py-2 px-4 rounded bg-green-500 text-white font-semibold hover:bg-green-600 transition-colors mb-2"
                        onClick={handleCloseTodo}
                      >
                        Close todo
                      </button>
                      <button
                        className="w-full py-2 px-4 rounded bg-yellow-500 text-white font-semibold hover:bg-yellow-600 transition-colors"
                        onClick={handleUnassignTodo}
                      >
                        Unassign from myself
                      </button>
                    </>
                  )}
                </section>
              </section>
            </section>
          </section>
        </section>
      )}
      <ErrorDialog
        errorMessage={errorDialogMessage}
        isOpen={errorDialogMessage !== ""}
        onClose={() => setErrorDialogMessage("")}
      />
    </>
  );
};

export default TodoDetails;
