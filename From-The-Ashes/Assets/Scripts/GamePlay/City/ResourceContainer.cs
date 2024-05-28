using UnityEngine;

[System.Serializable]
public class ResourceContainer
{
    [SerializeField] private int quantity;
    [SerializeField] private Resource resource;

    public int Quantity { get => quantity; }
    public Resource Resource { get => resource; }
}
