using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.name == "ClusterBase" || gameObject.transform.childCount<GameSettings.Instance.MaxClustersUI)
        {
            DraggableItem d = eventData.pointerDrag.GetComponent<DraggableItem>();
            d.ParentAfterDrag = transform;
            d.transform.SetParent(transform,true);
            CheckChildShift(transform.childCount, d.transform);
        }
    }
    private void CheckChildShift(int childCount, Transform draggable)
    {
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).transform.position.x > draggable.position.x)
            {
                draggable.SetSiblingIndex(i);
                break;
            }
        }
    }
}