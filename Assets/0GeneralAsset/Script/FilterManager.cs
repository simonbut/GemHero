using ClassHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterManager : MonoBehaviour
{
    #region instance
    private static FilterManager m_instance;

    public static FilterManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }
    #endregion

    public GeneralPanelFilter generalPanelFilter;

    public int sortingItemValue;
    public int filterItemValue1;
    public int filterItemValue2;
    //public int filterItemValue3;

    public List<string> sortingItemNames;
    public List<string> filterItemNames1;
    public List<string> filterItemNames2;
    //public List<AbilityPoint> filterItemReal2;



    //public void UpdateReferenceList(List<Equipment> _referenceList)
    //{
    //    GlobalCommunicateManager.selectingScrollViewId = 0;

    //    sortingItemNames = new List<string> { Database.GetLocalizedText("Time"), Database.GetLocalizedText("Rarity") };
    //    filterItemNames1 = new List<string> { Database.GetLocalizedText("NoFilter") };
    //    filterItemNames2 = new List<string> { Database.GetLocalizedText("NoFilter") };
    //    //filterItemNames3 = new List<string> { Database.GetLocalizedText("NoFilter") };

    //    filterItemReal2 = new List<AbilityPoint> { null };
    //    //filterItemReal3 = new List<EquipmentFeature> { null };

    //    UpdateAllItemProperties(_referenceList);
    //    UpdateAllItemName(_referenceList);
    //    //UpdateAllItemType(_referenceList);

    //    generalPanelFilter.UpdateDropDownItems();
    //}

    //public void UpdateAllItemName(List<Equipment> _referenceList)
    //{
    //    foreach (Equipment _item in _referenceList)
    //    {
    //        if (!filterItemNames1.Contains(_item.name.GetString()))
    //        {
    //            filterItemNames1.Add(_item.name.GetString());
    //        }
    //    }
    //}

    //public void UpdateAllItemProperties(List<Equipment> _referenceList)
    //{
    //    foreach (Equipment _item in _referenceList)
    //    {
    //        foreach (AbilityPoint _ap in _item.GetAbilityPoints())
    //        {
    //            if (!filterItemReal2.Contains(_ap))
    //            {
    //                filterItemReal2.Add(_ap);
    //                filterItemNames2.Add(_ap.name.GetString());
    //            }
    //        }
    //    }
    //}


    //public List<Equipment> GetFilteredList(List<Equipment> _originalItemList)
    //{
    //    //get sorted list
    //    switch (sortingItemValue)
    //    {
    //        case 0://Time
    //            _originalItemList.Sort(SortByTime);
    //            break;
    //        case 1://Quality
    //            _originalItemList.Sort(SortBySize);
    //            break;
    //            //case 2://PropertyStrength
    //            //    _originalItemList.Sort(SortByShape);
    //            //    break;
    //    }

    //    //get filtered list

    //    //minimum star
    //    List<Equipment> filteredList = new List<Equipment>();
    //    foreach (Equipment _fc in _originalItemList)
    //    {
    //        filteredList.Add(_fc);
    //    }
    //    if (filterItemNames1[filterItemValue1] != Database.GetLocalizedText("NoFilter"))
    //    {
    //        foreach (Equipment _fc in _originalItemList)
    //        {
    //            if (filterItemNames1[filterItemValue1] != _fc.name.GetString())
    //            {
    //                filteredList.Remove(_fc);
    //            }
    //        }
    //    }
    //    if (filterItemReal2[filterItemValue2] != null)
    //    {
    //        foreach (Equipment _fc in _originalItemList)
    //        {
    //            bool _hasAbilityPoint = false;
    //            foreach (AbilityPoint _ap in _fc.GetAbilityPoints())
    //            {
    //                if (filterItemReal2[filterItemValue2] == _ap)
    //                {
    //                    _hasAbilityPoint = true;
    //                }
    //            }
    //            if (!_hasAbilityPoint)
    //            {
    //                filteredList.Remove(_fc);
    //            }
    //        }
    //    }

    //    return filteredList;
    //}

    //static int SortByTime(Equipment p1, Equipment p2)
    //{
    //    return p1.uid.CompareTo(p2.uid);
    //}

    //static int SortBySize(Equipment p1, Equipment p2)
    //{
    //    return p1.sizeCoe.CompareTo(p2.sizeCoe);
    //}
}
