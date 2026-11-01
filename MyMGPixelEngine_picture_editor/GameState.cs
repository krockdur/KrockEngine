
namespace MyMGPixelEngine;
internal static class GameState
{
    private static bool _isPlaying = false;
    private static bool _isWritingTextBox = false;
    public static bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            if (value == true)
            {
                _isPlaying = true;
                _isWritingTextBox = false;
            }
        }
    }

    public static bool IsWritingTextBox
    {
        get => _isWritingTextBox;
        set
        {
            if (value == true)
            {
                _isPlaying = false;
                _isWritingTextBox = true;
            }
        }
    }

}