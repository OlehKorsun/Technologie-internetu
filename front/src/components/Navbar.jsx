import {NavLink} from "react-router-dom";

export default function Navbar(){
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
        </nav>
    );
}