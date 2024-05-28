using System;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private Button militaryBaseButton;
    [SerializeField] private Image selectionIndicator;
    [Space]
    [SerializeField] private VoidEvent somethingSelectedEvent;

    public static event Action OnMilitaryBaseBuilt;
    public static event Action OnMilitaryBaseSelected;
    public static event Action OnMilitaryBaseDeselected;

    private void Awake()
    {
        militaryBaseButton.onClick.AddListener(SelectMilitaryBase);
        somethingSelectedEvent.OnEventRaised += DeselectMilitaryBase;
    }

    private void OnEnable()
    {
        OnMilitaryBaseBuilt?.Invoke();
    }

    private void SelectMilitaryBase()
    {
        somethingSelectedEvent.RaiseEvent();
        selectionIndicator.enabled = true;
        OnMilitaryBaseSelected?.Invoke();
    }

    private void DeselectMilitaryBase()
    {
        selectionIndicator.enabled = false;
        OnMilitaryBaseDeselected?.Invoke();
    }
}
