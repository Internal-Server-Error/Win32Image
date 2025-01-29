namespace Win32Image.Win32Framework;

public static class ImageConstant
{
    /// <summary>
    /// Loads a bitmap.
    /// </summary>
    public const int IMAGE_BITMAP = 0;

    /// <summary>
    /// Loads an icon.
    /// </summary>
    public const int IMAGE_ICON = 1;

    /// <summary>
    /// Loads a cursor.
    /// </summary>
    public const int IMAGE_CURSOR = 2;

    /// <summary>
    /// Loads an enhanced metafile.
    /// </summary>
    public const int IMAGE_ENHMETAFILE = 3;
    /// <summary>
    /// The default flag; it does nothing. All it means is "not LR_MONOCHROME".
    /// </summary>
    public const int LR_DEFAULTCOLOR = 0x0000;

    /// <summary>
    /// Loads the image in black and white.
    /// </summary>
    public const int LR_MONOCHROME = 0x0001;

    /// <summary>
    /// Returns the original hImage if it satisfies the criteria for the copy—that is, correct dimensions and color depth—in 
    /// which case the LR_COPYDELETEORG flag is ignored. If this flag is not specified, a new object is always created.
    /// </summary>
    public const int LR_COPYRETURNORG = 0x0004;

    /// <summary>
    /// Deletes the original image after creating the copy.
    /// </summary>
    public const int LR_COPYDELETEORG = 0x0008;

    /// <summary>
    /// Specifies the image to load. If the hinst parameter is non-NULL and the fuLoad parameter omits LR_LOADFROMFILE, 
    /// lpszName specifies the image resource in the hinst module. If the image resource is to be loaded by name, 
    /// the lpszName parameter is a pointer to a null-terminated string that contains the name of the image resource. 
    /// If the image resource is to be loaded by ordinal, use the MAKEINTRESOURCE macro to convert the image ordinal 
    /// into a form that can be passed to the LoadImage function.
    ///
    /// If the hinst parameter is NULL and the fuLoad parameter omits the LR_LOADFROMFILE value, 
    /// the lpszName specifies the OEM image to load. The OEM image identifiers are defined in Winuser.h and have the following prefixes.
    ///
    /// OBM_ OEM bitmaps 
    /// OIC_ OEM icons 
    /// OCR_ OEM cursors 
    ///
    /// To pass these constants to the LoadImage function, use the MAKEINTRESOURCE macro. For example, to load the OCR_NORMAL cursor, 
    /// pass MAKEINTRESOURCE(OCR_NORMAL) as the lpszName parameter and NULL as the hinst parameter.
    ///
    /// If the fuLoad parameter includes the LR_LOADFROMFILE value, lpszName is the name of the file that contains the image.
    /// </summary>
    public const int LR_LOADFROMFILE = 0x0010;

    /// <summary>
    /// Retrieves the color value of the first pixel in the image and replaces the corresponding entry in the color table 
    /// with the default window color (COLOR_WINDOW). All pixels in the image that use that entry become the default window color. 
    /// This value applies only to images that have corresponding color tables. 
    /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp.
    ///
    /// If fuLoad includes both the LR_LOADTRANSPARENT and LR_LOADMAP3DCOLORS values, LRLOADTRANSPARENT takes precedence. 
    /// However, the color table entry is replaced with COLOR_3DFACE rather than COLOR_WINDOW.
    /// </summary>
    public const int LR_LOADTRANSPARENT = 0x0020;

    /// <summary>
    /// Uses the width or height specified by the system metric values for cursors or icons, 
    /// if the cxDesired or cyDesired values are set to zero. If this flag is not specified and cxDesired and cyDesired are set to zero, 
    /// the function uses the actual resource size. If the resource contains multiple images, the function uses the size of the first image. 
    /// </summary>
    public const int LR_DEFAULTSIZE = 0x0040;

    /// <summary>
    /// Uses true VGA colors.
    /// </summary>
    public const int LR_VGACOLOR = 0x0080;

    /// <summary>
    /// Searches the color table for the image and replaces the following shades of gray with the corresponding 3-D color: Color Replaced with 
    /// Dk Gray, RGB(128,128,128) COLOR_3DSHADOW 
    /// Gray, RGB(192,192,192) COLOR_3DFACE 
    /// Lt Gray, RGB(223,223,223) COLOR_3DLIGHT 
    /// Do not use this option if you are loading a bitmap with a color depth greater than 8bpp. 
    /// </summary>
    public const int LR_LOADMAP3DCOLORS = 0x1000;

    /// <summary>
    /// When the uType parameter specifies IMAGE_BITMAP, causes the function to return a DIB section bitmap rather than a compatible bitmap. 
    /// This flag is useful for loading a bitmap without mapping it to the colors of the display device.
    /// </summary>
    public const int LR_CREATEDIBSECTION = 0x2000;

    /// <summary>
    /// Tries to reload an icon or cursor resource from the original resource file rather than simply copying the current image. 
    /// This is useful for creating a different-sized copy when the resource file contains multiple sizes of the resource. 
    /// Without this flag, CopyImage stretches the original image to the new size. If this flag is set, CopyImage uses the size 
    /// in the resource file closest to the desired size. This will succeed only if hImage was loaded by LoadIcon or LoadCursor, 
    /// or by LoadImage with the LR_SHARED flag.
    /// </summary>
    public const int LR_COPYFROMRESOURCE = 0x4000;

    /// <summary>
    /// Shares the image handle if the image is loaded multiple times. If LR_SHARED is not set, a second call to LoadImage for the 
    /// same resource will load the image again and return a different handle. 
    /// When you use this flag, the system will destroy the resource when it is no longer needed. 
    ///
    /// Do not use LR_SHARED for images that have non-standard sizes, that may change after loading, or that are loaded from a file.
    ///
    /// When loading a system icon or cursor, you must use LR_SHARED or the function will fail to load the resource.
    ///
    /// Windows 95/98/Me: The function finds the first image with the requested resource name in the cache, regardless of the size requested.
    /// </summary>
    public const int LR_SHARED = 0x8000;

}
