using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;
    private CanvasGroup _group;
    public Transform ParentAfterDrag;
    private float _xOffset;
    [Inject]
    private GameSettings _gameSettings;
    private void Start()
    {
        _image = GetComponent<Image>();
        _group = GetComponent<CanvasGroup>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _xOffset = GetComponent<RectTransform>().sizeDelta.x / _gameSettings.OnDraggableOffsetXRatio;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _group.alpha = .5f;
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + new Vector3(_xOffset,0);    
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentAfterDrag,false);
        _group.alpha = 1f;
        _image.raycastTarget = true;
    }
}