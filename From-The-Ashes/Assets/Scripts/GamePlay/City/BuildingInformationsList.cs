using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingInformationsList : ScriptableObject
{
    [SerializeField] private List<BuildingData> buildingInformations;
    public List<BuildingData> BuildingInformations { get => buildingInformations; }

    public void AddNewBuildingInformation(BuildingData buildingInformation)
    {
        buildingInformations.Add(buildingInformation);
    }
}
