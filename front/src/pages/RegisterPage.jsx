import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function RegisterPage() {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        login: "",
        password: "",
        email: "",
        name: "",
        surname: "",
        birthDate: ""
    });

    const [validationErrors, setValidationErrors] = useState({
        login: "",
        password: "",
        email: "",
        name: "",
        surname: "",
        birthDate: ""
    });

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const today = new Date();

    const handleChange = e => {
        const { name, value } = e.target;

        setForm(prev => ({
            ...prev,
            [name]: value
        }));
    };


    const validateField = (name, value) => {
        let err = "";

        if (name === "login") {
            if (value.trim().length < 3) err = "Login musi mieć minimum 3 znaki.";
            else if (value.length > 50) err = "Login może mieć maksymalnie 50 znaków.";
        }

        if (name === "password") {
            if (value.length < 5) err = "Hasło musi mieć min. 5 znaków.";
        }

        if (name === "email") {
            if (!value.includes("@")) err = "Podaj poprawny email.";
        }

        if (name === "name") {
            if (value.trim().length < 2) err = "Imię musi mieć co najmniej 2 znaki.";
        }

        if (name === "surname") {
            if (value.trim().length < 2) err = "Nazwisko musi mieć co najmniej 2 znaki.";
        }

        if (name === "birthDate") {
            const date = new Date(value);
            const min = new Date("1900-01-01");

            if (isNaN(date.getTime())) err = "Podaj prawidłową datę.";
            else if (date < min) err = "Data musi być późniejsza niż 1900.";
            else if (date > today) err = "Nie można podać przyszłej daty.";
        }

        setValidationErrors(prev => ({ ...prev, [name]: err }));
        return err === "";
    };

    const validateForm = () => {
        const results = Object.keys(form).map(key =>
            validateField(key, form[key])
        );
        return results.every(r => r === true);
    };

    const handleSubmit = async e => {
        e.preventDefault();

        if (!validateForm()) {
            setError("Popraw błędy w formularzu.");
            return;
        }

        setError(null);
        setLoading(true);

        try {
            console.log(JSON.stringify(form));
            const res = await fetch("http://localhost:5058/api/auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form)
            });

            if (!res.ok) {
                const text = await res.text();
                throw new Error(text || "Błąd rejestracji.");
            }

            navigate("/login");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };


    return (
        <form onSubmit={handleSubmit}>
            <h2>Rejestracja</h2>

            {error && <p className="error-msg">{error}</p>}

            <div className="form_row">
                <input
                    name="login"
                    value={form.login}
                    onChange={handleChange}
                    placeholder="Login"
                    required
                />
                {validationErrors.login && <p className="error-msg">{validationErrors.login}</p>}
            </div>

            <div className="form_row">
                <input
                    name="email"
                    type="email"
                    value={form.email}
                    onChange={handleChange}
                    placeholder="Email"
                    required
                />
                {validationErrors.email && <p className="error-msg">{validationErrors.email}</p>}
            </div>

            <div className="form_row">
                <input
                    name="password"
                    type="password"
                    value={form.password}
                    onChange={handleChange}
                    placeholder="Hasło"
                    required
                />
                {validationErrors.password && <p className="error-msg">{validationErrors.password}</p>}
            </div>

            <div className="form_row">
                <input
                    name="name"
                    value={form.name}
                    onChange={handleChange}
                    placeholder="Imię"
                    required
                />
                {validationErrors.name && <p className="error-msg">{validationErrors.name}</p>}
            </div>

            <div className="form_row">
                <input
                    name="surname"
                    value={form.surname}
                    onChange={handleChange}
                    placeholder="Nazwisko"
                    required
                />
                {validationErrors.surname && <p className="error-msg">{validationErrors.surname}</p>}
            </div>

            <div className="form_row">
                <input
                    type="date"
                    name="birthDate"
                    value={form.birthDate}
                    onChange={handleChange}
                    min="1900-01-01"
                    max={today.toISOString().split("T")[0]}
                    required
                />
                {validationErrors.birthDate && <p className="error-msg">{validationErrors.birthDate}</p>}
            </div>

            <div className="form_action">
                <button type="submit" disabled={loading}>
                    {loading ? "Rejestrowanie..." : "Zarejestruj"}
                </button>
            </div>
        </form>
    );
}
