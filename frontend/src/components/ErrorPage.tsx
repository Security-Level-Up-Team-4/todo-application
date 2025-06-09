type ErrorPageProps = {
  errorTitle: string;
  errorMessage: string;
};

function ErrorPage({ errorTitle, errorMessage }: ErrorPageProps) {
  return (
    <section className="flex-1 flex items-center justify-center">
      <section className="w-full max-w-xl rounded-lg flex flex-col items-center py-4 px-4">
        <h1 className="text-center text-xl font-semibold mb-6">{errorTitle}</h1>
        <p className="text-center font-semibold mb-4">{errorMessage}</p>
        <button
          className="px-4 py-2 border hover:bg-gray-200 cursor-pointer"
          onClick={() => window.location.reload()}
        >
          Retry
        </button>
      </section>
    </section>
  );
}

export default ErrorPage;
