export default function ByteCloud() {
  return (
    <>
    <img
        src="/Logo.png"
        alt="ByteCloud Logo"
        className="w-40 sm:w-44 md:w-48 xl:w-48 self-start"/>
    
    <main className="min-h-screen bg-slate-50 text-slate-900">
      <section className="flex min-h-[50vh] items-center justify-center px-6 text-center">
        <div className="max-w-3xl">
          <h1 className="text-5xl font-bold tracking-tight sm:text-6xl text-blue-500">
            Welcome to ByteCloud
          </h1>

          <p className="mt-6 text-lg leading-8 text-slate-600 sm:text-xl">
            Your personal cloud storage. Store, manage, and access
            your files from one simple place.
          </p>

          <div className="mt-8 flex justify-center gap-4">
            <a
              href="/register"
              className="rounded-lg bg-blue-600 px-6 py-3 font-semibold text-white transition hover:bg-blue-700">
              Get Started
            </a>

            <a
              href="/login"
              className="rounded-lg border border-slate-300 bg-white px-6 py-3 font-semibold text-slate-900 transition hover:bg-slate-100">
              Login
            </a>
          </div>

        </div>
      </section>

      <section className="px-6">
        <div className="mx-auto max-w-6xl">

          <h2 className="text-center text-3xl font-bold text-blue-500">
            Everything You Need
          </h2>

          <div className="mt-7 grid gap-6 md:grid-cols-3">

            <div className="rounded-xl border border-slate-200 bg-white p-8 shadow-sm">
              <h3 className="text-xl font-semibold">
                Secure Storage
              </h3>

              <p className="mt-3 leading-7 text-slate-600">
                Keep your personal files stored securely in
                your own cloud environment.
              </p>
            </div>

            <div className="rounded-xl border border-slate-200 bg-white p-8 shadow-sm">
              <h3 className="text-xl font-semibold">
                Easy Management
              </h3>

              <p className="mt-3 leading-7 text-slate-600">
                Upload, organize, and manage your files through
                a simple interface.
              </p>
            </div>

            <div className="rounded-xl border border-slate-200 bg-white p-8 shadow-sm">
              <h3 className="text-xl font-semibold">
                Personal Cloud
              </h3>

              <p className="mt-3 leading-7 text-slate-600">
                Have control over your own storage instead of
                relying entirely on third-party cloud services.
              </p>
            </div>

          </div>
        </div>
      </section>

      <footer className="w-full shrink-0">
                <img
                    src="/footer.svg"
                    alt="Wave Footer"
                    className="block w-full h-auto"
                />
            </footer>
    </main>
    </>
  );
}
