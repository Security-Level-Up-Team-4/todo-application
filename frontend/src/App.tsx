import { Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Registration from "./pages/Registration";
import Teams from "./pages/Teams";
import Todos from "./pages/Todos";
import AdminManagement from "./pages/AdminManagement";
import Timeline from "./pages/Timeline";
import PageNotFound from "./pages/PageNotFound";
import Todo from "./pages/Todo";
function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Registration />} />
      <Route path="/teams" element={<Teams />} />
      <Route path="/todos" element={<Todos />} />
      <Route path="/admin" element={<AdminManagement />} />
      <Route path="/todo" element={<Todo />} />
      <Route path="/todo/timeline" element={<Timeline />} />
      <Route path="*" element={<PageNotFound />} />
    </Routes>
  );
}

export default App;
