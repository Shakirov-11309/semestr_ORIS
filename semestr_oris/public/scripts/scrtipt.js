const buttonFilter1 = document.getElementById("button-click-1");
const imageByFilter1 = document.getElementById("imgClick1");

buttonFilter1.addEventListener("click", (e) => {
    buttonFilter1.click()
    if(buttonFilter1.classList.contains("active"))
    {
        alert("Лох наелся блох")
        imageByFilter1.src = "images/button-open.png";
    }
    else
    {
        imageByFilter1.src = "images/button-close.png";
    }
})