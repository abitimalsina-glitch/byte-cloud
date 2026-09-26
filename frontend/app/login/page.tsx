"use client";

import { useState } from "react";

export default function LoginPage() {
    const [isOn, setisOn]= useState(false);

    function togglePassword() {
        setisOn(!isOn);
    }
    
    return (
    <main className="min-h-screen px-6 mt-10 sm:px-10 md:px-20 lg:px-32 xl:px-50">
        <img src="/Logo.png"
        alt="BitCloud Logo"
        className="w-50"/>
      <form className="w-full max-w-sm">

        <h1 className="text-[#458af1] font-bold text-3xl mt-15">
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
            className="w-full mt-2 border border-blue-300 rounded-md pl-2 pr-2 py-2 text-black"
            placeholder="Enter your Email"/>
        </div>

        <div className="mt-5 flex flex-col">
            <label htmlFor="password">Password</label>
        </div>

        <div className="relative">
            <input
            id="password"
            type={isOn ? "text" : "password"}
            className="w-full mt-2 border border-blue-300 rounded-md pl-2 pr-10 py-2 text-black"
            placeholder="Enter your Password"/>
            
            <button
            type="button"
            onClick={togglePassword}
            className="absolute right-2 top-6 -translate-y-1">
                <img
                src={isOn ? "/openeye.svg" : "/closedeye.svg"}
                className="w-5 h-5"
                alt=""/>
            </button>
        </div>

        <div className="mt-2 text-[#458af1]">
            <p>Forgot your password?</p>
        </div>

        <button
        type="submit"
        className="w-full bg-[#458af1] hover:bg-blue-400 active:bg-blue-350 rounded-lg text-center font-bold mt-4 py-2 text-white"
        >Sign In</button>

      </form>
    </main>
  );
}