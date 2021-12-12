using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetGridItem : GridItem
{
    public GameObject tick;
    public GameObject blackOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public new void Update()
    {
        CompositeMenuCanvas _cmc = MainGameView.Instance.compositeMenuCanvas;

        tick.SetActive(false);
        for (int i = 0; i < _cmc.assetSelectList.Length; i++)
        {
            if (_cmc.assetSelectList[i] == id)
            {
                tick.SetActive(true);
                blackOverlay.SetActive(_cmc.session != i);
            }
        }

        base.Update();
    }
}
