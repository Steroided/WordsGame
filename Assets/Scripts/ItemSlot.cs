using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.name == "ClusterBase" || gameObject.transform.childCount<GameSettings.Instance.MaxClustersUIPerSlot)
        {
            DraggableItem d = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (gameObject.name != "ClusterBase")
            {
                int ClusterLength = d.GetComponent<ClusterId>().Key.Length-1;
                int SiblingIndex = transform.GetSiblingIndex();
                print(SiblingIndex);
                for (int i = 0; i < ClusterLength; i++)
                {


                    int check = SiblingIndex;
                    check--;
                    if (check < 0)
                        return;
                    //Проверяем на популяцию слотов остаток клеток, нужных для расположения
                    if (transform.parent.GetChild(SiblingIndex-=1).childCount != 0)
                    {
                      //  print("POPPULATED" + i + "SLOT");
                        return;
                    }
                    
                }
            }
            d.ParentAfterDrag = transform;
        }
    }
}