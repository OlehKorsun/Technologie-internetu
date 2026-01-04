import {Link} from "react-router-dom";

export default function BarberList({ barbers, onDelete }) {
    if(!barbers){ return (<p>Ładowanie...</p>);}

    return (
        <article>
            <div className="table-header">
                <h2>Lista barberów</h2>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Imię i nazwisko</th>
                        <th>Akcje</th>
                    </tr>
                </thead>
                <tbody>
                {barbers.map(b => (
                    <tr key={b.barberId}>
                        <td>{b.name} {b.surname}</td>

                        <td className="actions">
                            <Link to={`/barber/${b.barberId}`} className="btn btn-add">
                                Szczegóły
                            </Link>

                            <Link to={`/barber/edit/${b.barberId}`} className="btn btn-edit">
                                Edytuj
                            </Link>

                            <button
                                className="btn btn-delete"
                                onClick={() => onDelete(b.barberId)}
                            >
                                Usuń
                            </button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
            <Link to={`/barber/add`} className="btn btn-add">
                Dodaj barbera
            </Link>
        </article>
    );
}