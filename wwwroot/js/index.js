window.addEventListener("load", addPushButton)

function addPushButton()
{
    let button = document.createElement("button");
    button.textContent = "Push?";
    button.addEventListener("click", addBubble);

    document.body.appendChild(button);
}



function addBubble()
{
    let div = document.createElement("div");
    div.classList.add("bubble")

    //div.style.right = 0;
    //div.style.top = 0;
    let x = Math.random() * 80 + 10;
    let y = Math.random() * 80 + 10;
    let size = Math.random() * 40 + 20;

    div.style.width = size + "px";
    div.style.height = size + "px";

    div.style.translate = x + "vw " + y + "vh";

    setTimeout(() => deleteBubble(div), 4000);

    document.body.appendChild(div);
}

function deleteBubble(bubble)
{
    bubble.remove();
}