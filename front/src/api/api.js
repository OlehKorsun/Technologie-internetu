export async function apiFetch(url, options = {}) {

    const token = localStorage.getItem("token");

    const response = await fetch(url, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            ...(token && { Authorization: `Bearer ${token}` }),
            ...options.headers,
        }
    });

    if (response.status === 401) {
        localStorage.removeItem("token");
        window.location.href = "/login";
        throw new Error("Unauthorized");
    }

    if (response.status === 403) {
        window.location.href = "/forbidden";
        throw new Error("Forbidden");
    }

    if (response.status === 204) return [];

    const text = await response.text();
    try {
        return text ? JSON.parse(text) : [];
    } catch (err) {
        console.error("Błąd parsowania JSON:", text);
        return [];
    }

}
