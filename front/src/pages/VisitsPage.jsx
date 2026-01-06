import React, {useEffect, useState} from "react";
import VisitList from "../components/VisitList";
import {apiFetch} from "../api/api";
import {useAuth} from "../auth/AuthContext";

export default function VisitsPage() {
    const {user} = useAuth();
    const [visits, setVisits] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if(!user) return;

        const url = user.role === "admin"
            ? `http://localhost:5058/api/visits`
            : `http://localhost:5058/api/visits/user/${user.id}`;


        apiFetch(url)
        .then(data => setVisits(data))
        .catch(err => {
            console.log(err);
            setVisits([]);
        })
            .finally(() => setLoading(false));
    }, [user]);

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

    if(loading) {return <p>Ładowanie...</p>}
    if(!visits || visits.length === 0) return <p>Brak wizyt do wyświetlenia</p>;

    return (<VisitList visits={visits} onDelete={deleteVisit} />);
}