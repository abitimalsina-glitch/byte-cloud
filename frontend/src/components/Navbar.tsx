const links = [
    { name: "Home", href: "/Home", icon: "/icons/home.svg" },
    { name: "Storage", href: "/Storage", icon: "/icons/storage.svg" },
    { name: "File Logs", href: "/Logs", icon: "/icons/logs.svg" },
    { name: "Notification", href: "", icon: "/icons/notification.svg" },
    { name: "Settings", href: "", icon: "/icons/settings.svg" }
];

export default function Navbar() {
    return (
        <nav className="min-h-screen w-60 bg-blue-500">
             <div className="flex flex-col items-center">
                <label className="mt-3 cursor-pointer">
                <div className="flex h-40 w-40 items-center justify-center rounded-full bg-gray-400 hover:bg-gray-300">
                <span className="text-sm text-gray-700 font-bold">Add Photo</span>
                </div>
                <input
                type="file"
                accept="image/*"
                className="hidden"/>
                </label>
                </div>

            <div className="text-white">
                <h1 className="mt-2 text-center font-bold text-xl">Username</h1>
                <p className="mt-3 text-center">example@gmail.com</p>
            </div>
            <div className="mt-3 h-[2px] w-full bg-white mb-2"></div>
            <div className="flex flex-col">
                {links.map((link) => (
                    <a
                        key={link.name}
                        href={link.href}
                        className="group flex w-full items-center gap-3 px-5 py-4 text-white hover:bg-white hover:text-blue-500">
                        <span
                            className="h-5 w-5 bg-current"
                            style={{
                                mask: `url(${link.icon}) center / contain no-repeat`,
                                WebkitMask: `url(${link.icon}) center / contain no-repeat`,
                            }}/>
                        <span>{link.name}</span>
                    </a>
                ))}
            </div>
        </nav>
    );
}