import {Link} from "react-router-dom";

export default function VisitList({ visits, onDelete }) {
    if(!visits) return (<p>Ładowanie...</p>)

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
            <Link to={`/visit/add`} className="btn btn-add">
                Dodaj wizytę
            </Link>
        </article>
    );
}