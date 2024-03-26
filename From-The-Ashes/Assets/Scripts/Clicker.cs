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

    public void IncreaseWoodFromSawmill()
    {
        resources.Wood++;
        resources.txtWOOD.text = " " + resources.Wood.ToString();
    }

    public void IncreaseIronFromMine()
    {
        resources.Iron++;
        resources.txtIRON.text = " " + resources.Iron.ToString();
    }

    public void IncreaseOilFromOilWell()
    {
        resources.Oil++;
        resources.txtOIL.text = " " + resources.Oil.ToString();
    }
}    