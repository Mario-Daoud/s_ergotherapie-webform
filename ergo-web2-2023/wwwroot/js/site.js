const setup = () => {
    const themeIcon = document.getElementById("themeIcon");
    const tooltipIcons = document.getElementsByClassName("hint_image");
    const body = document.body;
    const listItem = document.getElementsByClassName("listItem");
    let isDarkMode = (document.cookie.indexOf("darkmode=true") !== -1);

    const switchTheme = (newIsDarkMode) => {
        isDarkMode = newIsDarkMode;
        if (isDarkMode) {
            body.style.backgroundColor = "#303234";
            body.style.color = "white";
            for (let i = 0; i < listItem.length; i++) {
                listItem[i].style.color = "white";
                
            }
            for (let i = 0; i < tooltipIcons.length; i++) {
                tooltipIcons[i].src = "/images/tooltip_icon_inverted.png"
            }
            themeIcon.src = "/images/sun.png";
            
        } else {
            body.style.backgroundColor = "white";
            body.style.color = "black";
            for (let i = 0; i < listItem.length; i++) {
                listItem[i].style.color = "black";
                
            }

            for (let i = 0; i < tooltipIcons.length; i++) {
                tooltipIcons[i].src = "/images/tooltip_icon.png"
                
            }
            themeIcon.src = "/images/moon.png";
            
        }
        document.cookie = "darkmode=" + isDarkMode + "; path=/";
    };

    switchTheme(isDarkMode);

    themeIcon.addEventListener("click", () => {
        const newIsDarkMode = !isDarkMode;
        switchTheme(newIsDarkMode);
    });
};

window.addEventListener("load", setup);
