window.addEventListener('keydown', event => {
    if (event.code === 'ArrowUp') {
        DotNet.invokeMethodAsync('Pong', 'MovePlayer1Paddle', -10);
    } else if (event.code === 'ArrowDown') {
        DotNet.invokeMethodAsync('Pong', 'MovePlayer1Paddle', 10);
    }
});
