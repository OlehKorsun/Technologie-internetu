import React, {useEffect, useState} from "react";
import ClientList from "../components/ClientList";
import {apiFetch} from "../api/api";

export default function ClientsPage() {
    const [clients, setClients] = useState(null);

    useEffect(() => {
        apiFetch("http://localhost:5058/api/clients")
            .then(data => setClients(data))
        .catch(err => {
            console.error(err);
            setClients([]);
        });
    }, []);

    const deleteClient = async (id) => {
        if (!window.confirm("Czy na pewno usunąć klienta?")) return;

        const res = await apiFetch(`http://localhost:5058/api/clients/${id}`, {
            method: "DELETE"
        });

        if (!res.ok) {
            const msg = await res.text();
            alert(msg);
            return;
        }

        setClients(prev => prev.filter(c => c.clientId !== id));
    };

    return <ClientList clients={clients} onDelete={deleteClient} />
}