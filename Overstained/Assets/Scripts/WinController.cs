using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinController : MonoBehaviour
{
    public float timer = 90;

    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, 300);

        if (timer < 0.001f)
            Lost();
        if (Pickable.pickables.Count < 1)
            Win();

        UI.instance.timer.GetComponent<TextMeshProUGUI>().text = "Time left: " + timer.ToString("0");
    }

    public void Win()
    {
        UI.instance.win.SetActive(true);
        UI.instance.defeat.SetActive(false);
    }

    public void Lost()
    {
        UI.instance.win.SetActive(false);
        UI.instance.defeat.SetActive(true);
    }
}
