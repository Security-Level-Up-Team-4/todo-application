import { Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Registration from "./pages/Registration";
import './styles/app.css'
import Teams from "./pages/Teams";
import Todos from "./pages/Todos";
import AdminManagement from "./pages/AdminManagement";
import Timeline from "./pages/Timeline";
import "./styles/app.css";
import TodoDetails from "./pages/Todo";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Login/>} />
      <Route path="/register" element={<Registration/>} />
      <Route path="/teams" element={<Teams/>} />
      <Route path="/todos" element={<Todos />} />
      <Route path="/todo" element={<TodoDetails />} />
      <Route path="/admin" element={<AdminManagement />} />
      <Route path="/todo/timeline" element={<Timeline />} />
    </Routes>
  );
}

export default App;
