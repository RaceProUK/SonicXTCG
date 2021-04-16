using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollWithSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform scrollRectTransform;
    private RectTransform contentPanel;
    private GameObject lastSelected;
    private bool mouseHover;

    public void OnPointerEnter(PointerEventData eventData) => mouseHover = true;

    public void OnPointerExit(PointerEventData eventData) => mouseHover = false;

    private void Start()
    {
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
    }

    private void Update()
    {
        if (mouseHover) return; //Scroll with the mouse instead

        var selected = EventSystem.current.currentSelectedGameObject;
        if (!selected) return; //No selection to scroll to
        if (selected == lastSelected) return; //No need to scroll if the selection is unchanged
        if (selected.transform.parent != contentPanel.transform) return; //Don't scroll if the selection isn't parented correctly

        //Record the selection
        lastSelected = selected;

        var selectedRectTransform = selected.transform as RectTransform;
        if (!selectedRectTransform) return; //No transform, so can't do anything

        //Get the height, top, and bottom of selected item
        var selectedHeight = selectedRectTransform.rect.height;
        var selectedMid = -selectedRectTransform.localPosition.y;
        var selectedTop = selectedMid - selectedHeight / 2f;
        var selectedBottom = selectedTop + selectedHeight;

        //Get the height, top, and bottom of visible area
        var listHeight = contentPanel.sizeDelta.y - scrollRectTransform.sizeDelta.y;
        var visibleTop = contentPanel.anchoredPosition.y;
        var visibleBottom = visibleTop + listHeight;
        
        //Set the new scroll position only if the currently seledcted item is out of view
        if (selectedTop >= visibleTop && selectedBottom <= visibleBottom)
            return;
        else if (selectedTop < visibleTop)
            contentPanel.anchoredPosition = new Vector2
            {
                x = contentPanel.anchoredPosition.x,
                y = Mathf.Clamp(visibleTop - selectedHeight, 0, listHeight)
            };
        else if (selectedBottom > visibleBottom)
            contentPanel.anchoredPosition = new Vector2
            {
                x = contentPanel.anchoredPosition.x,
                y = Mathf.Clamp(visibleTop + selectedBottom - visibleBottom, 0, listHeight)
            };
    }
}