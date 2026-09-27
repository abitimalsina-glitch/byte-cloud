import LoginForm from "../components/LoginForm";

export default function Login() {
    return (
        <div className="min-h-screen flex flex-col overflow-hidden">
            <div className="flex-1 flex flex-col xl:flex-row">
                <main className="w-full xl:w-1/2 flex flex-col items-center xl:items-start px-6 sm:px-10 md:px-16 lg:px-24
                xl:px-20 2xl:px-32 pt-8 xl:pt-10">
                    <img
                        src="/Logo.png"
                        alt="ByteCloud Logo"
                        className="w-40 sm:w-44 md:w-48 xl:w-48 self-start"/>
                        <LoginForm />
                </main>

                <section className="w-full xl:w-1/2 flex justify-center items-center px-6 sm:px-10 md:px-16 py-10 md:py-14 xl:py-0">
                    <img
                        src="/systemarchitect.png"
                        alt="System Architecture"
                        className="w-full max-w-md md:max-w-lg xl:max-w-2xl h-auto object-contain"/>
                </section>
            </div>

            <footer className="w-full shrink-0">
                <img
                    src="/footer.svg"
                    alt="Wave Footer"
                    className="block w-full h-auto"
                />
            </footer>
        </div>
    );
}
