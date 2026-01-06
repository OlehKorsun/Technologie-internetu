import React, {useEffect, useState} from "react";
import BarberList from "../components/BarberList";
import {apiFetch} from "../api/api";

export default function BarbersPage() {
    const [barbers, setBarbers] = useState(null);

    useEffect(() => {
        apiFetch("http://localhost:5058/api/barbers")
            .then(data => setBarbers(data))
        .catch(err => {
            console.error(err);
            setBarbers([]);
        })
    }, []);

    const deleteBarber = async (id) => {
        if (!window.confirm("Czy na pewno usunąć barbera?")) return;

        const res = await apiFetch(`http://localhost:5058/api/barbers/${id}`, {
            method: "DELETE"
        });

        if (!res.ok) {
            const msg = await res.text();
            alert(msg);
            return;
        }

        setBarbers(prev => prev.filter(b => b.barberId !== id));
    };


    return <BarberList barbers={barbers} onDelete={deleteBarber} />
}