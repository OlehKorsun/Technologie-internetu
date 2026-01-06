import React, { useEffect, useState } from "react";
import VisitList from "../components/VisitList";
import { apiFetch } from "../api/api";
import { useAuth } from "../auth/AuthContext";

export default function VisitsPage() {
    const { user } = useAuth();
    const [visits, setVisits] = useState([]);
    const [loading, setLoading] = useState(true);

    console.log("Rola zalogowanego użytkownika:", user?.role);


    useEffect(() => {
        if (!user) return;
        console.log('userid: ', user?.id);
        const userId = user.id;
        const url =
            user.role === "admin"
                ? `http://localhost:5058/api/visits`
                : `http://localhost:5058/api/visits/user/${userId}`;

        apiFetch(url)
            .then(data => {
                setVisits(Array.isArray(data) ? data : []);
            })
            .catch(err => {
                console.error(err);
                setVisits([]);
            })
            .finally(() => setLoading(false));
    }, [user]);


    const deleteVisit = async (id) => {
        if (!window.confirm("Czy na pewno usunąć wizytę?")) return;

        try {
            const res = await apiFetch(`http://localhost:5058/api/visits/${id}`, {
                method: "DELETE",
            });

            if (!res.ok) {
                const msg = await res.text();
                alert(msg);
                return;
            }

            setVisits((prev) => prev.filter((v) => v.visitId !== id));
        } catch (err) {
            alert("Błąd przy usuwaniu wizyty!");
        }
    };

    if (loading) return <p>Ładowanie...</p>;
    if (!Array.isArray(visits) || visits.length === 0)
        return <p>Brak wizyt do wyświetlenia</p>;

    return <VisitList visits={visits} onDelete={deleteVisit} />;
}
