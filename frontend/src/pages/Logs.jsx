import React from "react";

const mockLogs = [
  { time: "14:23", date: "02/06/2025", message: "Created" },
  { time: "14:26", date: "02/06/2025", message: "Assigned to User 1" },
  { time: "18:45", date: "03/06/2025", message: "Unassigned from user 2" },
  { time: "19:23", date: "08/07/2025", message: "Assigned to user 3" },
  { time: "19:23", date: "12/07/2025", message: "Closed" },
];

const Logs = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-[#5254b6] py-10">
      <div className="bg-gray-200 rounded-lg shadow-lg p-8 w-full max-w-2xl">
        <h2 className="text-2xl font-bold text-center mb-6 tracking-wide">
          TO-DO 1
        </h2>
        <div className="space-y-3">
          {mockLogs.map((log, idx) => (
            <div key={idx} className="flex justify-between font-mono text-lg">
              <span>{`${log.time} - ${log.date}`}</span>
              <span>{log.message}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Logs;
