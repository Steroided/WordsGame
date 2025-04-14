using UnityEngine;
using UnityEngine.UI;

public class Cluster : MonoBehaviour
{
    [HideInInspector]
    public string Key;
    private Image _image;
    [HideInInspector]
    public Color DefaultColor;
    private void Start()
    {
        _image = GetComponent<Image>(); 
        DefaultColor = _image.color;
    }
    public void SetClusterColor(Color color) 
    {
        _image.enabled = color == new Color(0, 0, 0, 0) ? false : true;
        _image.color = color;
    } 
}
