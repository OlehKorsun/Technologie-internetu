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

            <NavLink
                to="/clients"
                className={({ isActive }) => isActive ? "active" : ""}>
                Klienci
            </NavLink>

            <NavLink
            to="/barbers"
            className={({ isActive }) => isActive ? "active" : ""}>
                Barberzy
            </NavLink>

            <NavLink
            to="/visits"
            className={({ isActive }) => isActive ? "active" : ""}>
                Wizyty
            </NavLink>

            {!user && <NavLink to="/login">Login</NavLink>}

            {user && (
                <>
                    <span>{user.login}</span>
                    <button onClick={logout}>Wyloguj</button>
                </>
            )}

        </nav>
    );
}