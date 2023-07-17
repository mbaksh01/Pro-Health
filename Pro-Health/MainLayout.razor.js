export function close() {
    const sideBar = document.querySelector(".side-bar");
    const icon = document.querySelector(".side-bar > svg");

    icon.classList.remove("rotate");
    sideBar.style.width = "4rem";
    sideBar.style.minWidth = "4rem";
}

export async function open() {
    const sideBar = document.querySelector(".side-bar");
    const icon = document.querySelector(".side-bar > svg");

    icon.classList.add("rotate");
    sideBar.style.width = "";
    await new Promise(resolve => setTimeout(resolve, 500));
    sideBar.style.minWidth = "15rem";
}

export function expand(item) {
    document.getElementById(item).classList.add("open");
}

export function collaps(item) {
    document.getElementById(item).classList.remove("open");
}
