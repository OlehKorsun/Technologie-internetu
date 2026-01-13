import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {apiFetch} from "../api/api";
import {useAuth} from "../auth/AuthContext";

export default function VisitDetails() {
    const {id} = useParams();
    const {user} = useAuth();
    const [visit, setVisit] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() =>{
        const url = user.role === "admin"
            ? `http://localhost:5058/api/visits/${id}`
            : `http://localhost:5058/api/visits/user/${id}/${user.id}`;
        const loadVisits = async () => {
            try{
                const response = await apiFetch(url);

                if(response.status === 404){
                    setVisit(null);
                } else {
                    setVisit(response)
                }
            } catch (err) {
                setVisit(null);
            } finally {
                setLoading(false);
            }
        };
        loadVisits();
    }, [id, user]);

    if(loading) return <p>Ładowanie...</p>
    if(!visit) return <h2>Nie znaleziono wizyty o id {id}</h2>

    return (
        <>
            <div className="details-card">
                <h2>Szczegóły wizyty</h2>

                <div className="card">
                    <p>Początek: {visit.start}</p>
                    <p>Koniec: {visit.end}</p>
                    <p>Komentarz: {visit.comment}</p>
                    <p>Imię klienta: {visit.clientName}</p>
                    <p>Imię barbera: {visit.barberName}</p>
                    <p>Cena: {visit.price}</p>
                </div>

            </div>
            <div className="btm-bottom">
                <Link to="/visits" className="btn btn-add">Powrót</Link>
            </div>
        </>
    );
}