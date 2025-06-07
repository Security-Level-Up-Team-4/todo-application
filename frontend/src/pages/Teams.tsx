import { useNavigate } from "react-router-dom";
import { type Team } from "../models/team";
import Navbar from "../components/Navbar";
import { useEffect, useState } from "react";
import CreateTeamDialog from "../components/dialogs/CreateTeamDialog";
import Loader from "../components/Loader";
import { getTeams, postTeam } from "../api/teams";

function Teams() {
  const navigate = useNavigate();
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const [teams, setTeams] = useState<Team[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    const fetchTeams = async () => {
      try {
        const data = await getTeams();
        setTeams(data);
      } catch {
        // TODO: Show error page
      } finally {
        setLoading(false);
      }
    };

    fetchTeams();
  }, []);

  const handleCreateTeam = async (teamName: string) => {
    try {
      const data = await postTeam(teamName);
      setTeams(data);
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
            {teams.map((team, index) => (
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
      )}
      <CreateTeamDialog
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onCreateTeam={handleCreateTeam}
      />
    </>
  );
}

export default Teams;
