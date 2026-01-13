import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { apiFetch } from "../api/api";

export default function VisitForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEdit = !!id;

    const [clients, setClients] = useState([]);
    const [barbers, setBarbers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [form, setForm] = useState({
        start: "",
        end: "",
        comment: "",
        clientId: "",
        barberId: "",
        price: ""
    });

    const now = new Date().toISOString().slice(0, 16);

    const [validationErrors, setValidationErrors] = useState({});

    useEffect(() => {
        const loadData = async () => {
            try {
                const clientsData = await apiFetch("http://localhost:5058/api/clients");
                setClients(clientsData.records);

                const barbersData = await apiFetch("http://localhost:5058/api/barbers");
                setBarbers(barbersData.records);

                if (isEdit) {
                    const visitData = await apiFetch(`http://localhost:5058/api/visits/${id}`);
                    setForm(mapVisitToForm(visitData));
                }
            } catch (err) {
                setError(err.message || "Błąd ładowania danych");
            } finally {
                setLoading(false);
            }
        };

        loadData();
    }, [id, isEdit]);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    function mapVisitToForm(data) {
        return {
            start: data.start ? data.start.slice(0, 16) : "",
            end: data.end ? data.end.slice(0, 16) : "",
            clientId: data.clientId ?? "",
            barberId: data.barberId ?? "",
            price: data.price ?? "",
            comment: data.comment ?? ""
        };
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        if(!validateForm(form)) {
            setError("Popraw błędy w formularzu.");
            return;
        }

        setLoading(true);
        setError(null);


        try {
            await apiFetch(
                isEdit
                    ? `http://localhost:5058/api/visits/${id}`
                    : "http://localhost:5058/api/visits",
                {
                    method: isEdit ? "PUT" : "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify(form)
                }
            );

            navigate("/visits");

        } catch (err) {
            if (err.errors) {
                setError(err.errors);
                setValidationErrors(err.errors);
            } else {
                setError( "Wystąpił błąd podczas zapisu." );
            }
        } finally {
            setLoading(false);
        }
    };

    const validateForm = () => {
        const checks = [
            validateField("start", form.start),
            validateField("end", form.end),
            validateField("barberId", form.barberId)
        ];


        checks.push(validateField("clientId", form.clientId));
        checks.push(validateField("price", form.price));


        const start = new Date(form.start);
        const end = new Date(form.end);

        if (start >= end) {
            setValidationErrors(prev => ({
                ...prev,
                end: "Data zakończenia musi być późniejsza niż rozpoczęcia."
            }));
            return false;
        }

        return checks.every(c => c === true);
    };

    const validateField = (name, value) => {
        let message = "";

        if (name === "start" && !value) {
            message = "Podaj początek wizyty.";
        }

        if (name === "end" && !value) {
            message = "Podaj koniec wizyty.";
        }

        if (name === "price") {
            if (value <= 0) message = "Cena musi być większa od 0.";
        }

        setValidationErrors(prev => ({
            ...prev,
            [name]: message
        }));

        return message === "";
    };

    if (loading) return <p>Ładowanie...</p>;
    if (error) return <p className="error">{error}</p>;

    return (
        <>
            <header>
                <h2>{isEdit ? "Edytuj wizytę" : "Dodaj wizytę"}</h2>
            </header>
            <form onSubmit={handleSubmit}>

                <div className="form_row">
                        <select name="clientId" value={form.clientId} onChange={handleChange} required>
                            <option value="">Wybierz klienta</option>
                            {clients.map((client) => (
                                <option key={client.clientId} value={client.clientId}>
                                    {client.name} {client.surname}
                                </option>
                            ))}
                        </select>

                        {validationErrors.clientId && <p className="error-msg">{validationErrors.clientId}</p>}

                        <select name="barberId" value={form.barberId} onChange={handleChange} required>
                            <option value="">Wybierz barbera</option>
                            {barbers.map((barber) => (
                                <option key={barber.barberId} value={barber.barberId}>
                                    {barber.name} {barber.surname}
                                </option>
                            ))}
                        </select>

                        {validationErrors.barberId && <p className="error-msg">{validationErrors.barberId}</p>}

                </div>


                <div className="form_row">

                        <input
                            type="datetime-local"
                            name="start"
                            min={now}
                            value={form.start}
                            onChange={handleChange}
                            required
                        />
                    {validationErrors.start && <p className="error-msg">{validationErrors.start}</p>}

                        <input
                            type="datetime-local"
                            name="end"
                            min={now}
                            value={form.end}
                            onChange={handleChange}
                            required
                        />
                    {validationErrors.end && <p className="error-msg">{validationErrors.end}</p>}

                </div>

                <div className="form_row">
                    <input type="number" name="price" value={form.price} onChange={handleChange} required />
                    {validationErrors.price && <p className="error-msg">{validationErrors.price}</p>}
                </div>


                <div className="form_row">
                    <textarea name="comment" value={form.comment} onChange={handleChange}></textarea>
                </div>


                <div className="form_action">
                    <button type="submit" disabled={loading}>
                        {isEdit ? "Zapisz zmiany" : "Dodaj"}
                    </button>
                    {error && <p className="error-msg">{error}</p>}
                </div>

            </form>
        </>
    );
}
