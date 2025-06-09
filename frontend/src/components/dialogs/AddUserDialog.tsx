import { useState } from "react";
import Dialog from "../Dialog";

type AddUserDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onAddUser?: (username: string) => void;
};

function AddUserDialog({ isOpen, onClose, onAddUser }: AddUserDialogProps) {
  const [username, setUsername] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if (username.trim()) {
      onAddUser?.(username.trim());
      setUsername("");
      onClose();
    }
  };

  const handleClose = () => {
    setUsername("");
    onClose();
  };

  return (
    <Dialog isOpen={isOpen} onClose={handleClose} title="Add new user">
      <form onSubmit={handleSubmit} className="">
        <section>
          <label htmlFor="username" className="mb-2">
            Username:
          </label>
          <input
            type="text"
            id="username"
            value={username}
            required
            onChange={(e) => setUsername(e.target.value)}
            className="w-full p-2 border rounded-md"
            placeholder="Enter username"
            autoFocus
          />
        </section>
        <section className="flex flex-wrap gap-2 justify-end mt-4">
          <button
            type="button"
            onClick={handleClose}
            className="px-4 py-2 border w-28 hover:bg-gray-200 cursor-pointer"
          >
            Cancel
          </button>
          <button
            type="submit"
            disabled={!username.trim()}
            className={`px-4 py-2 border w-28 hover:bg-gray-200 ${
              !username.trim() ? "cursor-not-allowed" : "cursor-pointer"
            } `}
          >
            Add User
          </button>
        </section>
      </form>
    </Dialog>
  );
}

export default AddUserDialog;
