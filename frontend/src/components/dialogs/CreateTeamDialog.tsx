import { useState } from "react";
import Dialog from "../Dialog";

type CreateTeamDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onCreateTeam?: (teamName: string) => void;
};

function CreateTeamDialog({
  isOpen,
  onClose,
  onCreateTeam,
}: CreateTeamDialogProps) {
  const [teamName, setTeamName] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (!teamName.trim()) {
      setErrorMessage("Team name is required")
      return;
    }
    if (teamName.length > 100) {
      setErrorMessage("Team name can be at most 100 characters")
      return
    }

    if (teamName.trim()) {
      onCreateTeam?.(teamName.trim());
      setTeamName("");
      onClose();
    }
  };

  const handleClose = () => {
    setTeamName("");
    onClose();
  };

  return (
    <Dialog isOpen={isOpen} onClose={handleClose} title="Create New Team">
      <form onSubmit={handleSubmit} className="">
        <section>
          <label htmlFor="teamName" className="mb-2">
            Team Name:
          </label>
          <input
            type="text"
            id="teamName"
            value={teamName}
            onChange={(e) => setTeamName(e.target.value)}
            className="w-full p-2 border rounded-md"
            placeholder="Enter team name"
            autoFocus
          />
        </section>
        <p className="min-h-6 text-red-500">{errorMessage}</p>
        <section className="flex flex-wrap gap-2 justify-end mt-4">
          <button
            type="button"
            onClick={handleClose}
            className="px-4 py-2 border w-24 hover:bg-gray-200 cursor-pointer"
          >
            Cancel
          </button>
          <button
            type="submit"
            className="px-4 py-2 border w-24 hover:bg-gray-200 cursor-pointer"
          >
            Create
          </button>
        </section>
      </form>
    </Dialog>
  );
}

export default CreateTeamDialog;
