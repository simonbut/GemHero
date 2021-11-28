using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class AssetDataUI : DataUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(ResourceAsset _ra)
    {

        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
