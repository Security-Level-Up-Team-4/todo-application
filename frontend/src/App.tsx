import { Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Registration from "./pages/Registration";
import './styles/app.css'
import Teams from "./pages/Teams";
import Todos from "./pages/Todos";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Login/>} />
      <Route path="/register" element={<Registration/>} />
      <Route path="/teams" element={<Teams/>} />
      <Route path="/todos" element={<Todos />} />
    </Routes>
  );
}

export default App;
