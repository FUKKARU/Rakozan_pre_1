using Ex;

public static class GameData
{
    public static int Difficulty = 0; // E, N, H.

    static int hotaruNum = 2;
    public static int HotaruNum
    {
        get { return hotaruNum; }
        set { hotaruNum = value.Clamp(0, 5); }
    }
}