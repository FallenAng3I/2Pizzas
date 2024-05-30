using System;
using UnityEngine;

[Serializable]
public class Cost
{
    [SerializeField] private ResourceContainer baseCost;
    [SerializeField] private int costIncrease;
    public ResourceContainer BaseCost { get => baseCost; }
    public int CostIncrease { get => costIncrease; }
}
