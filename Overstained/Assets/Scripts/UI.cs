using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [HideInInspector]
    public static UI instance;
    public TextMeshProUGUI activeItemText;

    void OnEnable()
    {
        instance = this;
    }

    void OnDisable()
    {
        instance = null;
    }
}
