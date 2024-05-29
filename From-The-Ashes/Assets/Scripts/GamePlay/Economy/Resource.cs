using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Economy/Resource", fileName = "New Resource")]
public class Resource : ScriptableObject
{
    [SerializeField] private string resourceDescription;
    [SerializeField] private Sprite resourceIcon;

    public string ResourceDescription { get => resourceDescription; }
    public Sprite ResourceIcon { get => resourceIcon; }
}
