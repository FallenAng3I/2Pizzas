using UnityEngine;

[System.Serializable]
public class StorageSlot
{
    [SerializeField] private Resource resource;
    [SerializeField] private int quantity;

    public StorageSlot(Resource resource, int quantity)
    {
        this.resource = resource;
        this.quantity = quantity;
    }

    public Resource Resource { get => resource; }
    public int Quantity 
    { 
        get => quantity;
        set
        {
            if (value >= 0)
            {
                quantity = value;
            }
        } 
    }
}
