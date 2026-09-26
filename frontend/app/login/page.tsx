export default function LoginPage() {
  return (
    <main className="min-h-screen px-6 pt-30 sm:px-10 md:px-20 lg:px-32 xl:px-50">
      <form className="w-full max-w-sm">
        <h1 className="text-[#458af1] font-bold text-3xl">
          Welcome Back
        </h1>

        <p className="text-[#458af1] mt-1">
          Please enter your details to continue
        </p>

        <div className="mt-5 flex flex-col">
            <label htmlFor="email">Email</label>
            
            <input
            id="email"
            type="email"
            className="w-full mt-2 bg-gray-200 border border-blue-300 rounded-md px-2 py-1"
            placeholder="Enter your Email"/>
        </div>

        <div className="mt-5 flex flex-col">
            <label htmlFor="password">Password</label>
            <input
            id="password"
            type="password"
            className="w-full mt-2 bg-gray-200 border border-blue-300 rounded-md px-2 py-1"
            placeholder="Enter your Email"/>
        </div>

        <div className="mt-2 text-[#458af1]">
            <p>Forgot your password?</p>
        </div>

        <button
        type="submit"
        className="w-full bg-[#458af1] hover:scale-90 rounded-md text-center font-bold mt-4 py-2"
        >Sign In</button>

      </form>
    </main>
  );
}