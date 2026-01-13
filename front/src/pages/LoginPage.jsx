import {useAuth} from "../auth/AuthContext";
import {useNavigate} from "react-router-dom";
import {useState} from "react";

export default function LoginPage() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();

        setLoading(true);

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
            setLoading(false);
            return;
        }

        const token = await res.text();
        login(token);
        navigate("/");
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Zaloguj się</h2>
            <div className="form_row">
                <input
                    name="login"
                    type="text"
                    placeholder="Login"
                    required
                />
            </div>

            <div className="form_row">
                <input
                    name="password"
                    type="password"
                    placeholder="Hasło"
                    required
                />
            </div>

            <div className="form_action">
                <button type="submit" disabled={loading}>Zaloguj</button>
            </div>
        </form>
    );
}