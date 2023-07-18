export function close() {
    const sideBar = document.querySelector(".side-bar");
    const icon = document.querySelector(".footer > svg");
    const navMenu = document.querySelector(".nav-menu");

    icon.classList.remove("rotate");
    sideBar.style.width = "4rem";
    sideBar.style.minWidth = "4rem";
    navMenu.style.visibility = "hidden";
}

export async function open() {
    const sideBar = document.querySelector(".side-bar");
    const icon = document.querySelector(".footer > svg");
    const navMenu = document.querySelector(".nav-menu");

    icon.classList.add("rotate");
    sideBar.style.width = "";
    await new Promise(resolve => setTimeout(resolve, 500));
    sideBar.style.minWidth = "15rem";
    navMenu.style.visibility = "";
}

export function expand(item) {
    document.getElementById(item).classList.add("open");
}

export function collaps(item) {
    document.getElementById(item).classList.remove("open");
}
