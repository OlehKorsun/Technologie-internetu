import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";

export default function ClientDetails(){
    const {id} = useParams();
    const [client, setClient] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const loadClients = async () => {
            try{
                const response = await fetch(`http://localhost:5058/api/clients/${id}`);

                if(response.status === 404){
                    setClient(null);
                } else {
                    const data = await response.json();
                    setClient(data);
                }
            } catch (err){
                setClient(null);
            } finally {
                setLoading(false);
            }
        };

        loadClients();
    }, [id]);

    if(loading){
        return <p>Ładuję...</p>;
    }

    if(!client){
        return <h2>Nie znaleziono klienta o id {id}</h2>
    }


    return (
        <div className="details-card">
            <h2>Szczegóły klienta</h2>

            <div className="card">
                <p><strong>Imię:</strong> {client.name}</p>
                <p><strong>Nazwisko:</strong> {client.surname}</p>
                <p><strong>Data urodzenia:</strong> {client.birthDate}</p>
            </div>

            <Link to="/clients" className="btn btn-add">Powrót</Link>
        </div>
    );
}