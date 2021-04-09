using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void MenuBack() => PlaySound(nameof(MenuBack));

    public void MenuMove() => PlaySound(nameof(MenuMove));

    public void MenuSelect() => PlaySound(nameof(MenuSelect));

    private void PlaySound(string name)
    {
        var clip = Resources.Load<AudioClip>($"Sounds/{GameManager.SoundTheme}/{name}");
        if (clip) audioSource.PlayOneShot(clip);
    }
}