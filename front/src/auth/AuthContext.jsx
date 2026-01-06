import {createContext, useContext, useEffect, useState} from "react";

const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(
        localStorage.getItem("token") || ""
    );


    const parseJwt = (token) => {
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(
                atob(base64)
                    .split('')
                    .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
                    .join('')
            );
            return JSON.parse(jsonPayload);
        } catch (e) {
            return null;
        }
    };

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            const decoded = parseJwt(token);
            if (decoded) {
                setUser({
                    login: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                    role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                    token
                });
            }
        }
    }, []);

    // useEffect(() => {
    //     if (!token) {return}
    //
    //     const payload = JSON.parse(atob(token.split('.')[1]));
    //
    //     setUser({
    //         login: payload.unique_name,
    //         role: payload.role,
    //     });
    // }, [token]);

    const login = (token) => {
        localStorage.setItem("token", token);
        const decoded = parseJwt(token);
        if (decoded) {
            setUser({
                login: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                token
            });
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, token, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
}

export const useAuth = () => useContext(AuthContext);