import { useNavigate } from "react-router-dom";
import { type Team } from "../models/team";
import Navbar from "../components/Navbar";
import { useEffect, useState } from "react";
import CreateTeamDialog from "../components/dialogs/CreateTeamDialog";
import Loader from "../components/Loader";
import { getTeams, addTeam } from "../api/teams";
import ErrorPage from "../components/ErrorPage";
import ErrorDialog from "../components/dialogs/ErrorDialog";
import { UserRoles } from "../models/user";

function Teams() {
  const navigate = useNavigate();
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [teams, setTeams] = useState<Team[]>([]);
  const [loading, setLoading] = useState(true);
  const [errorPageMessage, setErrorPageMessage] = useState("");
  const [errorDialogMessage, setErrorDialogMessage] = useState("");

  useEffect(() => {
    setLoading(true);
    const fetchTeams = async () => {
      try {
        const data = await getTeams();
        setTeams(data);
      } catch (error) {
        setErrorPageMessage(
          error instanceof Error ? error.message : "An unknown error occurred"
        );
      } finally {
        setLoading(false);
      }
    };

    fetchTeams();
  }, []);

  const handleCreateTeam = async (teamName: string) => {
    try {
      setLoading(true);
      const data = await addTeam(teamName);
      setTeams([...teams, data]);
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    } finally {
      setLoading(false);
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
            Welcome, {sessionStorage.getItem("username") ?? "User"}
          </h1>
          <section className="grid grid-cols-[repeat(auto-fit,minmax(12rem,1fr))] gap-4 w-full justify-center">
            {sessionStorage.getItem("user-role") == UserRoles.TEAMLEAD && (
              <button
                onClick={() => setIsDialogOpen(true)}
                className="w-full h-32 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer text-9xl pb-6"
              >
                +
              </button>
            )}
            {teams.map((team, index) => (
              <button
                key={index}
                onClick={() => navigate(`/todos?team=${team.teamId}`)}
                className="w-full h-32 shadow-lg border flex items-center justify-center hover:bg-gray-200 cursor-pointer"
              >
                {team.teamName}
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
      <ErrorDialog
        errorMessage={errorDialogMessage}
        isOpen={errorDialogMessage !== ""}
        onClose={() => setErrorDialogMessage("")}
      />
    </>
  );
}

export default Teams;
