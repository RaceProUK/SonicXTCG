using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Canvas MainMenu;
    public Canvas GameRules;
    public Canvas Options;
    public Canvas Credits;

    private enum Direction { In, Out }

    private void Start()
    {
        MainMenu.gameObject.SetActive(true);
        GameRules.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
        Credits.gameObject.SetActive(false);
    }

    public void GoToMainMenu() => Transition(MainMenu);

    public void GoToJoinGame() { }

    public void GoToHostGame() { }

    public void GoToGameRules() => Transition(GameRules);

    public void GoToDeckBuilder() { }

    public void GoToCardViewer() { }

    public void GoToOptions() => Transition(Options);

    public void GoToCredits() => Transition(Credits);

    public void QuitGame() => StartCoroutine(FindObjectOfType<ScreenFader>().FadeOut(() => Application.Quit()));

    private void Transition(Canvas to)
    {
        var from = EventSystem.current.currentSelectedGameObject.GetComponentInParent<Canvas>();
        var fromFader = from.GetComponent<CanvasGroup>();
        var toFader = to.GetComponent<CanvasGroup>();

        toFader.alpha = 0f;
        to.gameObject.SetActive(true);
        StartCoroutine(Fade(toFader, Direction.In, () => to.GetComponentsInChildren<Selectable>().First(i => i.interactable).Select()));
        StartCoroutine(Fade(fromFader, Direction.Out, () => from.gameObject.SetActive(false)));
    }

    private IEnumerator Fade(CanvasGroup fader, Direction direction, Action callback = null)
    {
        switch (direction)
        {
            case Direction.In:
                while (fader.alpha < 1)
                {
                    fader.alpha += (4f * Time.deltaTime);
                    yield return null;
                }
                break;

            case Direction.Out:
                while (fader.alpha > 0)
                {
                    fader.alpha -= (4f * Time.deltaTime);
                    yield return null;
                }
                break;
        }
        callback?.Invoke();
    }
}