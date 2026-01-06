import {Link} from "react-router-dom";

export default function ClientList({ clients, onDelete }) {

    if(!clients) return (<p>Ładowanie...</p>);

    if (clients.length === 0)  return <p>Brak klientów</p>;

    return (
        <article>
            <div className="table-header">
                <h2>Lista klientów</h2>

            </div>

            <table>
                <thead>
                <tr>
                    <th>Imię i nazwisko</th>
                    <th>Akcje</th>
                </tr>
                </thead>

                <tbody>
                {clients.map(c => (
                    <tr key={c.clientId}>
                        <td>{c.name} {c.surname}</td>

                        <td className="actions">
                            <Link to={`/client/${c.clientId}`} className="btn btn-add">
                                Szczegóły
                            </Link>

                            <Link to={`/client/edit/${c.clientId}`} className="btn btn-edit">
                                Edytuj
                            </Link>

                            <button
                                className="btn btn-delete"
                                onClick={() => onDelete(c.clientId)}
                            >
                                Usuń
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>

            <Link to={`/client/add`} className="btn btn-add">
                Dodaj klienta
            </Link>
        </article>
    );
}