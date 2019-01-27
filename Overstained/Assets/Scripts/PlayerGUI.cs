using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerGUI : MonoBehaviour
{
    private UI ui;
    private PlayerMovement player;

    private const string EMPTY = "Empty Hands";

    void OnEnable()
    {
        ui = UI.instance;
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(ui == null) FetchUI();

        if (player.target == null)
        {
            if (ui.activeItemText.text != EMPTY)
                ui.activeItemText.text = EMPTY;
            return;
        } else
        {
            if (player.target.displayName != ui.activeItemText.text)
                ui.activeItemText.text = player.target.displayName;
        }
    }

    void FetchUI()
    {
        ui = UI.instance;
    }
}
