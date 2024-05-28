using UnityEngine;

[CreateAssetMenu(fileName = "New SpecialItem", menuName = "Raids/SpecialItem")]
public class SpecialItem : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string decription;

    public Sprite Icon { get => icon; }
    public string Decription { get => decription; }
}
