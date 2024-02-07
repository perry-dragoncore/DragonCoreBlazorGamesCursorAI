namespace DragonCoreBlazorGames;

public class Rect
{
    public float Left { get; set; }
    public float Top { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public float Right => Left + Width;
    public float Bottom => Top + Height;

    public Rect(float left, float top, float width, float height)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }

    public bool Intersects(Rect other)
    {
        return Left < other.Right && Right > other.Left
            && Top < other.Bottom && Bottom > other.Top;
    }
}
