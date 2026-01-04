import {Link, useParams} from "react-router-dom";
import {useEffect, useState} from "react";

export default function BarberDetails(){
    const {id} = useParams();
    const [barber, setBarber] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadBarbers = async () => {
            try{
                const response = await fetch(`http://localhost:5058/api/barbers/${id}`);

                if(response.status === 404){
                    setBarber(null);
                } else{
                    const data = await response.json();
                    setBarber(data);
                }
            } catch (err){
                setBarber(null);
            } finally {
                setLoading(false);
            }
        };
        loadBarbers();
    }, [id]);

    if(loading){
        return <p>Ładuję...</p>
    }

    if(!barber){
        return <h2>Nie znaleziono barbera o id {id}</h2>;
    }

    return (
        <div className="details-card">
            <h2>Szczegóły barbera</h2>
            <div className="card">
                <p>Imię: {barber.name}</p>
                <p>Nazwisko: {barber.surname}</p>
                <p>Data urodzenia: {barber.birthDate}</p>
            </div>

            <Link to="/barbers" className="btn btn-add">Powrót</Link>
        </div>
    );
}