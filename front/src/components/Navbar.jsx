import {NavLink} from "react-router-dom";
import {useAuth} from "../auth/AuthContext";

export default function Navbar(){
    const {user, logout} = useAuth();
    return (
        <nav>
            <NavLink
                to={"/"}
                className={({ isActive }) => (isActive ? "active" : "")}>
                Strona główna
            </NavLink>

            {user && user.role === "user" && (
                <>
                    <NavLink to="/visits" className={({ isActive }) => isActive ? "active" : ""}>Moje wizyty</NavLink>
                    <NavLink to="/profile" className={({ isActive }) => isActive ? "active" : ""}>Moje dane</NavLink>
                </>
            )}

            {user && user.role === "admin" && (
                <>
                    <NavLink to="/clients" className={({ isActive }) => isActive ? "active" : ""}>Klienci</NavLink>
                    <NavLink to="/barbers" className={({ isActive }) => isActive ? "active" : ""}>Barberzy</NavLink>
                    <NavLink to="/visits" className={({ isActive }) => isActive ? "active" : ""}>Wszystkie wizyty</NavLink>
                </>
            )}

            {!user && (
                <>
                    <NavLink to="/login" className={({ isActive }) => isActive ? "active" : ""}>Zaloguj się</NavLink>
                    <NavLink to="/register" className={({ isActive }) => isActive ? "active" : ""}>Zarejestruj się</NavLink>
                </>
            )}

            {user && (
                <>
                    <span>{user.login}</span>
                    <button onClick={logout}>Wyloguj</button>
                </>
            )}

        </nav>
    );
}