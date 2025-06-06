import React from "react";
import { Link } from "react-router-dom";

const ErrorComponent = ({ errorTitle, errorMessage }) => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-[#5656b6]">
      <div className="w-full max-w-xl bg-[#5656b6] rounded-lg flex flex-col items-center py-16 px-4">
        <h1 className="text-white text-xl font-semibold mb-6">{errorTitle}</h1>
        <p className="text-gray-100 text-center mb-2">
          Uh-oh! The letter <span className="font-bold text-white">"p"</span>{" "}
          lost its <span className="font-bold text-white">age</span>.
        </p>
        <p className="text-white text-center font-semibold mb-4">
          {errorMessage}
        </p>
        <p className="text-white text-center mb-8">
          Looks like it wandered off and left the "page" behind. Let's help it
          find its way home.
        </p>
        <div className="flex items-end justify-center mb-8">
          <span className="text-5xl font-extrabold text-white mr-4">age</span>
          <span className="text-5xl font-extrabold text-blue-400">P</span>
        </div>
        <Link
          to="/"
          className="text-white font-medium underline hover:text-blue-200 transition-colors"
        >
          Go back home
        </Link>
      </div>
    </div>
  );
};

export default ErrorPage;
