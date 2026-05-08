using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;

namespace Anemone;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ServiceStore.RootFrame = rootFrame;
        rootFrame.Navigate(typeof(ProjectsView));

        IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

        appWindow.Resize(new SizeInt32(1050, 700));

        ExtendsContentIntoTitleBar = true;
    }
}
