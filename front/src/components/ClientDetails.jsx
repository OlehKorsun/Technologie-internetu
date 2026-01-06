import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";

export default function ClientDetails(){
    const {id} = useParams();
    const [client, setClient] = useState(null);
    const [loading, setLoading] = useState(true);
    const [visits, setVisits] = useState([]);

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

    useEffect(() => {
        const loadVisits = async () => {
            try{
                const response = await fetch(`http://localhost:5058/api/visits/client/${id}`);

                if(response.status === 404){
                    setVisits([]);
                } else {
                    const data = await response.json();
                    setVisits(data);
                }
            } catch (err){
                setVisits(null);
            }
        };
        loadVisits();
    }, [id]);

    if(loading){
        return <p>Ładuję...</p>;
    }

    if(!client){
        return <h2>Nie znaleziono klienta o id {id}</h2>
    }


    return (
        <>
            <div className="details-card">
                <h2>Szczegóły klienta</h2>

                <div className="card">
                    <p><strong>Imię:</strong> {client.name}</p>
                    <p><strong>Nazwisko:</strong> {client.surname}</p>
                    <p><strong>Data urodzenia:</strong> {client.birthDate}</p>
                </div>

                <Link to="/clients" className="btn btn-add">Powrót</Link>
            </div>

            <h3>Powiązane wizyty</h3>
            <table>
                <thead>
                <tr>
                    <th>Początek</th>
                    <th>Koniec</th>
                    <th>Imię klienta</th>
                    <th>Imię barbera</th>
                    <th>Cena</th>
                </tr>
                </thead>
                <tbody>
                {visits.map(v => (
                    <tr key={v.visitId}>
                        <td>{v.start}</td>
                        <td>{v.end}</td>
                        <td>{v.clientName}</td>
                        <td>{v.barberName}</td>
                        <td>{v.price}</td>
                    </tr>
                ))}
                </tbody>
            </table>

        </>
    );
}