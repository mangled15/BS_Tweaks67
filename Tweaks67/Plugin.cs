using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using UnityEngine;
using UnityEngine.SceneManagement;
using IpaLogger = IPA.Logging.Logger;
using IpaConfig = IPA.Config.Config;

namespace Tweaks67;

[Plugin(RuntimeOptions.DynamicInit)]
internal class Plugin
{
    internal static IpaLogger Log { get; private set; } = null!;
    internal static PluginConfig Config { get; private set; } = null!;

    // Methods with [Init] are called when the plugin is first loaded by IPA.
    // All the parameters are provided by IPA and are optional.
    // The constructor is called before any method with [Init]. Only use [Init] with one constructor.
    [Init]
    public Plugin(IpaLogger ipaLogger, IpaConfig ipaConfig, PluginMetadata pluginMetadata)
    {
        Log = ipaLogger;
        // Creates an instance of PluginConfig used by IPA to load and store config values
        Config = ipaConfig.Generated<PluginConfig>();
    }

    [OnStart]
    public void OnApplicationStart()
    {
        Log.Debug("OnApplicationStart");
        new GameObject("Tweaks67Controller").AddComponent<Tweaks67Controller>();
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private static void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            if (!Config.DustEnabled)
            {
                PluginConfig.dustPS();
            }

            if (!Config.LogoEnabled)
            {
                PluginConfig.logo();
            }
            
            if (Config.DefaultLogoColorsEnabled)
            {
                PluginConfig.defaultLogoColors();
                return;
            }

            if (!Config.DefaultLogoColorsEnabled)
            {
                PluginConfig.BeatLogoColorChange();
                PluginConfig.SaberLogoColorChange();
            }
        }

        if (scene.name == "GameCore")
        {
            PluginConfig.dustPSGameCore();
            PluginConfig.GradientBackground();
        }
    }

    [OnExit]
    public void OnApplicationQuit()
    {
        Log.Debug("OnApplicationQuit");
        SceneManager.sceneLoaded -= OnSceneChanged;
    }
}