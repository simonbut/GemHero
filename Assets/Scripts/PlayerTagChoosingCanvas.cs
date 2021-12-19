using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagChoosingCanvas : TagChoosingCanvas
{
    public PlayerTagCanvas playerTagCanvas;

    public override void AddUI()
    {
        playerTagCanvas.ShowPlayerTag();

        base.AddUI();
    }

    private void Update()
    {
        if (playerTagCanvas.selectingTagId > 0)
        {
            DisplayTagDescription(playerTagCanvas.selectingTagId);
        }
    }
}
