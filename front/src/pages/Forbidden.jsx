export default function Forbidden() {
    return (
        <>
            <div className="error-card">
                <h2 className="error-code">403</h2>
                <p className="error-message">Nie masz uprawnień.</p>
            </div>
        </>
    );
}