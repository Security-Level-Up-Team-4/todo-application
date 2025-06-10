import { useLocation } from "react-router-dom";
import Navbar from "../components/Navbar";
import { type TodoTimeline } from "../models/todo";
import { useEffect, useState } from "react";
import { getTodoTimeline } from "../api/todos";
import Loader from "../components/Loader";
import ErrorPage from "../components/ErrorPage";

const Timeline = () => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const todoId = queryParams.get("id");

  const [timeline, setTimeline] = useState<TodoTimeline>();
  const [loading, setLoading] = useState(true);
  const [errorPageMessage, setErrorPageMessage] = useState("");

  useEffect(() => {
    setLoading(true);
    const fetchTimeline = async () => {
      try {
        const data = await getTodoTimeline(todoId ?? "");
        setTimeline(data);
      } catch (error) {
        setErrorPageMessage(
          error instanceof Error ? error.message : "An unknown error occurred"
        );
      } finally {
        setLoading(false);
      }
    };

    fetchTimeline();
  }, [todoId]);

  return (
    <>
      <Navbar></Navbar>
      {loading ? (
        <Loader />
      ) : errorPageMessage ? (
        <ErrorPage
          errorMessage={errorPageMessage}
          errorTitle="An error has occurred"
        />
      ) : (
        <section className="m-auto bg-gray-200 rounded-lg shadow-lg p-4 box-border w-full max-w-2xl">
          <h2 className="text-2xl font-bold text-center mb-6 tracking-wide">
            {timeline?.title}
          </h2>
          <section className="space-y-3">
            {timeline?.timeline.map((event, idx) => (
              <>
              <section
                key={idx}
                className="flex justify-between items-center font-mono text-lg"
              >
                <span className="w-1/4">{`${new Date(event.createdAt).toLocaleString()}`}</span>
                <span className="w-1/2">{event.event}</span>
              </section>
              <hr></hr>
              </>
            ))}
          </section>
        </section>
      )}
    </>
  );
};

export default Timeline;
