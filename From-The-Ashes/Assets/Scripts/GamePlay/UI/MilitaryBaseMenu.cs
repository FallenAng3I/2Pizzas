using System;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryBaseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindowObject;
    [Space]

    [SerializeField] private Button closeButton;
    [SerializeField] private Button raidsButton;
    [Space]

    [SerializeField] private Transform defaultTarget;
    [SerializeField] private Transform moveTarget;

    public static event Action OnRaidsButtonClicked;
    public static event Action OnMenuOpened;
    public static event Action OnMenuClosed;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseMenu);
        raidsButton.onClick.AddListener(() => OnRaidsButtonClicked?.Invoke());

        CloseMenu();
    }

    private void OpenMenu()
    {
        menuWindowObject.SetActive(true);
        OnMenuOpened?.Invoke();
    }

    private void CloseMenu()
    {
        menuWindowObject.SetActive(false);
        OnMenuClosed?.Invoke();
    }

    private void MoveMenu()
    {
        menuWindowObject.transform.position = moveTarget.position;
    }

    private void MoveMenuBack()
    {
        menuWindowObject.transform.position = defaultTarget.position;
    }

    private void OnEnable()
    {
        MilitaryBase.OnMilitaryBaseSelected += OpenMenu;
        MilitaryBase.OnMilitaryBaseDeselected += CloseMenu;
        RaidsMenu.OnRaidsMenuOpened += MoveMenu;
        RaidsMenu.OnRaidsMenuClosed += MoveMenuBack;
    }

    private void OnDisable()
    {
        MilitaryBase.OnMilitaryBaseSelected -= OpenMenu;
        MilitaryBase.OnMilitaryBaseDeselected -= CloseMenu;
        RaidsMenu.OnRaidsMenuOpened -= MoveMenu;
        RaidsMenu.OnRaidsMenuClosed -= MoveMenuBack;
    }
}
