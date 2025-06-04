import { Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Registration from "./pages/Registration";
import AdminManagement from "./pages/AdminManagement";
import Logs from "./pages/Logging";
import "./styles/app.css";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/register" element={<Registration />} />
      <Route path="/admin" element={<AdminManagement />} />
      <Route path="/logs" element={<Logs />} />
    </Routes>
  );
}

export default App;
