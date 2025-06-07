import { useState } from "react";
import Dialog from "../Dialog";

type CreateTodoDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onCreateTodo?: (TodoName: string) => void;
};

function CreateTodoDialog({
  isOpen,
  onClose,
  onCreateTodo,
}: CreateTodoDialogProps) {
  const [TodoName, setTodoName] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (TodoName.trim()) {
      onCreateTodo?.(TodoName.trim());
      setTodoName("");
      onClose();
    }
  };

  const handleClose = () => {
    setTodoName("");
    onClose();
  };

  return (
    <Dialog isOpen={isOpen} onClose={handleClose} title="Create New Todo">
      <form onSubmit={handleSubmit} className="">
        <section>
          <label htmlFor="todoName" className="mb-2">
            Todo Name:
          </label>
          <input
            type="text"
            id="todoName"
            value={TodoName}
            onChange={(e) => setTodoName(e.target.value)}
            className="w-full p-2 border rounded-md"
            placeholder="Enter Todo name"
            autoFocus
          />
        </section>
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
            disabled={!TodoName.trim()}
            className="px-4 py-2 border w-24 hover:bg-gray-200 cursor-pointer"
          >
            Create
          </button>
        </section>
      </form>
    </Dialog>
  );
}

export default CreateTodoDialog;
