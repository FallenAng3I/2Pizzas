using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingMenuView : MonoBehaviour
{
    [SerializeField] private BuildingMenuItem _contextMenu;

    private void Awake()
    {
        BuildingFieldView.OnBuildingSelected.AddListener(OnSelected);
        BuildingFieldView.OnBuildingDeselected.AddListener(OnDeselected);
        gameObject.SetActive(false);
    }
    
    private void OnSelected(BuildingFieldView view)
    {
        gameObject.SetActive(true);
    }   
    
    private void OnDeselected(BuildingFieldView view)
    {
        if (view == null)
        {
            gameObject.SetActive(false);
        }
    }
}