import { useNavigate } from "react-router-dom";

function Login() {
    const navigate = useNavigate();

  return (
    <button onClick={()=>navigate("teams")}>teams</button>

  );
}

export default Login;
