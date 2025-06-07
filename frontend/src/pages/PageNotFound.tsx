import { useNavigate } from "react-router-dom";

function PageNotFound() {
  const navigate = useNavigate();

  return (
    <main className="min-h-screen flex flex-col items-center justify-center px-4 gap-4">
      <h1 className="text-5xl font-bold justify-center">Page not found</h1>
      <button
        onClick={() => navigate("/")}
        className="px-4 py-2 border hover:bg-gray-200 cursor-pointer"
      >
        Go to Login Page
      </button>
    </main>
  );
}

export default PageNotFound;
