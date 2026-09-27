import { useState } from "react";

export default function LoginForm() {
    const [isOn, setIsOn] = useState(false);

    function togglePassword() {
        setIsOn(!isOn);
    }
    return (
        <form className="w-full max-w-sm mt-12 sm:mt-14 md:mt-16 xl:mt-20">
            <h1 className="text-[#458af1] font-bold text-3xl md:text-4xl">
            Welcome Back
            </h1>
            <p className="text-[#458af1] mt-1 text-sm md:text-base">
            Please enter your details to continue
            </p>
            <div className="mt-6 flex flex-col">
                <label htmlFor="email">Email</label>
                <input
                    id="email"
                    type="email"
                    className="w-full mt-2 border border-blue-300 rounded-md px-3 py-2.5 text-black outline-none focus:border-[#458af1]"
                    placeholder="Enter your Email"/>
            </div>
            
            <div className="mt-5 flex flex-col">
                <label htmlFor="password">Password</label>
            </div>
            
            <div className="relative">
                <input
                    id="password"
                    type={isOn ? "text" : "password"}
                    className="w-full mt-2 border border-blue-300 rounded-md pl-3 pr-10 py-2.5 text-black outline-none
                    focus:border-[#458af1]"
                    placeholder="Enter your Password"/>
                <button
                    type="button"
                    onClick={togglePassword}
                    className="absolute right-2.5 top-1/2 -translate-y-[25%]">
                    <img
                        src={isOn ? "/openeye.svg" : "/closedeye.svg"}
                        className="w-5 h-5"
                        alt="Toggle Password Visibility"/>
                </button>
            </div>
            
            <div className="mt-2 text-[#458af1] text-sm">
                <p>Forgot your password?</p>
            </div>
            
            <button
                type="submit"
                className="w-full bg-[#458af1] hover:bg-blue-400 active:bg-blue-500 rounded-lg text-center font-bold mt-5 py-2.5
                text-white transition-colors">
                Sign In
            </button>
        </form>
    );
}
