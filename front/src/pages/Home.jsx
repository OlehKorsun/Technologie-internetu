export default function Home() {

    return (
        <>
            <header>
                <h1>Strona główna</h1>
            </header>
            <main>
                <article>

                    <h2>O naszym barbershopie</h2>

                    <p>
                        Witamy w <strong>Barbershopie „Broda i Wąsy”</strong> – miejscu, gdzie tradycja spotyka się z
                        nowoczesnością. Nasz salon to przestrzeń stworzona z pasją do klasycznego golenia i stylizacji
                        męskich fryzur. Naszym celem jest nie tylko zadbać o Twoje włosy i zarost, ale także zapewnić Ci
                        relaksującą atmosferę, która sprawi, że każda wizyta u nas będzie przyjemnością.
                    </p>

                    <p>
                        Nasi barberzy to doświadczeni profesjonaliści, którzy łączą fachową wiedzę z zamiłowaniem do
                        sztuki strzyżenia i golenia. Stawiamy na indywidualne podejście do każdego klienta – doradzimy
                        Ci, jaką fryzurę wybrać, aby pasowała do Twojego stylu i kształtu twarzy. Dzięki nowoczesnym
                        technikom i tradycyjnym narzędziom, zapewniamy doskonały efekt za każdym razem.
                    </p>

                    <p>
                        W naszym barbershopie poczujesz się jak w męskim świecie pełnym klasyki – od wnętrza, które
                        oddaje ducha dawnych barbershopów, po relaksującą muzykę, która wprowadzi Cię w odpowiedni
                        nastrój. Jesteśmy tu, by dostarczyć Ci nie tylko perfekcyjnych usług fryzjerskich, ale także
                        niezapomnianych wrażeń.
                    </p>

                    <p>
                        Dołącz do naszej rodziny zadowolonych klientów i przekonaj się, że <strong>„Broda i
                        Wąsy”</strong> to coś więcej niż tylko barbershop – to miejsce, w którym mężczyźni czują się
                        wyjątkowo.
                    </p>

                </article>

                <article>
                    <h2>Galeria</h2>
                    <div className="gallery">
                        <img src="/images/image1.jpg" alt="Strzyżenie 1"/>
                        <img src="/images/image2.jpg" alt="Strzyżenie 2"/>
                        <img src="/images/image3.jpg" alt="Zarost 1"/>
                        <img src="/images/image4.jpg" alt="Zarost 2"/>
                        <img src="/images/image5.jpg" alt="Wnętrze barbershopu"/>
                        <img src="/images/image6.jpg" alt="Przed/Po strzyżenie"/>
                    </div>
                </article>

                <article>
                    <h2>Nasza lokalizacja</h2>
                    <div id="map-container">
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2235.5531826410456!2d20.975121633496684!3d52.25692517788233!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x471ecb001faa7a1b%3A0x7b486793a9c32ed8!2sWESTBIL%20BARBERSHOP!5e1!3m2!1sru!2spl!4v1764000679223!5m2!1sru!2spl"
                            width="600" height="450" allowFullScreen="" loading="lazy"
                            referrerPolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </article>

            </main>


        </>
    );
}