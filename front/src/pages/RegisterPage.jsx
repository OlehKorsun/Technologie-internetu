import {useState} from "react";
import {useNavigate} from "react-router-dom";

export default function RegisterPage() {
    const [form, setForm] = useState({
        login: "",
        password: "",
        email: ""
    });

    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        setError(null);

        const res = await fetch("http://localhost:5058/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(form)
        });

        if(!res.ok) {
            const text = await res.text();
            setError(text || "Błąd rejestracji");
            return;
        }

        navigate("/login");
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Rejestracja</h2>

            {error && <p className="error">{error}</p>}

            <input
                name="login"
                type="text"
                placeholder="Login"
                onChange={handleChange}
                required
            />

            <input
                name="email"
                type="email"
                placeholder="Email"
                onChange={handleChange}
                required
            />

            <input
                name="password"
                type="password"
                placeholder="Hasło"
                onChange={handleChange}
                required
            />

            <button type="submit">Zarejestruj</button>
        </form>
    );
}