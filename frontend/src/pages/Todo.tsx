import { useLocation } from "react-router-dom";
import Navbar from "../components/Navbar";
import { TodoStatus, type Todo } from "../models/todo";
import { useEffect, useState } from "react";
import Loader from "../components/Loader";
import { getTodo, assignTodo, closeTodo, unassignTodo } from "../api/todos";

const TodoDetails = () => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const todoId = queryParams.get("id");

  const assignedToMe = true; // TODO: Replace with real logic if needed

  const [todo, setTodo] = useState<Todo | undefined>(undefined);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    const fetchTodo = async () => {
      try {
        const data = await getTodo(todoId ?? "0");
        setTodo(data);
      } catch {
        // TODO: Show error page
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
    } catch {
      // TODO: Show error page
    } finally {
      setLoading(false);
    }
  };

  const handleUnassignTodo = async () => {
    try {
      const data = await unassignTodo(todoId ?? "0");
      setTodo(data);
    } catch {
      // TODO: Show error page
    } finally {
      setLoading(false);
    }
  };

  const handleCloseTodo = async () => {
    try {
      const data = await closeTodo(todoId ?? "0");
      setTodo(data);
    } catch {
      // TODO: Show error page
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Navbar />
      {loading ? (
        <Loader />
      ) : (
        <div className="min-h-screen flex items-center justify-center bg-gray-100 py-10">
          <div className="bg-white shadow-lg rounded-lg w-full max-w-3xl p-6">
            {/* Header */}
            <div className="flex items-center justify-between border-b pb-4 mb-4">
              <div>
                <div className="text-xl font-semibold text-gray-800">
                  {todo?.name}
                </div>
                <div className="flex items-center mt-1 text-gray-500 text-sm">
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
                </div>
              </div>
            </div>
            {/* Details Table */}
            <div className="grid grid-cols-2 gap-4 border-b pb-4 mb-4">
              <div>
                <div className="text-xs text-gray-400">State</div>
                <div className="text-sm font-medium">{todo?.status}</div>
              </div>
              <div>
                <div className="text-xs text-gray-400">Priority</div>
                <div className="text-sm font-medium">{todo?.priority}</div>
              </div>
              <div>
                <div className="text-xs text-gray-400">Created</div>
                <div className="text-sm font-medium">
                  {todo?.createdAt
                    ? new Date(todo.createdAt).toLocaleString()
                    : "-"}{" "}
                  by {todo?.createdBy}
                </div>
              </div>
              <div>
                <div className="text-xs text-gray-400">Last updated</div>
                <div className="text-sm font-medium">
                  {todo?.updatedAt
                    ? new Date(todo.updatedAt).toLocaleString()
                    : todo?.createdAt
                    ? new Date(todo.createdAt).toLocaleString()
                    : "-"}
                </div>
              </div>
              {todo?.status === TodoStatus.CLOSED && (
                <div>
                  <div className="text-xs text-gray-400">Closed</div>
                  <div className="text-sm font-medium">
                    {todo?.closedAt
                      ? new Date(todo.closedAt).toLocaleString()
                      : "-"}
                  </div>
                </div>
              )}
            </div>
            {/* Description and Actions */}
            <div className="grid grid-cols-3 gap-4">
              <div className="col-span-2">
                <div className="text-xs text-gray-400 mb-1">Description</div>
                {todo?.description ? (
                  <div className="text-base text-gray-800 bg-gray-50 border border-gray-200 rounded p-3">
                    {todo.description}
                  </div>
                ) : (
                  <div className="text-base text-gray-400 italic bg-gray-50 border border-gray-200 rounded p-3">
                    No description provided
                  </div>
                )}
              </div>
              <div>
                <br />
                <div className="flex flex-col gap-2">
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
                  {todo?.status === TodoStatus.INPROGRESS && !assignedToMe && (
                    <div className="text-gray-500 text-sm">
                      Assigned to - {todo?.assignedTo}
                    </div>
                  )}
                  {/* {todo?.status === TodoStatus.CLOSED && (
                    <div className="text-gray-500 text-sm">
                      Closed -{" "}
                      {todo?.closedAt
                        ? new Date(todo.closedAt).toLocaleString()
                        : "-"}
                    </div>
                  )} */}
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default TodoDetails;
