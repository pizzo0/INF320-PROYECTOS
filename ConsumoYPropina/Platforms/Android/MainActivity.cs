using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Content.Res;
using ConsumoYPropina.Platforms.Android.Services;
using Microsoft.Maui.ApplicationModel;

namespace ConsumoYPropina;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
        {
            DynamicColorService.Apply();
        }
    }

    protected override void OnResume()
    {
        base.OnResume();
        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            DynamicColorService.Apply();
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
        {
            DynamicColorService.Apply();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}

