import {Link} from "react-router-dom";
import React from "react";

export default function VisitList({ visits, onDelete, page, setPage, totalPages, loading }) {

    if(loading) {return <p>Ładowanie...</p>}
    if(!visits || visits.length === 0) return (
        <div>
            <p>Brak wizyt</p>
            <button disabled={page <= 1} onClick={() => setPage(page - 1)}>« Poprzednia</button>
        </div>
    );

    return (
        <article>
            <div className="table-header">
                <h2>Lista wizyt</h2>
            </div>

            <table>
                <thead>
                    <tr>
                        <th>Początek</th>
                        <th>Koniec</th>
                        <th>Imię klienta</th>
                        <th>Imię barbera</th>
                        <th>Cena</th>
                        <th>Akcje</th>
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
                        <td className="actions">
                            <Link to={`/visit/${v.visitId}`} className="btn btn-add">
                                Szczegóły
                            </Link>
                            <Link to={`/visit/edit/${v.visitId}`} className="btn btn-edit">
                                Edytuj
                            </Link>

                            <button className="btn btn-delete" onClick={() => onDelete(v.visitId)}>
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

            <Link to={`/visit/add`} className="btn btn-add">
                Dodaj wizytę
            </Link>
        </article>
    );
}