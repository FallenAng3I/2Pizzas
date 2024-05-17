using System.Collections.Generic;
using UnityEngine;

public class ResetBuildingInformation : MonoBehaviour
{
    [SerializeField] private List<BuildingInformation> buildingInformations = new List<BuildingInformation>();

    private void Awake()
    {
        foreach (var building in buildingInformations)
        {
            building.ResetCurrentCost();
        }
    }
}
