import React, { useEffect, useState } from "react";
import { apiFetch } from "../api/api";
import ClientList from "../components/ClientList";

export default function ClientsPage() {
    const [clients, setClients] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);

    const pageSize = 5;

    const loadClients = async (pageNumber = 1) => {
        setLoading(true);
        try {
            const data = await apiFetch(`http://localhost:5058/api/clients?page=${pageNumber}&pageSize=${pageSize}`);
            setClients(data.records);
            setTotalPages(data.totalPages);
        } catch (err) {
            console.error(err);
            setClients([]);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadClients(page);
    }, [page]);

    const deleteClient = async (id) => {
        if (!window.confirm("Czy na pewno usunąć klienta?")) return;

        await apiFetch(`http://localhost:5058/api/clients/${id}`, { method: "DELETE" });

        loadClients(page);
    };

    return (
        <ClientList
            clients={clients}
            onDelete={deleteClient}
            page={page}
            setPage={setPage}
            totalPages={totalPages}
            loading={loading}
        />
    );
}
