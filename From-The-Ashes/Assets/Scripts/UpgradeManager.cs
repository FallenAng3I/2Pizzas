using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeManager : MonoBehaviour
{
    public Resources resources;
    
    public bool doubleClick = false;
    public bool passiveProfit = false;

    public void DoubleClickEnable()
    {
        doubleClick = true;
    }

    public void PassiveProfitEnable()
    {
        passiveProfit = true;
    }
}
