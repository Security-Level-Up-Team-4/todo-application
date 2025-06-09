import { useState, useEffect } from "react";
import Dialog from "../Dialog";
import { type User } from "../../models/user";

type RemoveUsersProps = {
  isOpen: boolean;
  onClose: () => void;
  users: User[];
  onRemoveUsers?: (users: number[]) => void;
};

function RemoveUsersDialog({
  isOpen,
  onClose,
  users,
  onRemoveUsers,
}: RemoveUsersProps) {
  const [selectedUsers, setSelectedUsers] = useState<number[]>([]);

  useEffect(() => {
    if (isOpen) {
      setSelectedUsers([]);
    }
  }, [isOpen, users]);

  const toggleUser = (id: number) => {
    setSelectedUsers((prev) =>
      prev.includes(id) ? prev.filter((userId) => userId !== id) : [...prev, id]
    );
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onRemoveUsers?.(selectedUsers);
    setSelectedUsers([]);
    onClose();
  };

  const handleClose = () => {
    setSelectedUsers([]);
    onClose();
  };

  return (
    <Dialog isOpen={isOpen} onClose={handleClose} title="Remove Users">
      <form onSubmit={handleSubmit} className="space-y-4">
        <section className="max-h-60 overflow-y-auto">
          {users.filter(
            (user) => user.username !== localStorage.getItem("username")
          ).length === 0 ? (
            <p>No users have been added to this team yet</p>
          ) : (
            users
              .filter(
                (user) => user.username !== localStorage.getItem("username")
              )
              .map((user) => (
                <label key={user.id} className="flex items-center space-x-2">
                  <input
                    type="checkbox"
                    checked={selectedUsers.includes(user.id)}
                    onChange={() => toggleUser(user.id)}
                    className="w-4 h-4"
                  />
                  <span>{user.username}</span>
                </label>
              ))
          )}
        </section>

        <section className="flex justify-end space-x-2 pt-4">
          <button
            type="button"
            onClick={handleClose}
            className="px-4 py-2 border rounded hover:bg-gray-200"
          >
            Cancel
          </button>
          <button
            type="submit"
            disabled={selectedUsers.length === 0}
            className="px-4 py-2 border rounded disabled:opacity-50 disabled:cursor-not-allowed hover:bg-gray-200"
          >
            Remove
          </button>
        </section>
      </form>
    </Dialog>
  );
}

export default RemoveUsersDialog;
