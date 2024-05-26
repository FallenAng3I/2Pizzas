using System.Collections.Generic;
using UnityEngine;

// ���� ������ ������������� ScriptableObject-� ��� ����� � PlayMode
public class BuildingInformationsReseter : MonoBehaviour
{
    [SerializeField] private BuildingInformationsList buildingInformationsList;

    private void Awake()
    {
        foreach (var buildingInformation in buildingInformationsList.BuildingInformations)
        {
            buildingInformation.ResetAllValues();
        }
    }
}
