import React, {useEffect, useState} from "react";
import VisitList from "../components/VisitList";
import {apiFetch} from "../api/api";
import {useAuth} from "../auth/AuthContext";

export default function VisitsPage() {
    const {user} = useAuth();
    const [visits, setVisits] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);

    const pageSize = 5;

    const loadVisits = async (user, pageNumber = 1) => {
        setLoading(true);
        try{
            const url = user.role === "admin"
                ? `http://localhost:5058/api/visits?page=${pageNumber}&pageSize=${pageSize}`
                : `http://localhost:5058/api/visits/user/${user.id}?page=${pageNumber}&pageSize=${pageSize}`;

            const data = await apiFetch(url);
               setVisits(data.records);
               setTotalPages(data.totalPages);
        } catch (err){
            console.error(err);
            setVisits([]);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadVisits(user, page)
    }, [user, page]);

    const deleteVisit = async (id) => {
        if (!window.confirm("Czy na pewno usunąć wizytę?")) return;

        await apiFetch(`http://localhost:5058/api/visits/${id}`, {
            method: "DELETE"
        });

        setVisits(prev => prev.filter(b => b.visitId !== id));
    };


    return (
        <VisitList
            visits={visits}
            onDelete={deleteVisit}
            page={page}
            setPage={setPage}
            totalPages={totalPages}
            loading={loading}
        />);
}