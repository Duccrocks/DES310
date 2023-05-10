public class MiniGameProgression
{
    public static bool KelpieGameCompleted = false;
    public static bool MQoSCompleted = false;

    public static bool HasWon()
    {
        return KelpieGameCompleted && MQoSCompleted;
    }
}
