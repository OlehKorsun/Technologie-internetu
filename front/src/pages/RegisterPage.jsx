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
    const [loading, setLoading] = useState(false);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        const res = await fetch("http://localhost:5058/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(form)
        });

        if(!res.ok) {
            const text = await res.text();
            setError(text || "Błąd rejestracji");
            setLoading(false);
            return;
        }

        navigate("/login");
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Rejestracja</h2>

            {error && <p className="error">{error}</p>}

            <div className="form_row">
                <input
                    name="login"
                    type="text"
                    placeholder="Login"
                    onChange={handleChange}
                    required
                />
            </div>

            <div className="form_row">
                <input
                    name="email"
                    type="email"
                    placeholder="Email"
                    onChange={handleChange}
                    required
                />
            </div>

            <div className="form_row">
                <input
                    name="password"
                    type="password"
                    placeholder="Hasło"
                    onChange={handleChange}
                    required
                />
            </div>

            <div className="form_action">
                <button type="submit" disabled={loading}>Zarejestruj</button>
            </div>
        </form>
    );
}