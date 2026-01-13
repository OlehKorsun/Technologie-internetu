import { Link } from "react-router-dom";

export default function ClientList({ clients, onDelete, page, setPage, totalPages, loading }) {

    if (loading) return <p>Ładowanie...</p>;
    if (!clients || clients.length === 0)
        return (
            <div>
                <p>Brak klientów</p>
                <button disabled={page <= 1} onClick={() => setPage(page - 1)}>« Poprzednia</button>
            </div>
        );

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
                                onClick={() => onDelete(c.clientId)}>
                                Usuń
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>

            <div className="pagination">
                <button disabled={page <= 1} onClick={() => setPage(prev => prev - 1)}>« Poprzednia</button>
                <span>Strona {page} z {totalPages}</span>
                <button disabled={page >= totalPages} onClick={() => setPage(prev => prev + 1)}>Następna »</button>
            </div>

            <Link to={`/client/add`} className="btn btn-add">
                Dodaj klienta
            </Link>
        </article>
    );
}
