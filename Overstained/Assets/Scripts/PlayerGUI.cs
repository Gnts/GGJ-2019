using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerGUI : MonoBehaviour
{
    private UI ui;
    private PlayerMovement player;

    void OnEnable()
    {
        ui = UI.instance;
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(ui == null) FetchUI();
        ui.activeItemText.text = player.target != null ? player.target.name : "Empty hands!";
    }

    void FetchUI()
    {
        ui = UI.instance;
    }
}
