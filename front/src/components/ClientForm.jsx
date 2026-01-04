// import {useState} from "react";
//
// export default function ClientForm({onClientAdded}) {
//     const [name, setName] = useState("");
//     const [surname, setSurname] = useState("");
//     const [birthdate, setBirthdate] = useState("");
//
//     const handleSubmit = async (e) => {
//         e.preventDefault();
//
//         const newClient = {
//             name,
//             surname,
//             birthdate,
//         };
//
//
//         try{
//             const response = await fetch("http://localhost:5058/api/clients", {
//                 method: "POST",
//                 headers: {"Content-Type": "application/json"},
//                 body: JSON.stringify(newClient),
//             });
//
//             if(!response.ok) {
//                 alert(await response.text());
//                 return;
//
//             }
//             alert("Klient dodany!");
//             setName("");
//             setSurname("");
//             setBirthdate("");
//             if(onClientAdded) onClientAdded();
//         } catch (err){
//             alert("Błąd!");
//         }
//     };
//
//
//     return (
//         <article className="registration_container">
//             <form onSubmit={handleSubmit}>
//                 <h2>Dodaj klienta</h2>
//
//                 <div className="form_row">
//                     <div className="form_group">
//                         <label>Imię</label>
//                         <input
//                             type="text"
//                             value={name}
//                             onChange={e => setName(e.target.value)}
//                         />
//                     </div>
//
//                     <div className="form_group">
//                         <label>Nazwisko</label>
//                         <input
//                             type="text"
//                             value={surname}
//                             onChange={e => setSurname(e.target.value)}
//                         />
//                     </div>
//
//                     <div className="form_group">
//                         <label>Data urodzenia</label>
//                         <input
//                             type="date"
//                             value={birthdate}
//                             onChange={e => setBirthdate(e.target.value)}
//                         />
//                     </div>
//                 </div>
//
//                 <div className="form_action">
//                     <button type="submit">Dodaj</button>
//                 </div>
//             </form>
//         </article>
//     );
//
// }



import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

export default function ClientForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEdit = !!id;

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const [form, setForm] = useState({
        name: "",
        surname: "",
        birthDate: ""
    });

    useEffect(() => {
        if (!isEdit) return;

        fetch(`http://localhost:5058/api/clients/${id}`)
            .then(async res => {
                if (!res.ok) {
                    const msg = await res.text();
                    throw new Error(msg);
                }
                return res.json();
            })
            .then(data => {
                setForm({
                    name: data.name,
                    surname: data.surname,
                    birthDate: data.birthDate.slice(0, 10)
                });
            })
            .catch(err => setError(err.message));
    }, [id, isEdit]);

    const handleChange = e => {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        const url = isEdit
            ? `http://localhost:5058/api/clients/${id}`
            : "http://localhost:5058/api/clients";

        const method = isEdit ? "PUT" : "POST";

        try {
            const res = await fetch(url, {
                method,
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(form)
            });

            if (!res.ok) {
                const message = await res.text();
                throw new Error(message || "Błąd zapisu danych");
            }

            navigate("/clients");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const today = new Date();
    today.setFullYear(today.getFullYear() - 18);


    return (
        <form onSubmit={handleSubmit}>
            <h2>{isEdit ? "Edytuj klienta" : "Dodaj klienta"}</h2>

            <input
                name="name"
                value={form.name}
                onChange={handleChange}
                placeholder="Imię"
                required
            />

            <input
                name="surname"
                value={form.surname}
                onChange={handleChange}
                placeholder="Nazwisko"
                required
            />



            <input
                type="date"
                name="birthDate"
                value={form.birthDate}
                onChange={handleChange}
                min="1900-01-01"
                max={today.toISOString().split("T")[0]}
                required
            />

            <button type="submit" disabled={loading}>
                {isEdit ? loading ? "Zapisywanie" : "Zapisz zmiany" : "Dodaj"}
            </button>

            {error && <p className="error">{error}</p>}
        </form>
    );
}
