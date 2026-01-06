import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {apiFetch} from "../api/api";

export default function VisitForm() {

    const {id} = useParams();
    const navigate = useNavigate();
    const isEdit = !!id;

    const [clients, setClients] = useState([]);
    const [barbers, setBarbers] = useState([]);

    const [form, setForm] = useState({
        start: "",
        end: "",
        comment: "",
        clientId: "",
        barberId: "",
        price: ""
    });

    useEffect(() => {
        apiFetch("http://localhost:5058/api/clients")
            .then((res) => res.json())
            .then(setClients);

        apiFetch("http://localhost:5058/api/barbers")
            .then((res) => res.json())
            .then(setBarbers);

        if (isEdit) {
            apiFetch(`http://localhost:5058/api/visits/${id}`)
                .then(async res => {
                    if (!res.ok) {
                        const text = await res.text();
                        throw new Error(text || "Błąd pobierania wizyty");
                    }
                    return res.json();
                })
                .then(data => setForm(mapVisitToForm(data)))
                .catch(err => alert(err.message));
        }
    }, [id, isEdit]);

    const handleChange = (e) => {
        setForm({...form, [e.target.name]: e.target.value})
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

        const url = isEdit
            ? `http://localhost:5058/api/visits/${id}`
            : "http://localhost:5058/api/visits";

        const method = isEdit ? "PUT" : "POST";

        const res = await apiFetch(url, {
            method,
            headers: {"Content-Type": "application/json"},
            body: JSON.stringify(form)
        });

        if (!res.ok) {
            alert("Błąd zapisu wizyty!");
            return;
        }

        navigate("/visits");
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>{isEdit ? "Edytuj wizytę" : "Dodaj wizytę"}</h2>

            <select name="clientId" value={form.clientId} onChange={handleChange} required>
                <option value="">Wybierz klienta</option>
                {clients.map((client) => (
                    <option key={client.clientId} value={client.clientId}>
                        {client.name} {client.surname}
                    </option>

                ))}
            </select>

            <select name="barberId" value={form.barberId} onChange={handleChange} required>
                <option value="">Wybierz barbera</option>
                {barbers.map((barber) => (
                    <option key={barber.barberId} value={barber.barberId}>
                        {barber.name} {barber.surname}
                    </option>
                ))}
            </select>
            
            <input type="datetime-local" name="start" value={form.start} onChange={handleChange} required></input>
            <input type="datetime-local" name="end" value={form.end} onChange={handleChange} required></input>
            <input type="number" name="price" value={form.price} onChange={handleChange} required></input>

            <textarea name="comment" value={form.comment} onChange={handleChange}></textarea>

            <button type="submit">Zapisz</button>

        </form>
    );
}