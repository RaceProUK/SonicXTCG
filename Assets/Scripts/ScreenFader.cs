using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Color FadeColour = new Color(0f, 0f, 0f);

    public float Duration = 1f;

    private float Speed { get => Duration > 0 ? 1f / Duration : float.MaxValue; }

    private enum Direction { In, Out }

    internal IEnumerator FadeIn(Action callback = null) => Fade(Direction.In, callback);

    internal IEnumerator FadeOut(Action callback = null) => Fade(Direction.Out, callback);

    private IEnumerator Fade(Direction direction, Action callback = null)
    {
        var colour = gameObject.GetComponent<Image>().color;
        switch (direction)
        {
            case Direction.In:
                while (gameObject.GetComponent<Image>().color.a > 0)
                {
                    colour = new Color(FadeColour.r, FadeColour.g, FadeColour.b, colour.a - (Speed * Time.deltaTime));
                    gameObject.GetComponent<Image>().color = colour;
                    yield return null;
                }
                break;

            case Direction.Out:
                while (gameObject.GetComponent<Image>().color.a < 1)
                {
                    colour = new Color(FadeColour.r, FadeColour.g, FadeColour.b, colour.a + (Speed * Time.deltaTime));
                    gameObject.GetComponent<Image>().color = colour;
                    yield return null;
                }
                break;
        }
        callback?.Invoke();
    }
}