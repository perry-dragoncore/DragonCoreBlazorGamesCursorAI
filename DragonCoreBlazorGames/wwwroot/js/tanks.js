// Export functions explicitly
let canvas;
let ctx;
let tileSize = 30;
let tankImage1;
let tankImage2;
let imagesLoaded = false;

// Add the loadImage helper function
function loadImage(img, src) {
    return new Promise((resolve, reject) => {
        img.onload = () => resolve();
        img.onerror = () => reject(new Error(`Failed to load image: ${src}`));
        img.src = src;
    });
}


export function initGame(canvasElement, dotNetHelper) {
    canvas = canvasElement;
    ctx = canvas.getContext('2d');
    
    // Pre-load tank images
    tankImage1 = new Image();
    tankImage2 = new Image();
    
    Promise.all([
        loadImage(tankImage1, '/images/tank-green.png'),
        loadImage(tankImage2, '/images/tank-red.png')
    ]).then(() => {
        imagesLoaded = true;
    }).catch(error => {
        console.error('Failed to load tank images:', error);
        imagesLoaded = false;
    });
}

export function renderGame(gameState) {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    
    // Draw map
    for (let y = 0; y < gameState.map.length; y++) {
        for (let x = 0; x < gameState.map[y].length; x++) {
            if (gameState.map[y][x] === 1) {
                ctx.fillStyle = '#666';
                ctx.fillRect(x * tileSize, y * tileSize, tileSize, tileSize);
            }
        }
    }
    
    // Draw tanks
    drawTank(gameState.player1, tankImage1);
    drawTank(gameState.player2, tankImage2);
    
    // Draw bullets
    gameState.bullets.forEach(bullet => {
        ctx.beginPath();
        ctx.fillStyle = '#fff';
        ctx.arc(
            bullet.x * tileSize, 
            bullet.y * tileSize, 
            2, 0, Math.PI * 2
        );
        ctx.fill();
    });

    // Draw FPS in top-right corner of canvas
    ctx.font = '16px monospace';
    ctx.fillStyle = '#00FF00';
    ctx.textAlign = 'right';
    ctx.textBaseline = 'top';
    ctx.fillText(`FPS: ${gameState.fps}`, canvas.width - 10, 10);
}

function drawTank(tank, image) {
    ctx.save();
    ctx.translate(tank.x * tileSize, tank.y * tileSize);
    ctx.rotate(tank.rotation * Math.PI / 180);
    
    if (imagesLoaded) {
        ctx.drawImage(
            image,
            -tileSize/2,
            -tileSize/2,
            tileSize,
            tileSize
        );
    } else {
        // Fallback rectangle tank
        ctx.fillStyle = image === tankImage1 ? '#00ff00' : '#ff0000';
        ctx.fillRect(-tileSize/2, -tileSize/2, tileSize, tileSize);
        // Draw tank barrel
        ctx.fillRect(0, -2, tileSize/2, 4);
    }
    
    ctx.restore();
}

// Add these functions to handle animations
export function createBulletImpact(x, y, playerNumber) {
    const impact = document.createElement('div');
    impact.className = `bullet-impact-p${playerNumber}`;
    const rect = canvas.getBoundingClientRect();
    impact.style.left = `${rect.left + (x * tileSize)}px`;
    impact.style.top = `${rect.top + (y * tileSize)}px`;
    document.body.appendChild(impact);
    
    impact.addEventListener('animationend', () => impact.remove());
}

export function createTankExplosion(x, y, playerNumber) {
    const explosion = document.createElement('div');
    explosion.className = `tank-explosion-p${playerNumber}`;
    const rect = canvas.getBoundingClientRect();
    explosion.style.left = `${rect.left + (x * tileSize)}px`;
    explosion.style.top = `${rect.top + (y * tileSize)}px`;
    document.body.appendChild(explosion);
    
    explosion.addEventListener('animationend', () => explosion.remove());
} 