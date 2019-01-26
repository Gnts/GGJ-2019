using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [HideInInspector]
    public static UI instance;
    public TextMeshProUGUI activeItemText;
    public TextMeshProUGUI scoreText;
    public GameObject win;
    public GameObject defeat;
    public GameObject timer;
    public TextMeshProUGUI itemsLeft;

    void OnEnable()
    {
        instance = this;
    }

    void OnDisable()
    {
        instance = null;
    }
}
