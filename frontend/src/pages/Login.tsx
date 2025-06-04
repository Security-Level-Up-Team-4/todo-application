import { Link } from "react-router-dom";
import { useState } from "react";
function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(email, password);
  };
  return (
    <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-md">
      <h2 className="text-3xl font-bold text-center text-gray-800 mb-6">
        Sign in to your account
      </h2>
      <form className="space-y-5" onSubmit={handleSubmit}>
        <div>
          <label
            htmlFor="email"
            className="block text-sm font-medium text-gray-700"
          >
            Email address or username
          </label>
          <input
            type="email"
            id="email"
            name="email"
            autoComplete="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-2 focus:ring-blue-400 focus:border-blue-400 outline-none"
            placeholder="you@example.com or username"
          />
        </div>
        <div>
          <label
            htmlFor="password"
            className="block text-sm font-medium text-gray-700"
          >
            Password
          </label>
          <input
            type="password"
            id="password"
            name="password"
            autoComplete="current-password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-2 focus:ring-blue-400 focus:border-blue-400 outline-none"
            placeholder="••••••••"
          />
        </div>
        <button
          type="submit"
          className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 font-semibold transition-colors"
        >
          Sign In
        </button>
      </form>
      <p className="mt-6 text-center text-sm text-gray-600">
        Don't have an account?{" "}
        <Link
          to="/register"
          className="text-blue-600 hover:underline font-medium"
        >
          Sign up
        </Link>
      </p>
    </div>
  );
}

export default Login;
