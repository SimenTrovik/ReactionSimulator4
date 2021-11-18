using System.Windows.Media;

namespace SoftwareDesignExam
{
    public static class Colors
    {
        public static SolidColorBrush Green { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("Green");
        public static SolidColorBrush Red { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("DarkRed");
        public static SolidColorBrush Yellow { get; } = (SolidColorBrush)new BrushConverter().ConvertFromString("Yellow");
    }
}