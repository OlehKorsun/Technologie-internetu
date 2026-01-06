import React, {useEffect, useState} from "react";
import VisitList from "../components/VisitList";
import {apiFetch} from "../api/api";

export default function VisitsPage() {
    const [visits, setVisits] = useState([]);

    useEffect(() => {
        apiFetch("http://localhost:5058/api/visits")
        .then(data => setVisits(data))
        .catch(err => {
            console.log(err);
            setVisits([]);
        });
    }, []);

    const deleteVisit = async (id) => {
        if (!window.confirm("Czy na pewno usunąć wizytę?")) return;

        const res = await apiFetch(`http://localhost:5058/api/visits/${id}`, {
            method: "DELETE"
        });

        if (!res.ok) {
            const msg = await res.text();
            alert(msg);
            return;
        }

        setVisits(prev => prev.filter(b => b.visitId !== id));
    };

    return (<VisitList visits={visits} onDelete={deleteVisit} />);
}