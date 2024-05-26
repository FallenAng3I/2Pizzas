using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingInformationsList : ScriptableObject
{
    [SerializeField] private List<BuildingInformation> buildingInformations;
    public List<BuildingInformation> BuildingInformations { get => buildingInformations; }

    public void AddNewBuildingInformation(BuildingInformation buildingInformation)
    {
        buildingInformations.Add(buildingInformation);
    }
}
