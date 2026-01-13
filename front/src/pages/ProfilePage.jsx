import { useAuth } from "../auth/AuthContext";
import { useEffect, useState } from "react";
import { apiFetch } from "../api/api";

export default function ProfilePage() {
    const { user } = useAuth();
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!user?.id) return;

        setLoading(true);
        setError("");

        apiFetch(`http://localhost:5058/api/clients/user/${user.id}`)
            .then(setData)
            .catch(err => {
                setError(err.message || "Błąd ładowania danych");
            })
            .finally(() => setLoading(false));
    }, [user]);

    if (loading) return <p>Ładowanie...</p>;
    if (error) return <p>Błąd: {error}</p>;
    if (!data) return <p>Brak danych użytkownika.</p>;

    return (
        <div className="details-card">
            <h2>Moje dane</h2>
            <div className="card">
                <p><b>Imię:</b> {data.name}</p>
                <p><b>Nazwisko:</b> {data.surname}</p>
                <p><b>Data urodzenia:</b> {data.birthDate}</p>
                <p><b>Email:</b> {data.email}</p>
            </div>
        </div>
    );
}
