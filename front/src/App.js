import {Route, Routes} from "react-router-dom";
import Home from "./pages/Home";
import About from "./pages/About";
import ClientDetails from "./components/ClientDetails";
import NotFound from "./pages/NotFound";
import Navbar from "./components/Navbar";
import "./styles.css"
import Footer from "./components/Footer";
import BarberDetails from "./components/BarberDetails";
import VisitDetails from "./components/VisitDetails";
import ClientsPage from "./pages/ClientsPage";
import BarbersPage from "./pages/BarbersPage";
import VisitsPage from "./pages/VisitsPage";
import ClientForm from "./components/ClientForm";
import BarberForm from "./components/BarberForm";
import VisitForm from "./components/VisitForm";
import PrivateRoute from "./auth/PrivateRoute";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";

function App() {
    return (
        <>
            <Navbar/>

            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/register" element={<RegisterPage/>}/>
                <Route path="/login" element={<LoginPage/>}/>
                <Route path="/about" element={<About/>}/>
                <Route path="/clients" element={
                    <PrivateRoute roles={["admin"]}>
                        <ClientsPage/>
                    </PrivateRoute>
                }/>
                <Route path="/barbers" element={
                    <PrivateRoute roles={["admin"]}>
                        <BarbersPage/>
                    </PrivateRoute>
                }/>
                <Route path="/visits" element={
                    <PrivateRoute roles={["admin"]}>
                        <VisitsPage/>
                    </PrivateRoute>}/>

                <Route path="/client/:id" element={<ClientDetails/>}/>
                <Route path="/barber/:id" element={
                    <PrivateRoute roles={["admin"]}>
                        <BarberDetails/>
                    </PrivateRoute>}/>
                <Route path="/visit/:id" element={<VisitDetails/>}/>

                <Route path="/client/add" element={<ClientForm/>}/>
                <Route path="/client/edit/:id" element={<ClientForm/>}/>

                <Route path="/barber/add" element={<BarberForm/>}/>
                <Route path="/barber/edit/:id" element={<BarberForm/>}/>

                <Route path="/visit/add" element={<VisitForm/>}/>
                <Route path="/visit/edit/:id" element={<VisitForm/>}/>

                <Route path="*" element={<NotFound/>}/>
            </Routes>

            <Footer />

        </>
    );
}

export default App;
