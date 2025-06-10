function PageNotFound() {
  return (
    <main className="min-h-screen flex flex-col items-center justify-center px-4 gap-4">
      <h1 className="text-5xl font-bold justify-center">Page not found</h1>
      <button
        onClick={() => {
          sessionStorage.removeItem("username");
          sessionStorage.removeItem("token");
          sessionStorage.removeItem("user-role");
          window.location.href = "/";
        }}
        className="px-4 py-2 border hover:bg-gray-200 cursor-pointer"
      >
        Go to Login Page
      </button>
    </main>
  );
}

export default PageNotFound;
