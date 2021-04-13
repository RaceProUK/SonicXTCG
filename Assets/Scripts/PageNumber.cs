using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PageNumber : MonoBehaviour
{
    public ScrollSnapBase ScrollSnap;
    public TMP_Text Label;

    public void Start() => UpdatePageNumber(ScrollSnap.CurrentPage);

    public void UpdatePageNumber(int page) => Label.text = $"{page + 1}/{ScrollSnap.ChildObjects.Length}";
}