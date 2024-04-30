using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Buildings : MonoBehaviour
{
    public Resources resources;
    
    public void ClickSawmill()
    {
        resources.Wood++;
        resources.txtWOOD.text = " " + resources.Wood.ToString();
    }
    

    public void ClickMine()
    {
        resources.Iron++;
        resources.txtIRON.text = " " + resources.Iron.ToString();
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
            resources.txtIRON.text = " " + resources.Iron.ToString();
        }
    }

    public void ClickLeadMine()
    {
        resources.LeadOre++;
        resources.txtLEADORE.text = " " + resources.LeadOre.ToString();
    }

    public void ClickLeadFactory()
    {
        if (resources.LeadOre >= 3) 
        {          
            resources.Lead++;
            resources.LeadOre -= 3;
            resources.txtLEAD.text = " " + resources.Lead.ToString();
            resources.txtLEADORE.text = " " + resources.LeadOre.ToString();            
        }
    }

    public void ClickMilitaryFactory()
    {
        if (resources.Lead >= 2)
        {
            resources.Ammos += 15;
            resources.Lead -= 2;
            resources.txtAMMOS.text = " " + resources.Ammos.ToString();
            resources.txtLEAD.text = " " + resources.Lead.ToString();
        }
    }
}