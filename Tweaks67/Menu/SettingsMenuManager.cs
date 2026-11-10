using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;
using UnityEngine;

namespace Tweaks67.Menu;

internal static class SettingsMenuManager
{
    private static MenuSettings? Instance { get; set; }

    private const string MenuName = nameof(Tweaks67);
    private const string ResourcePath = nameof(Tweaks67) + ".Menu.settings.bsml";

    /// <summary>
    /// Adds a custom menu in the Mod Settings section of the main menu.
    /// This should only be called when the main menu is active.
    /// </summary>
    public static void AddSettingsMenu()
    {
        if (Instance == null)
        {
            Instance = new GameObject(nameof(MenuSettings)).AddComponent<MenuSettings>();
            Object.DontDestroyOnLoad(Instance.gameObject);
        }

        RemoveSettingsMenu();

        // BSMLSettings.Instance.AddSettingsMenu(MenuName, ResourcePath, Instance);
        // Turns it into a tab
        GameplaySetup.Instance.AddTab(MenuName, ResourcePath, Instance);
    }

    public static void RemoveSettingsMenu()
    {
        BSMLSettings.Instance.RemoveSettingsMenu(Instance);
    }
}