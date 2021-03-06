using Humanizer;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public TextMeshProUGUI MasterVolumeLabel;
    public Slider MasterVolume;
    public TMP_Dropdown SoundTheme;
    public TMP_Dropdown Resolution;
    public TMP_Dropdown ScreenMode;

    private int OldMasterVolume;
    private string OldSoundTheme;
    private Resolution OldResolution;
    private FullScreenMode OldScreenMode;

    private static readonly (string Folder, string Text)[] SoundThemes = new[]
    {
        ("Classic", "Genesis/Mega Drive Classics"),
        (    "SA1", "Sonic Adventure/SADX"       ),
        (    "SA2", "Sonic Adventure 2/SA2B"     ),
        ("Advance", "Sonic Advance Trilogy"      ),
        ( "Battle", "Sonic Battle"               ),
        ( "Heroes", "Sonic Heroes"               ),
        (     "06", "Sonic the Hedgehog (2006)"  ),
        (  "Mania", "Sonic Mania"                )
    };

    private void OnEnable()
    {
        if (SoundTheme.options.Count < 1) SoundTheme.AddOptions(SoundThemes.Select(t => new TMP_Dropdown.OptionData { text = t.Text }).ToList());
        if (Resolution.options.Count < 1) Resolution.AddOptions(Screen.resolutions.Select(r => new TMP_Dropdown.OptionData { text = r.ToString() }).ToList());
        if (ScreenMode.options.Count < 1) ScreenMode.AddOptions(Enum.GetNames(typeof(FullScreenMode)).Select(m => new TMP_Dropdown.OptionData { text = m.Humanize(LetterCasing.Title) }).ToList());

        OldMasterVolume = GameManager.MasterVolume;
        OldSoundTheme = GameManager.SoundTheme;
        OldResolution = GameManager.Resolution;
        OldScreenMode = GameManager.ScreenMode;

        MasterVolume.value = GameManager.MasterVolume;
        SoundTheme.value = Array.IndexOf(SoundThemes, SoundThemes.First(t => string.Equals(t.Folder, GameManager.SoundTheme, StringComparison.OrdinalIgnoreCase)));
        Resolution.value = Array.IndexOf(Screen.resolutions, GameManager.Resolution);
        ScreenMode.value = (int)GameManager.ScreenMode;
    }

    public void UpdateMasterVolume(float value)
    {
        GameManager.MasterVolume = Convert.ToInt32(value);
        MasterVolumeLabel.text = value.ToString("F0");
    }

    public void UpdateSoundTheme(int value) => GameManager.SoundTheme = SoundThemes[value].Folder;

    public void UpdateResolution(int value) => GameManager.Resolution = Screen.resolutions[value];

    public void UpdateScreenMode(int value) => GameManager.ScreenMode = (FullScreenMode)value;

    public void Save() => GameManager.SaveOptions();

    public void Discard()
    {
        GameManager.MasterVolume = OldMasterVolume;
        GameManager.SoundTheme = OldSoundTheme;
        GameManager.Resolution = OldResolution;
        GameManager.ScreenMode = OldScreenMode;
    }
}