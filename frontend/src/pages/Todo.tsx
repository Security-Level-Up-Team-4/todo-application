import { useLocation } from "react-router-dom";
import Navbar from "../components/Navbar";
import { TodoStatus } from "../models/todo";
import type { Todo } from "../models/todo";
import { useEffect, useState } from "react";
import Loader from "../components/Loader";
import { getTodo, assignTodo, closeTodo, unassignTodo } from "../api/todos";

const TodoDetails = () => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const todoId = queryParams.get("id");

  const assignedToMe = true;

  const [todo, setTodo] = useState<Todo>();
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
      <Navbar></Navbar>
      {loading ? (
        <Loader />
      ) : (
        <main>
          <h2>{todo?.name}</h2>
          <p>{todo?.description}</p>
          <p>Status - {todo?.status}</p>
          <p>Priority - {todo?.priority}</p>
          <p>
            Created - {todo?.createdAt?.toLocaleString() /*format better*/} by{" "}
            {todo?.createdBy}
          </p>

          <p>
            Last updated -{" "}
            {todo?.updatedAt
              ? todo?.updatedAt.toLocaleString() /*format better*/
              : todo?.createdAt.toLocaleString()}
          </p>
          <p></p>
          {todo?.status === TodoStatus.OPEN && (
            <button className="bg-amber-200" onClick={handleAssignTodo}>
              Assign to myself
            </button>
          )}
          {todo?.status === TodoStatus.INPROGRESS && assignedToMe ? (
            <>
              <button className="bg-amber-200 mr-4" onClick={handleCloseTodo}>
                Close todo
              </button>
              <button className="bg-amber-200" onClick={handleUnassignTodo}>
                Unassign from myself
              </button>
            </>
          ) : (
            <p>Assigned to - {todo?.assignedTo}</p>
          )}
          {todo?.status === TodoStatus.CLOSED && (
            <p>Closed - {todo?.closedAt?.toLocaleString() /*format better*/}</p>
          )}
        </main>
      )}
    </>
  );
};

export default TodoDetails;
