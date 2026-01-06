import {useAuth} from "../auth/AuthContext";
import {Form, useNavigate} from "react-router-dom";

export default function LoginPage() {
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        const res = await fetch("http://localhost:5058/api/auth/login", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify({
                login: e.target.login.value,
                password: e.target.password.value
            })
        });

        if(!res.ok) {
            alert("Błędne dane logowania!");
            return;
        }

        const token = await res.text();
        login(token);
        navigate("/");
    };

    return (
        <form onSubmit={handleSubmit}>
            <input name="login" placeholder="Login" />
            <input name="password" placeholder="Password" />
            <button type="submit">Zaloguj</button>
        </form>
    );
}