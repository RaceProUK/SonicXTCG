using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScreenFader ScreenFader;
    private ScreenFader instance;

    public static int MasterVolume
    {
        get => PlayerPrefs.GetInt(nameof(MasterVolume), 8);
        set
        {
            PlayerPrefs.SetInt(nameof(MasterVolume), value);
            SetGlobalVolume();
        }
    }
    public const float MaxVolume = 11f;

    public static string SoundTheme
    {
        get => PlayerPrefs.GetString(nameof(SoundTheme), "SA1");
        set => PlayerPrefs.SetString(nameof(SoundTheme), value);
    }

    public static Resolution Resolution
    {
        get => Screen.resolutions.First(r => string.Equals(r.ToString(),
                                                           PlayerPrefs.GetString(nameof(Resolution), Screen.currentResolution.ToString()),
                                                           StringComparison.Ordinal));
        set
        {
            PlayerPrefs.SetString(nameof(Resolution), value.ToString());
            SetScreenResolutionAndMode();
        }
    }

    public static FullScreenMode ScreenMode
    {
        get => (FullScreenMode)PlayerPrefs.GetInt(nameof(ScreenMode), (int)Screen.fullScreenMode);
        set
        {
            PlayerPrefs.SetInt(nameof(ScreenMode), (int)value);
            SetScreenResolutionAndMode();
        }
    }

    public static void SaveOptions() => PlayerPrefs.Save();

    public void Quit() => StartCoroutine(instance.FadeOut(() => Application.Quit()));

    private void Start()
    {
        SetGlobalVolume();
        instance = Instantiate(ScreenFader);
        switch (SceneManager.GetActiveScene().name)
        {
            case "Disclaimer":
                StartCoroutine(instance.FadeIn(() => StartCoroutine(AutoDismissDisclaimer())));
                break;

            default:
                StartCoroutine(instance.FadeIn());
                break;
        }
    }

    private IEnumerator AutoDismissDisclaimer()
    {
        yield return new WaitForSecondsRealtime(5);
        StartCoroutine(instance.GetComponent<ScreenFader>().FadeOut(() => SceneManager.LoadScene("Menus")));
    }

    private static void SetGlobalVolume() => AudioListener.volume = MasterVolume / MaxVolume;

    private static void SetScreenResolutionAndMode() => Screen.SetResolution(Resolution.width, Resolution.height, ScreenMode, Resolution.refreshRate);
}