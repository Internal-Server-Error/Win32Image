
namespace Win32Image;

public class Program
{
    static int Main(string[] args)
    {
        string? errorMessage;
        if (args == null || args.Length < 1)
        {
            errorMessage = "Error: First argument must be the image path!" + Environment.NewLine;
            return ErrorWindow.Open(errorMessage);
        }

        var imagePath = args[0];

        //TODO add support for jpg/png
        if (!imagePath.EndsWith(".bmp"))
        {
            errorMessage = "Error: File format not supported. Supported format: .bmp" + Environment.NewLine;
            return ErrorWindow.Open(errorMessage);
        }

        return ImageWindow.Open(imagePath);
    }

}
