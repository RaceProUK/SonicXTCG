using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollPropagator : MonoBehaviour
{
    private void Start()
    {
        var scrollRect = GetComponentInParent<ScrollRect>();
        var trigger = GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry { eventID = EventTriggerType.Scroll };
        entry.callback.AddListener(data => { if (data is PointerEventData arg) scrollRect.OnScroll(arg); });
        trigger.triggers.Add(entry);
    }
}