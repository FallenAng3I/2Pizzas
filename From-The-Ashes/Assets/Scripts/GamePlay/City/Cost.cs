using UnityEngine;

[System.Serializable]
public class Cost
{
    [SerializeField] private Resource resource;
    [SerializeField] private int baseQuantity;
    [SerializeField] private int quantityIncrease;
    private int currentQuantity;

    public Resource Resource { get => resource; }
    public int Quantity { get => currentQuantity; }

    public void IncreaseCost()
    {
        currentQuantity += quantityIncrease;
    }

    public void DecreaseCost()
    {
        currentQuantity -= quantityIncrease;
    }

    public void ResetCost()
    {
        currentQuantity = baseQuantity;
    }
}
