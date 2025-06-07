import { useRef, useEffect } from "react";

type DialogProps = {
  isOpen: boolean;
  onClose: () => void;
  title?: string;
  children?: React.ReactNode;
};

function Dialog({ isOpen, onClose, title, children }: DialogProps) {
  const dialogRef = useRef<HTMLDialogElement>(null);

  useEffect(() => {
    const dialog = dialogRef.current;
    if (!dialog) return;

    if (isOpen && !dialog.open) {
      dialog.showModal();
    } else if (!isOpen && dialog.open) {
      dialog.close();
    }
  }, [isOpen]);

  return (
    <dialog
      ref={dialogRef}
      onClose={onClose}
      className="p-8 rounded-md w-full max-w-xl shadow-lg backdrop:bg-black/40 m-auto"
    >
      <section className="flex justify-between items-center mb-4">
        {title && <h2 className="text-xl font-bold">{title}</h2>}
        <button
          onClick={onClose}
          className="text-gray-500 hover:text-gray-900 cursor-pointer text-4xl font-bold"
        >
          &times;
        </button>
      </section>
      {children}
    </dialog>
  );
}

export default Dialog;
