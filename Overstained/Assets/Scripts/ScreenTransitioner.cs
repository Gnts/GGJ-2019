using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransitioner : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;

    public void TransitionToMenu()
    {
        credits.SetActive(false);
        menu.SetActive(true);
    }

    public void TransitionToCredits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
    }
}
