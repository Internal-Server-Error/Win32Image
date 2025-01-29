namespace Win32Image.Win32Framework;

public enum MessageBoxResult : uint
{
    Ok = 1,
    Cancel,
    Abort,
    Retry,
    Ignore,
    Yes,
    No,
    Close,
    Help,
    TryAgain,
    Continue,
    Timeout = 32000
}
