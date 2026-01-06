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

    // Obsługa błędów
    if (response.status === 401) {
        localStorage.removeItem("token");
        window.location.href = "/login";
        throw new Error("Unauthorized");
    }

    if (response.status === 403) {
        window.location.href = "/forbidden";
        throw new Error("Forbidden");
    }

    if (response.status === 204) {
        return null; // No Content
    }

    // Spróbuj sparsować JSON, jeśli jest
    const contentType = response.headers.get("content-type");
    if (contentType && contentType.includes("application/json")) {
        return await response.json();
    }

    return null;
}
