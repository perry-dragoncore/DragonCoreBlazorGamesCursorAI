namespace DragonCoreBlazorGames;

public class PongGame
{
    private const double MAX_BOUNCE_ANGLE = Math.PI / 3; // Maximum angle at which ball can bounce off paddle
    public float HEIGHT = 300;
    private readonly Random _random = new Random();
    private readonly float BallSpeed = 5;
    private readonly float _paddleSpeed = 10;
    private readonly float _aiPaddleOffset = 50;

    public Ball Ball { get; private set; }
    public PlayerPaddle Player1Paddle { get; set; }
    public PlayerPaddle Player2Paddle { get; private set; }

    private float BallXVelocity = 1;
    private float BallYVelocity = 1;

    public PongGame()
    {
        Reset();
    }

    public void Reset()
    {
        Ball = new Ball(new Rect(400, 300, 20, 20));
        Player1Paddle = new PlayerPaddle(40, 250);// new Rect(50, 250, 20, 100);
        Player2Paddle = new PlayerPaddle(730, 250); // new Rect(730, 250, 20, 100);
        BallXVelocity = _random.NextDouble() > 0.5 ? 1 : -1;
        BallYVelocity = _random.Next(-2, 2);
    }

    public void Update(double elapsedSeconds)
    {
        // Update ball position
        Ball.Update(elapsedSeconds);

        // Check for ball-paddle collision
        if (Ball.Left < 0 || Ball.Left + Ball.Width > 400)
        {
            // Ball has hit the left or right wall, bounce horizontally
            Ball.BounceHorizontally();
        }
        else if (Ball.Top < 0 || Ball.Top + Ball.Height > 300)
        {
            // Ball has hit the top or bottom wall, bounce vertically
            Ball.BounceVertically();
        }
        else if ((Ball.VelocityX < 0 && Ball.Left < Player1Paddle.Bounds.Right && Ball.CenterY >= Player1Paddle.Bounds.Top && Ball.CenterY <= Player1Paddle.Bounds.Bottom) ||
                 (Ball.VelocityX > 0 && Ball.Left > Player2Paddle.Bounds.Left && Ball.CenterY >= Player2Paddle.Bounds.Top && Ball.CenterY <= Player2Paddle.Bounds.Bottom))
        {
            // Ball has hit a paddle, bounce horizontally and adjust vertical velocity
            Ball.BounceHorizontally();
            var relativeIntersectY = (Player1Paddle.Bounds.Top + Player1Paddle.PADDLE_HEIGHT / 2) - Ball.CenterY;
            var normalizedRelativeIntersectionY = relativeIntersectY / (Player1Paddle.PADDLE_HEIGHT / 2);
            var bounceAngle = normalizedRelativeIntersectionY * MAX_BOUNCE_ANGLE;
            Ball.VelocityX = Math.Sign(Ball.VelocityX) * Ball.Speed * Math.Cos(bounceAngle);
            Ball.VelocityY = Ball.Speed * -Math.Sin(bounceAngle);
        }

        // Update paddle positions
        Player1Paddle.Update(elapsedSeconds);
        Player2Paddle.Update(elapsedSeconds);
    }
}

public class PlayerPaddle
{
    private const float PADDLE_SPEED = 200; // Speed at which paddle moves
    public float PADDLE_HEIGHT = 80; // Height of paddle in pixels
    private const float PADDLE_WIDTH = 20; // Width of paddle in pixels
    private const float PADDLE_OFFSET = 40; // Distance of paddle from edge of screen in pixels

    public Rect Bounds { get; private set; } // Bounding rectangle of paddle

    public PlayerPaddle(float x, float y)
    {
        Bounds = new Rect(x, y, PADDLE_WIDTH, PADDLE_HEIGHT);
    }

    public void Update(double elapsedSeconds)
    {
        // Move paddle up or down based on input
        var deltaY = 0.0;

        //if (KeyboardState.IsKeyDown(Keys.Up))
        //{
        //    deltaY -= PADDLE_SPEED * elapsedSeconds;
        //}

        //if (KeyboardState.IsKeyDown(Keys.Down))
        //{
        //    deltaY += PADDLE_SPEED * elapsedSeconds;
        //}

        // Clamp paddle to screen bounds
        var newY = Math.Clamp(Bounds.Top + deltaY, PADDLE_OFFSET, 300 - PADDLE_HEIGHT - PADDLE_OFFSET);

        Bounds = new Rect(Bounds.Left, (float)newY, PADDLE_WIDTH, PADDLE_HEIGHT);
    }

    //public void Draw(Canvas2DContext context)
    //{
    //    context.FillRect(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
    //}
}


public class Ball
{
    public double Speed { get; set; } // Speed of the ball

    public Rect Bounds { get; private set; }
    public double VelocityX { get; set; } = 200;
    public double VelocityY { get; set; } = 200;

    public Ball(Rect bounds)
    {
        Bounds = bounds;
    }

    public void Update(double elapsedSeconds)
    {
        Bounds = new Rect(
            Convert.ToSingle(Bounds.Left + VelocityX * elapsedSeconds),
            Convert.ToSingle(Bounds.Top + VelocityY * elapsedSeconds),
            Bounds.Width,
            Bounds.Height);
    }

    public void BounceVertically()
    {
        VelocityY *= -1;
    }

    public void BounceHorizontally()
    {
        VelocityX *= -1;
    }

    public double Left
    {
        get { return Bounds.Left; }
    }

    public double Top
    {
        get { return Bounds.Top; }
    }

    public double Width
    {
        get { return Bounds.Width; }
    }

    public double Height
    {
        get { return Bounds.Height; }
    }

    public double CenterX
    {
        get { return Bounds.Left + Bounds.Width / 2; }
    }

    public double CenterY
    {
        get { return Bounds.Top + Bounds.Height / 2; }
    }
}
