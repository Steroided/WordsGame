using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [Inject]
    private GameSettings _gameSettings;
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.name == "ClusterBase" || gameObject.transform.childCount<_gameSettings.MaxClustersUIPerSlot)
        {
            DraggableItem d = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (gameObject.name != "ClusterBase")
            {
                int ClusterLength = d.GetComponent<Cluster>().Key.Length;
                int SiblingIndex = transform.GetSiblingIndex();
                print(SiblingIndex);
                for (int i = 0; i < ClusterLength-1; i++)
                {
                    int check = SiblingIndex;
                    check--;
                    if (check < 0)
                        return;
                    //Проверяем на популяцию слотов остаток клеток, нужных для расположения
                    if (transform.parent.GetChild(SiblingIndex-=1).childCount != 0)
                    {
                        return;
                    }
                    
                }
            }
            d.ParentAfterDrag = transform;
        }
    }
}