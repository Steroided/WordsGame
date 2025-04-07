using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private CanvasGroup _group;
    public Transform ParentAfterDrag;
    [HideInInspector]
    public bool Swap = false;

    private void Start()
    {
        _image = GetComponent<Image>();
        _group = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _group.alpha = .5f;
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        _group.alpha = 1f;
        _image.raycastTarget = true;
    }
}