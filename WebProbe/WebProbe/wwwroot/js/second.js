const canvases = document.querySelectorAll(".drawingX");

canvases.forEach((canvas) => {
    const ctx = canvas.getContext("2d");

    canvas.addEventListener("mousemove", (e) => {
        const pos = getMousePos(canvas, e);
        const x = pos.x;
        const y = pos.y;

        ctx.clearRect(0, 0, canvas.width, canvas.height);

        const colors = ["#e74c3c", "#3498db", "#2ecc71", "#f1c40f"];
        const corners = [
            [0, 0],
            [canvas.width, 0],
            [0, canvas.height],
            [canvas.width, canvas.height],
        ];

        corners.forEach((corner, i) => {
            ctx.beginPath();
            ctx.moveTo(corner[0], corner[1]);
            ctx.lineTo(x, y);
            ctx.strokeStyle = colors[i];
            ctx.lineWidth = 2;
            ctx.stroke();
        });
    });

    canvas.addEventListener("mouseleave", () => {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    });
});

function getMousePos(canvas, evt) {
    const rect = canvas.getBoundingClientRect();
    const scaleX = canvas.width / rect.width;
    const scaleY = canvas.height / rect.height;
    return {
        x: (evt.clientX - rect.left) * scaleX,
        y: (evt.clientY - rect.top) * scaleY,
    };
}