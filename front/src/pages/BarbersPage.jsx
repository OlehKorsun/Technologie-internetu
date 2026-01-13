import React, {useEffect, useState} from "react";
import BarberList from "../components/BarberList";
import {apiFetch} from "../api/api";

export default function BarbersPage() {
    const [barbers, setBarbers] = useState(null);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);

    const pageSize = 5;

    const loadBarbers = async (pageNumber = 1) => {
        setLoading(true);
        try{
            const data = await apiFetch(`http://localhost:5058/api/barbers?page=${pageNumber}&pageSize=${pageSize}`);
            setBarbers(data.records);
            setTotalPages(data.totalPages);
        } catch (err) {
            console.error(err);
            setBarbers([]);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadBarbers(page);
    }, [page]);

    const deleteBarber = async (id) => {
        if (!window.confirm("Czy na pewno usunąć barbera?")) return;

        await apiFetch(`http://localhost:5058/api/barbers/${id}`);

        setBarbers(prev => prev.filter(b => b.barberId !== id));
    };


    return <BarberList
        barbers={barbers}
        onDelete={deleteBarber}
        page={page}
        setPage={setPage}
        totalPages={totalPages}
        loading={loading}
    />
}