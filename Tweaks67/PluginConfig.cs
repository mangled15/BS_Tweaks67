using System.Linq;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Utilities;
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
    
    // Global stuff
    public virtual bool DustEnabled { get; set; } = true;
    // Menu
    public virtual bool LogoEnabled { get; set; } = true;
    public virtual Color BeatLogoColor { get; set; } = new Color(1,0,0,1);
    public virtual Color SaberLogoColor { get; set; } =  new Color(0, 0.6588235f, 1, 1);
    public virtual bool DefaultLogoColorsEnabled { get; set; } = true;
    // Gameplay
    public virtual bool GradientBackgroundEnabled { get; set; } = true;
    public virtual bool CutParticleEnabled { get; set; } = true;

    // global
    public static void dustPS()
    {
        var dustPS = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "DustPS");
        if (dustPS != null)
        {
            dustPS.SetActive(Plugin.Config.DustEnabled);
        }
        else
        {
            Plugin.Log.Notice("DustPS not found!");
        }
    }
    
    // gameplay
    public static void dustPSGameCore()
    {
        var dustPS = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "DustPS" && go.transform.parent.name == "Environment");
        if (dustPS != null)
        {
            dustPS.SetActive(Plugin.Config.DustEnabled);
        }
        else
        {
            Plugin.Log.Notice("DustPS not found!");
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
            Plugin.Log.Notice("GradientBackground not found!");
        }
    }

    // menu
    public static void logo()
    {
       var gameLogo = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "Logo" && go.transform.parent.name == "DefaultMenuEnvironment");

        var line = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines");
        var line1 = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines (1)");
        if (gameLogo != null && line != null && line1 != null)
        {
            gameLogo.SetActive(Plugin.Config.LogoEnabled);
            line.SetActive(Plugin.Config.LogoEnabled);
            line1.SetActive(Plugin.Config.LogoEnabled);
        }
        else
        {
            Plugin.Log.Notice("logo not found!");
        }
    }

    public static void BeatLogoColorChange()
    {
        var beatColor = Plugin.Config?.BeatLogoColor ?? new Color(0f,0f,0f,0f);
        
        var batLogo = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "BatLogo");
        var logoE = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "LogoE");
        var EFlickering = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "EFlickering");
        var line = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines");
        if (batLogo != null && logoE != null && EFlickering != null && line != null)
        {
            batLogo.GetComponent<SpriteRenderer>().color = beatColor;
            logoE.GetComponent<SpriteRenderer>().color = beatColor;
            EFlickering.GetComponent<FlickeringNeonSign>().SetField("_spriteOnColor", beatColor);
            EFlickering.GetComponent<FlickeringNeonSign>().SetField("_lightOnColor", beatColor);

            line.GetComponent<SpriteRenderer>().color = new Color(beatColor.r, beatColor.g, beatColor.b, 0.2352941f);
        }
        
        var b = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "BNeon");
        var e = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "ENeon");
        var a = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "ANeon");
        var t = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "TNeon");

        if (b == null || e == null || a == null || t == null) return;
        
        b.GetComponent<TubeBloomPrePassLight>().color = beatColor;
        e.GetComponent<TubeBloomPrePassLight>().color = beatColor;
        a.GetComponent<TubeBloomPrePassLight>().color = beatColor;
        t.GetComponent<TubeBloomPrePassLight>().color = beatColor;
    }

    public static void SaberLogoColorChange()
    {
        var saberColor = Plugin.Config?.SaberLogoColor ?? new Color(0f,0f,0f,0f);
        
        var saber = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "SaberLogo");
        var saberNeon = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "SaberNeon");
        var line = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines (1)");
        if (saber != null && saberNeon != null && line != null)
        {
            saber.GetComponent<SpriteRenderer>().color = saberColor;
            saberNeon.GetComponent<TubeBloomPrePassLight>().color = saberColor;
            line.GetComponent<SpriteRenderer>().color = new Color(saberColor.r, saberColor.g, saberColor.b, 0.2352941f);
        }
    }

    private readonly static Color defaultBeatColor = new Color(1,0,0,1);
    private readonly static Color defaultSaberColor = new Color(0, 0.6588235f, 1, 1);
    public static void defaultLogoColors()
    {
        // saber logo
        var saber = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "SaberLogo");
        var saberNeon = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "SaberNeon");
        var line = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines (1)");
        if (saber != null && saberNeon != null && line != null)
        {
            saber.GetComponent<SpriteRenderer>().color = defaultSaberColor;
            saberNeon.GetComponent<TubeBloomPrePassLight>().color = defaultSaberColor;
            // idk why beat saber's alpha for the glowlines is so specific lolol
            line.GetComponent<SpriteRenderer>().color = new Color(defaultSaberColor.r, defaultSaberColor.g, defaultSaberColor.b, 0.2352941f);
        }
        
        // beat logo (pain)
        var batLogo = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "BatLogo");
        var logoE = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "LogoE");
        var EFlickering = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "EFlickering");
        var line1 = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "GlowLines");
        if (batLogo != null && logoE != null && EFlickering != null && line1 != null)
        {
            batLogo.GetComponent<SpriteRenderer>().color = defaultBeatColor;
            logoE.GetComponent<SpriteRenderer>().color = defaultBeatColor;
            EFlickering.GetComponent<FlickeringNeonSign>().SetField("_spriteOnColor", defaultBeatColor);
            EFlickering.GetComponent<FlickeringNeonSign>().SetField("_lightOnColor", defaultBeatColor);

            // idk why beat saber's alpha for the glowlines is so specific lolol
            line1.GetComponent<SpriteRenderer>().color = new Color(defaultBeatColor.r, defaultBeatColor.g, defaultBeatColor.b, 0.2352941f); 
        }
        
        var b = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "BNeon");
        var e = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "ENeon");
        var a = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "ANeon");
        var t = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(go => go.name == "TNeon");

        if (b == null || e == null || a == null || t == null) return;

        b.GetComponent<TubeBloomPrePassLight>().color = defaultBeatColor;
        e.GetComponent<TubeBloomPrePassLight>().color = defaultBeatColor;
        a.GetComponent<TubeBloomPrePassLight>().color = defaultBeatColor;
        t.GetComponent<TubeBloomPrePassLight>().color = defaultBeatColor;
    }

    private int cursedFixMaybe = 0;
    public virtual void Changed()
    {
        if (cursedFixMaybe == 0)
        {
            cursedFixMaybe += 1;
            return;
        }
        
        dustPS();
        logo();
        
        if (DefaultLogoColorsEnabled)
        {
            defaultLogoColors();
            cursedFixMaybe += 1;
            return;
        }
        
        if (!DefaultLogoColorsEnabled)
        {
            BeatLogoColorChange();
            SaberLogoColorChange();
        }
        cursedFixMaybe += 1;
    }
}