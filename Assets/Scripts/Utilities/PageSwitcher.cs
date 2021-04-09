using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageSwitcher : MonoBehaviour
{
    public Button FirstTrigger;
    public Button PreviousTrigger;
    public Button NextTrigger;
    public Button LastTrigger;
    public GameObject PageNumber;
    public List<Canvas> Pages;

    private TextMeshProUGUI PageNumberText { get => PageNumber.GetComponent<TextMeshProUGUI>(); }

    private int PageCount { get => Pages.Count; }

    private int PageIndex { get; set; } = 1;

    private void Start() => SwitchPage(true);

    public void GoToFirst()
    {
        PageIndex = 1;
        SwitchPage();
    }

    public void GoToPrevious()
    {
        PageIndex--;
        SwitchPage();
    }

    public void GoToNext()
    {
        PageIndex++;
        SwitchPage();
    }

    public void GoToLast()
    {
        PageIndex = PageCount;
        SwitchPage();
    }

    private void SwitchPage(bool skipSelects = false)
    {
        //Constrain values
        if (PageIndex < 1) PageIndex = 1;
        if (PageIndex > PageCount) PageIndex = PageCount;

        //Configure buttons
        if (PageIndex > 1)
        {
            if (FirstTrigger) FirstTrigger.interactable = true;
            if (PreviousTrigger) PreviousTrigger.interactable = true;
        }
        else
        {
            if (FirstTrigger) FirstTrigger.interactable = false;
            if (PreviousTrigger) PreviousTrigger.interactable = false;
            if (!skipSelects) PreviousTrigger.FindSelectableOnRight().Select();
        }

        if (PageIndex < PageCount)
        {
            if (NextTrigger) NextTrigger.interactable = true;
            if (LastTrigger) LastTrigger.interactable = true;
        }
        else
        {
            if (NextTrigger) NextTrigger.interactable = false;
            if (LastTrigger) LastTrigger.interactable = false;
            if (!skipSelects) NextTrigger.FindSelectableOnLeft().Select();
        }

        //Switch visible page
        Pages.ForEach(p => p.gameObject.SetActive(false));
        Pages[PageIndex - 1].gameObject.SetActive(true);
        PageNumberText.text = $"{PageIndex}/{PageCount}";
    }
}