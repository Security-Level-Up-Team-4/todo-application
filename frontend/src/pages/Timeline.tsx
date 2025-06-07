import { useLocation } from "react-router-dom";
import Navbar from "../components/Navbar";
import { type TodoTimeline } from "../models/todo";
import { useEffect, useState } from "react";
import { getTodoTimeline } from "../api/todos";
import Loader from "../components/Loader";

const Timeline = () => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const todoId = queryParams.get("id");

  const [timeline, setTimeline] = useState<TodoTimeline>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    const fetchTimeline = async () => {
      try {
        const data = await getTodoTimeline(todoId ?? "");
        setTimeline(data);
      } catch {
        // TODO: Show error page
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
      ) : (
        <section className="m-auto bg-gray-200 rounded-lg shadow-lg p-8 w-full max-w-2xl">
          <h2 className="text-2xl font-bold text-center mb-6 tracking-wide">
            {timeline?.name}
          </h2>
          <section className="space-y-3">
            {timeline?.events.map((event, idx) => (
              <section
                key={idx}
                className="flex justify-between font-mono text-lg"
              >
                <span>{`${event.time} - ${event.date}`}</span>
                <span>{event.event}</span>
              </section>
            ))}
          </section>
        </section>
      )}
    </>
  );
};

export default Timeline;
