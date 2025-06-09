import { useState } from "react";
import Dialog from "../Dialog";

type CreateTodoDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  onCreateTodo?: (
    todoName: string,
    todoDescription: string,
    todoPriority: number
  ) => void;
};

function CreateTodoDialog({
  isOpen,
  onClose,
  onCreateTodo,
}: CreateTodoDialogProps) {
  const [todoName, setTodoName] = useState("");
  const [todoDescription, setTodoDescription] = useState("");
  const [todoPriority, setTodoPriority] = useState("1"); // 1 = Medium

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (todoName.trim()) {
      onCreateTodo?.(
        todoName.trim(),
        todoDescription.trim(),
        Number(todoPriority)
      );
      setTodoName("");
      setTodoDescription("");
      setTodoPriority("1");
      onClose();
    }
  };

  const handleClose = () => {
    setTodoName("");
    setTodoDescription("");
    setTodoPriority("1");
    onClose();
  };

  return (
    <Dialog isOpen={isOpen} onClose={handleClose} title="Create New Todo">
      <form onSubmit={handleSubmit}>
        <section className="mb-4">
          <label htmlFor="todoName" className="mb-2 block">
            Todo Name:
          </label>
          <input
            type="text"
            id="todoName"
            value={todoName}
            onChange={(e) => setTodoName(e.target.value)}
            className="w-full p-2 border rounded-md"
            placeholder="Enter Todo name"
            autoFocus
          />
        </section>

        <section className="mb-4">
          <label htmlFor="todoDescription" className="mb-2 block">
            Description:
          </label>
          <textarea
            id="todoDescription"
            value={todoDescription}
            onChange={(e) => setTodoDescription(e.target.value)}
            className="w-full p-2 border rounded-md"
            rows={3}
            placeholder="Enter a description"
          />
        </section>

        <section className="mb-4">
          <label htmlFor="todoPriority" className="mb-2 block">
            Priority:
          </label>
          <select
            id="todoPriority"
            value={todoPriority}
            onChange={(e) => setTodoPriority(e.target.value)}
            className="w-full p-2 border rounded-md"
          >
            <option value="1">Low</option>
            <option value="2">Medium</option>
            <option value="3">High</option>
            <option value="4">Critical</option>
          </select>
        </section>

        <section className="flex flex-wrap gap-2 justify-end">
          <button
            type="button"
            onClick={handleClose}
            className="px-4 py-2 border w-24 hover:bg-gray-200 cursor-pointer"
          >
            Cancel
          </button>
          <button
            type="submit"
            disabled={!todoName.trim()}
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
