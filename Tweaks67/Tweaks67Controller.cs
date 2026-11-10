using BeatSaberMarkupLanguage.Util;
using Tweaks67.Menu;
using UnityEngine;

namespace Tweaks67;

// MonoBehaviours are scripts added to in-game GameObjects which execute code during runtime.
// For a full list of Messages a MonoBehaviour can receive from the game, refer to the Unity documentation.
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
internal class Tweaks67Controller : MonoBehaviour
{
    private static Tweaks67Controller? Instance { get; set; }

    /// <summary>
    /// Called a single time by Unity when this script is created.
    /// </summary>
    private void Awake()
    {
        // For this particular MonoBehaviour, we only want one instance to exist at any time.
        // Store a reference to it in a static property and destroy any that are created while one already exists.
        if (Instance != null)
        {
            DestroyImmediate(this);
            return;
        }

        DontDestroyOnLoad(this); // Don't destroy this object on scene changes
        Instance = this;
        // Invoked when the main menu scene loads, or loads fresh after applying settings.
        // This is important for initializing objects in the menu, namely UI objects.
        MainMenuAwaiter.MainMenuInitializing += SettingsMenuManager.AddSettingsMenu;
    }

    /// <summary>
    /// Called a single time by Unity on the first frame the script is enabled.
    /// </summary>
    private void Start()
    {
        Plugin.Log.Debug($"{name} started");
    }

    /// <summary>
    /// Called a single time by Unity when the script is being destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Plugin.Log.Debug($"{name} destroyed");
        MainMenuAwaiter.MainMenuInitializing -= SettingsMenuManager.AddSettingsMenu;

        if (Instance == this)
        {
            // This MonoBehaviour is being destroyed, so set the static instance property to null.
            Instance = null;
        }
    }
}