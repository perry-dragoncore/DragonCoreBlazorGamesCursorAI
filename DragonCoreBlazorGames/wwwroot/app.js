window.playAudio = function (audioId) {
  let audioElement = document.getElementById(audioId);
  audioElement.currentTime = 0;
  audioElement.play();
};

//window.requestAnimationFrame = callback => {
//    const animationFrame = () => {
//        callback();
//        window.requestAnimationFrame(animationFrame);
//    };
//    window.requestAnimationFrame(animationFrame);
//};


window.requestAnimationFrame = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame;

window.getContext = (canvas, context) => {
    return canvas.getContext(context);
}
