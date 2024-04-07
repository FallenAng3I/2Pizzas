using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOONNAME : MonoBehaviour
{
    public Buildings build;
    
    public Button[] typeButtons;
    
    public void AssignBuildingMethod(int buildingID)
    {
        if (buildingID < 0 || buildingID >= typeButtons.Length)
        {
            Debug.LogError("Invalid building index!");
            return;
        }
        
        Button selectedBuildingButton = typeButtons[buildingID];
        
        switch (buildingID)
        {
            case 0: // Sawmill / Лесопилка
                selectedBuildingButton.onClick.AddListener(build.ClickSawmill);
                break;
            case 1: // Mine / Шахта
                selectedBuildingButton.onClick.AddListener(build.ClickMine);
                break;
            case 2: // OilWell / Нефтянная скважина
                selectedBuildingButton.onClick.AddListener(build.ClickOilWell);
                break;
            case 3: // OilFactory / Нефтеперерабатывающий завод
                selectedBuildingButton.onClick.AddListener(build.ClickOilFactory);
                break;
            case 4: // SteelFactory / Сталелитейный завод
                selectedBuildingButton.onClick.AddListener(build.ClickSteelFactory);
                break;
            case 5: // MilitaryFactory / Военный завод
                selectedBuildingButton.onClick.AddListener(build.ClickMilitaryFactory);
                break;
            default:
                Debug.LogWarning("Building index not implemented!");
                break;
        }
    }
}