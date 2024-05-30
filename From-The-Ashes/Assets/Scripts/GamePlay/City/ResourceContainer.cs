using UnityEngine;

[System.Serializable]
public class ResourceContainer
{
    [SerializeField] protected Resource resource;
    [SerializeField] protected int quantity;

    public ResourceContainer(Resource resource, int quantity)
    {
        this.resource = resource;
        this.quantity = quantity;
    }

    public Resource Resource { get => resource; }
    public int Quantity { get => quantity; }
}
