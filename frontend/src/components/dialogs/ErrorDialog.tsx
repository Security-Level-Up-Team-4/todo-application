import Dialog from "../Dialog";

type ErrorDialogProps = {
  isOpen: boolean;
  onClose: () => void;
  errorMessage: string;
};

function ErrorDialog({ isOpen, onClose, errorMessage }: ErrorDialogProps) {
  return (
    <Dialog isOpen={isOpen} onClose={onClose} title="An error has occurred">
      <section className="mb-4">
        {errorMessage}
      </section>
      <section className="flex justify-end mt-4">
        <button
          type="button"
          onClick={onClose}
          className="px-4 py-2 border w-28 hover:bg-gray-200 cursor-pointer"
        >
          Close
        </button>
      </section>
    </Dialog>
  );
}

export default ErrorDialog;
