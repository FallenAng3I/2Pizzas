using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private Button militaryBaseButton;
    [Space]
    [SerializeField] private GameEvent somethingSelectedEvent;

    public static event Action OnMilitaryBaseBuilt;
    public static event Action OnMilitaryBaseSelected;
    public static event Action OnMilitaryBaseDeselected;

    private void Awake()
    {
        militaryBaseButton.onClick.AddListener(SelectMilitaryBase);
        DeselectMilitaryBase();
    }

    private void Start()
    {
        OnMilitaryBaseBuilt?.Invoke();
    }

    public void SelectMilitaryBase()
    {
        somethingSelectedEvent.Raise();
        OnMilitaryBaseSelected?.Invoke();
    }

    public void DeselectMilitaryBase()
    {
        OnMilitaryBaseDeselected?.Invoke();
    }
}
