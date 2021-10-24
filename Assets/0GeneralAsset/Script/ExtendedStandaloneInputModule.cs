using UnityEngine.EventSystems;

public class ExtendedStandaloneInputModule : StandaloneInputModule
{
    public PointerEventData GetPointerEventData(int pointerId = -1)
    {
        PointerEventData eventData;
        GetPointerData(pointerId, out eventData, true);
        return eventData;
    }

    protected override void Awake()
    {
        base.Awake();
    }
}