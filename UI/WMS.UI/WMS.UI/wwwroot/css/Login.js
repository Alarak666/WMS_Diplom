function generateBubbles() {
    var bubblesContainer = document.getElementById("bubbles-container");
    var numBubbles = getRandomInt(10, 16);

    for (var i = 0; i < numBubbles; i++) {
        var bubble = document.createElement("div");
        bubble.classList.add("bubble");
        bubble.style.top = getRandomInt(0, 100) + "%";
        bubble.style.left = getRandomInt(0, 100) + "%";
        bubblesContainer.appendChild(bubble);
    }
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}