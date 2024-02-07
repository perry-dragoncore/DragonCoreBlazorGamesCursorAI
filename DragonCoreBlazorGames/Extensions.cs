namespace DragonCoreBlazorGames;

public static class EnumerableExtensions
{
    private static readonly Random rng = new Random();

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        var elements = source.ToArray();
        for (int i = elements.Length - 1; i >= 0; i--)
        {
            int j = rng.Next(i + 1);
            yield return elements[j];
            elements[j] = elements[i];
        }
    }
}

public static class Utils
{
    public static void Shuffle<T>(T[] array)
    {
        Random rng = new Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T temp = array[k];
            array[k] = array[n];
            array[n] = temp;
        }
    }
}