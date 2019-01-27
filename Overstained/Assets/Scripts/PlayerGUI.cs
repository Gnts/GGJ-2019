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

        if (player.target != null && player.target.displayName != ui.activeItemText.text)
        {
            ui.activeItemText.text = player.target.displayName;
        } else
        {
            if (ui.activeItemText.text != "Empty Hands")
                ui.activeItemText.text = "Empty Hands";
        }
    }

    void FetchUI()
    {
        ui = UI.instance;
    }
}
