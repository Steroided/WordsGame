using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameVariables;

public class Cluster : MonoBehaviour
{
    [HideInInspector]
    public string Key;
    private Image _image;
    private Color _defaultColor;
    private void Start()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
    }
    public void SetClusterColor(Color color) => StartCoroutine(ChangeColor(color));

    public IEnumerator ChangeColor(Color color)
    {
        _image.color = color;
        yield return new WaitForSeconds(2);
        _image.color = _defaultColor;
    }
}
