using Android.OS;
using Android.Util;
using Google.Android.Material.Color;
using Microsoft.Maui.ApplicationModel;

using AndroidColor = Android.Graphics.Color;
using MauiColor   = Microsoft.Maui.Graphics.Color;
using MauiApp     = Microsoft.Maui.Controls.Application;

namespace ConsumoYPropina.Platforms.Android.Services;

public static class DynamicColorService
{
    public static void Apply()
    {
        if (Build.VERSION.SdkInt < BuildVersionCodes.S)
            return;

        var activity = Platform.CurrentActivity;
        if (activity is null) return;
        DynamicColors.ApplyToActivityIfAvailable(activity);

        var theme = activity.Theme;
        var resources = MauiApp.Current?.Resources;
        if (theme is null || resources is null) return;

        var tv = new TypedValue();

        MauiColor Read(int attr)
        {
            theme.ResolveAttribute(attr, tv, true);
            var c = new AndroidColor(tv.Data);
            return MauiColor.FromRgba(c.R, c.G, c.B, c.A);
        }

        // Calcular primero los colores fuera del hilo UI
        var dict = new System.Collections.Generic.Dictionary<string, MauiColor>()
        {
            // Primary
            ["Primary"] = Read(Resource.Attribute.colorPrimary),
            ["OnPrimary"] = Read(Resource.Attribute.colorOnPrimary),
            ["PrimaryContainer"] = Read(Resource.Attribute.colorPrimaryContainer),
            ["OnPrimaryContainer"] = Read(Resource.Attribute.colorOnPrimaryContainer),

            // Secondary
            ["Secondary"] = Read(Resource.Attribute.colorSecondary),
            ["OnSecondary"] = Read(Resource.Attribute.colorOnSecondary),
            ["SecondaryContainer"] = Read(Resource.Attribute.colorSecondaryContainer),
            ["OnSecondaryContainer"] = Read(Resource.Attribute.colorOnSecondaryContainer),

            // Tertiary
            ["Tertiary"] = Read(Resource.Attribute.colorTertiary),
            ["OnTertiary"] = Read(Resource.Attribute.colorOnTertiary),
            ["TertiaryContainer"] = Read(Resource.Attribute.colorTertiaryContainer),
            ["OnTertiaryContainer"] = Read(Resource.Attribute.colorOnTertiaryContainer),

            // Error
            ["Error"] = Read(Resource.Attribute.colorError),
            ["OnError"] = Read(Resource.Attribute.colorOnError),
            ["ErrorContainer"] = Read(Resource.Attribute.colorErrorContainer),
            ["OnErrorContainer"] = Read(Resource.Attribute.colorOnErrorContainer),

            // Surface
            ["Surface"] = Read(Resource.Attribute.colorSurface),
            ["OnSurface"] = Read(Resource.Attribute.colorOnSurface),
            ["SurfaceVariant"] = Read(Resource.Attribute.colorSurfaceVariant),
            ["OnSurfaceVariant"] = Read(Resource.Attribute.colorOnSurfaceVariant),

            // Background
            ["Background"] = Read(global::Android.Resource.Attribute.ColorBackground),
            ["OnBackground"] = Read(Resource.Attribute.colorOnBackground),

            // Outline
            ["Outline"] = Read(Resource.Attribute.colorOutline),
            ["OutlineVariant"] = Read(Resource.Attribute.colorOutlineVariant),
        };

        // Actualizar recursos en el hilo UI
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var kv in dict)
            {
                resources[kv.Key] = kv.Value;
            }
        });
    }
}