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
    }
    

    public void ClickMine()
    {
        resources.Iron++;
    }

    public void ClickOilWell()
    {
        resources.Oil++;
    }

    public void ClickOilFactory()
    {
        if (resources.Oil >= 2)
        {
            resources.Fuel++;
            resources.Oil -=  2;
        }
    }

    public void ClickSteelFactory()
    {
        if (resources.Iron >= 3)
        {
            resources.Steel++;
            resources.Iron -= 3;
        }
    }

    public void ClickLeadMine()
    {
        resources.LeadOre++;
    }

    public void ClickLeadFactory()
    {
        if (resources.LeadOre >= 3) 
        {          
            resources.Lead++;
            resources.LeadOre -= 3;
        }
    }

    public void ClickMilitaryFactory()
    {
        if (resources.Lead >= 2)
        {
            resources.Ammos += 15;
            resources.Lead -= 2;
        }
    }
    
    
    
    /*   инструкция, чтобы добавить механику нового типа здания:
     *
     *   1) добавить метод для реализации логики вашего здания:
     *
     *   public void ClickЗДАНИЕ()
     *   {
     *      *логика вашего здания. например:
     *      resources.ВашРесурс++;
     *   }
     *
     *   Готово.
     *   Для реализации логики сложнее примера, обратитесь к лиду или адаптируйте из существующих механик.
     */
}