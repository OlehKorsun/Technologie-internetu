import {useAuth} from "./AuthContext";
import {Navigate} from "react-router-dom";

export default function PrivateRoute({ roles, children }) {
    const { user } = useAuth();

    if (!user) return <Navigate to="/login" />;

    if(roles && !roles.includes(user.role))
        return <Navigate to="/forbidden" />;

    return children;
}