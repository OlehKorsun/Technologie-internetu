import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {apiFetch} from "../api/api";

export default function ClientForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEdit = !!id;

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const [form, setForm] = useState({
        name: "",
        surname: "",
        birthDate: ""
    });

    useEffect(() => {
        if (!isEdit) return;

        apiFetch(`http://localhost:5058/api/clients/${id}`)
            .then(async res => {
                if (!res.ok) {
                    const msg = await res.text();
                    throw new Error(msg);
                }
                return res.json();
                // return res;
            })
            .then(data => {
                setForm({
                    name: data.name,
                    surname: data.surname,
                    birthDate: data.birthDate.slice(0, 10)
                });
            })
            .catch(err => setError(err.message));
    }, [id, isEdit]);

    const handleChange = e => {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        const url = isEdit
            ? `http://localhost:5058/api/clients/${id}`
            : "http://localhost:5058/api/clients";

        const method = isEdit ? "PUT" : "POST";


        try {
            const data = await apiFetch(url, {
                method,
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form)
            });

            navigate("/clients");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }


    };

    const today = new Date();
    today.setFullYear(today.getFullYear() - 18);


    return (
        <form onSubmit={handleSubmit}>
            <h2>{isEdit ? "Edytuj klienta" : "Dodaj klienta"}</h2>

            <input
                name="name"
                value={form.name}
                onChange={handleChange}
                placeholder="Imię"
                required
            />

            <input
                name="surname"
                value={form.surname}
                onChange={handleChange}
                placeholder="Nazwisko"
                required
            />



            <input
                type="date"
                name="birthDate"
                value={form.birthDate}
                onChange={handleChange}
                min="1900-01-01"
                max={today.toISOString().split("T")[0]}
                required
            />

            <button type="submit" disabled={loading}>
                {isEdit ? loading ? "Zapisywanie" : "Zapisz zmiany" : "Dodaj"}
            </button>

            {error && <p className="error">{error}</p>}
        </form>
    );
}
