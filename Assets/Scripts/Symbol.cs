using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public void SetText(string c) => Text.text = c;
}
