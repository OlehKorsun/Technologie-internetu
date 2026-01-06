import {useAuth} from "../auth/AuthContext";
import {useEffect, useState} from "react";

export default function ProdilePage(){
    const { token } = useAuth();
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        fetch("http://localhost:5058/api/users/me", {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(res => {
                if (!res.ok) throw new Error("Błąd ładowania danych");
                return res.json();
            })
            .then(setData)
            .catch(err => setError(err.message))
            .finally(() => setLoading(false));
    }, [token]);

    if (loading) return <p>Ładowanie...</p>;
    if (error) return <p>Błąd: {error}</p>;

    return (
        <div>
            <h2>Moje dane</h2>
            <p><b>Imię:</b> {data.firstName}</p>
            <p><b>Nazwisko:</b> {data.lastName}</p>
            <p><b>Email:</b> {data.email}</p>
            <p><b>Telefon:</b> {data.phoneNumber}</p>
        </div>
    );
}