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


function showLoading() {
    document.getElementById('loadingSpinner').style.display = 'flex';
}

function hideLoading() {
    document.getElementById('loadingSpinner').style.display = 'none';
}


window.playSound = (soundId) => {
    const sound = document.getElementById(soundId);
    if (sound) {
        sound.currentTime = 0;
        sound.play().catch(err => console.log('Error playing sound:', err));
    }
}

window.scrollToBottom = (elementClassName) => {
    const elements = document.getElementsByClassName(elementClassName);
    if (elements && elements.length > 0) {
        const element = elements[0];
        element.scrollTop = element.scrollHeight;
    }
}

window.typeWriter = (elementId, text, speed) => {
    const element = document.getElementById(elementId);
    if (!element) return;

    element.style.opacity = "0";
    element.innerHTML = "";
    element.style.opacity = "1";

    let i = 0;
    const type = () => {
        if (i < text.length) {
            element.innerHTML += text.charAt(i);
            i++;
            setTimeout(type, speed);
        }
    };
    type();
};

window.playBackgroundMusic = (volume = 0.3) => {
    const music = document.getElementById('bgMusic');
    if (music) {
        music.volume = volume;
        music.loop = true;
        music.play().catch(err => console.log('Error playing music:', err));
    }
};

window.toggleBackgroundMusic = () => {
    const music = document.getElementById('bgMusic');
    const btn = document.getElementById('musicToggle');
    if (music) {
        if (music.paused) {
            music.play();
            btn.innerHTML = '<i class="fas fa-volume-up"></i>';
        } else {
            music.pause();
            btn.innerHTML = '<i class="fas fa-volume-mute"></i>';
        }
    }
};

window.focusElement = (element) => {
    if (element) {
        element.focus();
    }
};

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Store the start time
    const startTime = Date.now();
    const minLoadTime = 5000; // 3 seconds minimum

    // Store the original Blazor start function
    const originalStart = Blazor.start;

    // Override Blazor's start function
    Blazor.start = function() {
        // Return a promise that chains our delay and the original start
        return new Promise((resolve) => {
            const currentTime = Date.now();
            const elapsedTime = currentTime - startTime;
            const remainingTime = Math.max(0, minLoadTime - elapsedTime);

            console.log(`Waiting for ${remainingTime}ms before starting Blazor...`);

            setTimeout(async () => {
                // Call the original start function and resolve with its result
                const result = await originalStart.apply(Blazor, arguments);
                resolve(result);
            }, remainingTime);
        });
    };
});
