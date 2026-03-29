using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using XPaint.Resources;

namespace XPaint.Converters;

public class TypeToInstanceConverter : IValueConverter
{
    private readonly Dictionary<Type, object> CachedInstances = new();

    private static Page BuildErrorPage(string message)
    {
        return new Page
        {
            Content = new TextBlock
            {
                Text = message,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Margin = new System.Windows.Thickness(12)
            }
        };
    }

    private object GetPage(Type pageType)
    {
        if (CachedInstances.TryGetValue(pageType, out var cachedInstance))
        {
            return cachedInstance;
        }

        try
        {
            var newInstance = Activator.CreateInstance(pageType);

            if (newInstance is null)
            {
                return BuildErrorPage($"Failed to create page instance for '{pageType.FullName}'.");
            }

            CachedInstances[pageType] = newInstance;
            return newInstance;
        }
        catch (Exception ex)
        {
            return BuildErrorPage($"Failed to load page '{pageType.FullName}': {ex.Message}");
        }
    }
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter is not Type pageType)
        {
            return BuildErrorPage("Invalid page mapping in ExpandersView.");
        }

        return GetPage(pageType);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}