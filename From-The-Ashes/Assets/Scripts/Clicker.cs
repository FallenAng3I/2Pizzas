using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public Resources resources;

    public Button[] Sawmill;
    public Button[] Mine;
    public Button[] OilWell;
    public Button[] OilFactory;
    public Button[] SteelFactory;
    public Button[] MilitaryFactory;

    public void ClickSawmill()
    {
        resources.Wood++;
        resources.txtWOOD.text = " " + resources.Wood.ToString();
    }

    public void ClickMine()
    {
        resources.Iron++;
        resources.txtOIL.text = " " + resources.Oil.ToString();
    }

    public void ClickOilWell()
    {
        resources.Oil++;
        resources.txtOIL.text = " " + resources.Oil.ToString();
    }

    public void ClickOilFactory()
    {
        if (resources.Oil >= 2)
        {
            resources.Fuel++;
            resources.Oil -=  2;
            resources.txtFUEL.text = " " + resources.Fuel.ToString();
            resources.txtOIL.text = " " + resources.Oil.ToString();
        }
    }

    public void ClickSteelFactory()
    {
        if (resources.Iron >= 3)
        {
            resources.Steel++;
            resources.Iron -= 3;
            resources.txtSTEEL.text = " " + resources.Steel.ToString();
            resources.txtWOOD.text = " " + resources.Wood.ToString();
        }
    }

    public void ClickMilitaryFactory()
    {
        if (resources.Steel >= 2)
        {
            resources.Ammos += 15;
            resources.Steel -= 2;
            resources.txtAMMOS.text = " " + resources.Ammos.ToString();
            resources.txtSTEEL.text = " " + resources.Steel.ToString();
        }
    }
}