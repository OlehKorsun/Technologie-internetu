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

    const [validationErrors, setValidationErrors] = useState({
        name: "",
        surname: "",
        birthDate: ""
    });

    useEffect(() => {
        if (!isEdit) return;

        apiFetch(`http://localhost:5058/api/clients/${id}`)
            .then(async res => {
                return res;
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
        const { name, value } = e.target;

        setForm(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async e => {
        e.preventDefault();

        if (!validateForm()) {
            setError("Popraw błędy w formularzu.");
            return;
        }

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

            if(!data.ok){
                const message = data;
                throw new Error(message || "Błąd zapisu danych");
            }


        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
            navigate("/clients");
        }
    };


    const validateField = (name, value) => {
        let error = "";

        if (name === "name") {
            if (value.trim().length < 2) error = "Imię musi mieć co najmniej 2 znaki.";
            else if (value.length > 50) error = "Imię może mieć maksymalnie 50 znaków.";
        }

        if (name === "surname") {
            if (value.trim().length < 2) error = "Nazwisko musi mieć co najmniej 2 znaki.";
            else if (value.length > 50) error = "Nazwisko może mieć maksymalnie 50 znaków.";
        }

        if (name === "birthDate") {
            const date = new Date(value);
            const today = new Date();
            today.setFullYear(today.getFullYear() - 18);

            if (isNaN(date.getTime())) error = "Podaj prawidłową datę.";
            else if (date > today) error = "Klient musi mieć co najmniej 18 lat.";
        }

        setValidationErrors(prev => ({ ...prev, [name]: error }));
        return error === "";
    };


    const validateForm = () => {
        const results = [
            validateField("name", form.name),
            validateField("surname", form.surname),
            validateField("birthDate", form.birthDate)
        ];

        return results.every(r => r === true);
    };


    const today = new Date();
    today.setFullYear(today.getFullYear() - 18);


    return (
        <>
            <header>
                <h2>{isEdit ? "Edytuj klienta" : "Dodaj klienta"}</h2>
            </header>
            <form onSubmit={handleSubmit}>

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
                        {isEdit ? loading ? "Zapisywanie" : "Zapisz zmiany" : "Dodaj"}
                    </button>
                    {error && <p className="error-msg">{error}</p>}
                </div>

            </form>
        </>
    );
}
