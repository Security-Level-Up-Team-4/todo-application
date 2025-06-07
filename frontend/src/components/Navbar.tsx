import { useNavigate } from "react-router-dom";

type NavbarProps = {
  isAdminPage?: boolean;
};

function Navbar({ isAdminPage = false }: NavbarProps) {
  const navigate = useNavigate();

  return (
    <nav className="flex flex-wrap items-center justify-end gap-2 bg-gray-50 p-2 shadow-md">
      {!isAdminPage && (
        <>
          <button
            onClick={() => navigate(-1)}
            className="px-4 py-2 border max-w-22 w-full hover:bg-gray-200 cursor-pointer"
          >
            Back
          </button>

          <button
            onClick={() => navigate("/teams")}
            className="px-4 py-2 border max-w-22 w-full hover:bg-gray-200 cursor-pointer"
          >
            Home
          </button>
        </>
      )}

      <button
        onClick={() => {
          //TODO clear whatever is needed to logout
          navigate("/");
        }}
        className="px-4 py-2 border max-w-22 w-full hover:bg-gray-200 cursor-pointer"
      >
        Logout
      </button>
    </nav>
  );
}

export default Navbar;
