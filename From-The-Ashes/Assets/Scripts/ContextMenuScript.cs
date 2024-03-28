using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContextMenuScript : MonoBehaviour
{
    [SerializeField] private ContextMenuItem _contextMenu;

    private void Awake()
    {
        SelectedAreaBuild.OnBuildingSelected.AddListener(OnSelected);
        SelectedAreaBuild.OnBuildingDeselected.AddListener(OnDeselected);
    }

    private void OnSelected(SelectedAreaBuild view)
    {
        gameObject.SetActive(true);
    }

    private void OnDeselected(SelectedAreaBuild view)
    {
        gameObject.SetActive(false);
    }
}
