using BeatSaberMarkupLanguage.Attributes;
using HMUI;
using TMPro;
using UnityEngine;

namespace Tweaks67.Menu;

// This class is for managing the BSML resource and interact with the UI object in game.
// To see more about components read the BSML docs. https://monkeymanboy.github.io/BSML-Docs/
internal class MenuSettings : MonoBehaviour
{
    [UIAction("#post-parse")]
    private void PostParse()
    {
        Plugin.Log.Debug($"{name} parsed!");
    }

    [UIValue("dustValue")]
    private bool dustValue
    {
        get => Plugin.Config.DustEnabled;
        set => Plugin.Config.DustEnabled = value;
    }
    
    [UIValue("gradientValue")]
    public bool gradientValue
    {
        get => Plugin.Config.GradientBackgroundEnabled;
        set => Plugin.Config.GradientBackgroundEnabled = value;
    }
    
    [UIValue("logoValue")]
    public bool logoValue
    {
        get => Plugin.Config.LogoEnabled;
        set => Plugin.Config.LogoEnabled = value;
    }
}