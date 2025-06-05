import { useNavigate } from "react-router-dom";
import { mockTeams } from "../models/team";
import Navbar from "../components/Navbar";
import { useState } from "react";
import CreateTeamDialog from "../components/dialogs/CreateTeamDialog";

function Teams() {
  const navigate = useNavigate();
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const handleCreateTeam = (teamName: string) => {
    console.log("Creating team:", teamName);
  };

  return (
    <>
      <Navbar/>
      <main className="flex flex-col gap-8 p-4 w-full">
        <h1 className="w-full pt-4 text-center font-bold text-2xl">
          Welcome, User
        </h1>
        <section className="grid grid-cols-[repeat(auto-fit,minmax(12rem,1fr))] gap-4 w-full justify-center">
          <button
            onClick={() => setIsDialogOpen(true)}
            className="w-full h-32 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer text-9xl pb-6"
          >
            +
          </button>
          {mockTeams.map((team, index) => (
            <button
              key={index}
              onClick={() => navigate(`/todos?team=${team.id}`)}
              className="w-full h-32 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer"
            >
              {team.name}
            </button>
          ))}
        </section>
      </main>
      <CreateTeamDialog
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onCreateTeam={handleCreateTeam}
      />
    </>
  );
}

export default Teams;
