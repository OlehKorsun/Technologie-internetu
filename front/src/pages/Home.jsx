import ClientForm from "../components/ClientForm";
import ClientList from "../components/ClientList";
import {useEffect, useState} from "react";

export default function Home() {

    // const [clients, setClients] = useState([]);
    //
    // const fetchClients = async () => {
    //     const res = await fetch("http://localhost:5058/api/clients");
    //     const data = await res.json();
    //     setClients(data);
    // };
    //
    // useEffect(() => {
    //     fetchClients();
    // }, []);

    return (
        <>
            <header>
                <h1>Strona główna</h1>
            </header>
            <main>
                {/*<ClientList clients={clients} />*/}
                {/*<ClientForm onClientAdded={fetchClients} />*/}
                Zarządzaj klientami, barberami i wizytami wygodnie!!!
            </main>
        </>
    );
}