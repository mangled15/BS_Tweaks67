using System.Linq;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace Tweaks67;

internal class PluginConfig
{
    // Members must be 'virtual' if you want BSIPA to detect a value change and save the config automatically
    // You can assign a default value to be used when the config is first created by assigning one after '=' 
    // examples:
    // public virtual bool FeatureEnabled { get; set; } = true;
    // public virtual int NumValue { get; set; } = 42;
    // public virtual Color TheColor { get; set; } = new Color(0.12f, 0.34f, 0.56f);

    /*
    /// <summary>
    /// This is called whenever BSIPA reads the config from disk (including when file changes are detected).
    /// </summary>
    public virtual void OnReload() { }

    /// <summary>
    /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
    /// </summary>
    public virtual void Changed() { }

    /// <summary>
    /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
    /// </summary>
    public virtual void CopyFrom(PluginConfig other) { }
    */

    public virtual bool DustEnabled { get; set; } = true;
    public virtual bool GradientBackgroundEnabled { get; set; } = true;
    public virtual bool LogoEnabled { get; set; } = true;

    public static void dustPS()
    {
        var dustPS = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "DustPS");
        if (dustPS != null)
        {
            dustPS.SetActive(Plugin.Config.DustEnabled);
        }
        else
        {
            //Plugin.Log.Notice("DustPS not found!");
        }
    }
    
    public static void dustPSGameCore()
    {
        var dustPS = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "DustPS" && go.transform.parent.name == "Environment");
        if (dustPS != null)
        {
            dustPS.SetActive(Plugin.Config.DustEnabled);
        }
        else
        {
            //Plugin.Log.Notice("DustPS not found!");
        }
    }

    public static void GradientBackground()
    {
        var gradientBackground = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GradientBackground");
        if (gradientBackground != null)
        {
            gradientBackground.SetActive(Plugin.Config.GradientBackgroundEnabled);
        }
        else
        {
            //Plugin.Log.Notice("GradientBackground not found!");
        }
    }

    public static void logo()
    {
        var logo = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "Logo");
        var line = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines");
        var line1 = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines (1)");
        if (logo != null && line != null && line1 != null)
        {
            logo.SetActive(Plugin.Config.LogoEnabled);
            line.SetActive(Plugin.Config.LogoEnabled);
            line1.SetActive(Plugin.Config.LogoEnabled);
        }
        else
        {
            //Plugin.Log.Notice("logo not found!");
        }
    }

    public virtual void Changed()
    {
        dustPS();
        logo();
    }
}