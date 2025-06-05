import Navbar from "../components/Navbar";

const mockLogs = [
  { time: "14:23", date: "02/06/2025", message: "Created" },
  { time: "14:26", date: "02/06/2025", message: "Assigned to User 1" },
  { time: "18:45", date: "03/06/2025", message: "Unassigned from user 2" },
  { time: "19:23", date: "08/07/2025", message: "Assigned to user 3" },
  { time: "19:23", date: "12/07/2025", message: "Closed" },
];

const Logs = () => {
  return (
    <>
    <Navbar></Navbar>
    <section className="m-auto bg-gray-200 rounded-lg shadow-lg p-8 w-full max-w-2xl">
      <h2 className="text-2xl font-bold text-center mb-6 tracking-wide">
        TO-DO 1
      </h2>
      <section className="space-y-3">
        {mockLogs.map((log, idx) => (
          <section key={idx} className="flex justify-between font-mono text-lg">
            <span>{`${log.time} - ${log.date}`}</span>
            <span>{log.message}</span>
          </section>
        ))}
      </section>
    </section>
    </>
  );
};

export default Logs;
