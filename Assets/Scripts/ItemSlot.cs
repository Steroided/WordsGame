using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.name == "ClusterBase" || gameObject.transform.childCount<GameSettings.Instance.MaxClustersUI)
        {
            eventData.pointerDrag.GetComponent<DraggableItem>().ParentAfterDrag = transform;
            eventData.pointerDrag.GetComponent<DraggableItem>().transform.SetParent(transform,true);
            eventData.pointerDrag.GetComponent<DraggableItem>().Swap = true;
            print("DROP");

            switch (transform.childCount)
            {
                case 2:
                    {
                        print(eventData.pointerDrag.transform.position.x);
                        print(transform.GetChild(0).transform.position.x);
                        if (eventData.pointerDrag.transform.position.x < transform.GetChild(0).transform.position.x)
                            eventData.pointerDrag.transform.SetAsFirstSibling();
                    }
                    break;
                case 3:
                    {
                        if (eventData.pointerDrag.transform.position.x < transform.GetChild(0).transform.position.x)
                            eventData.pointerDrag.transform.SetAsFirstSibling();
                        if (eventData.pointerDrag.transform.position.x < transform.GetChild(1).transform.position.x)
                            transform.GetChild(1).transform.SetAsLastSibling();
                    }
                    break;
            }
        }
    }
    private void CheckChild(int childCount)
    {
        for(int i = 0; i < childCount; i++)
        {

        }
    }
}