using System;
using TMPro;
using UnityEngine;

public class NewResources : MonoBehaviour
{
    [SerializeField] private int wood;
    [SerializeField] private int ironOre;
    [SerializeField] private int steel;
    [SerializeField] private int oil;
    [SerializeField] private int fuel;
    [SerializeField] private int leadOre;
    [SerializeField] private int lead;
    [SerializeField] private int ammunition;

    [SerializeField] private TextMeshProUGUI woodCountText;
    [SerializeField] private TextMeshProUGUI ironOreCountText;
    [SerializeField] private TextMeshProUGUI steelCountText;
    [SerializeField] private TextMeshProUGUI oilCountText;
    [SerializeField] private TextMeshProUGUI fuelCountText;
    [SerializeField] private TextMeshProUGUI leadOreCountText;
    [SerializeField] private TextMeshProUGUI leadCountText;
    [SerializeField] private TextMeshProUGUI ammunitionCountText;

    public static Action<int> WoodProduced;
    public static Action<int> IronOreProduced;
    public static Action<int> SteelProduced;
    public static Action<int> OilProduced;
    public static Action<int> FuelProduced;
    public static Action<int> LeadOreProduced;
    public static Action<int> LeadProduced;
    public static Action<int> AmmunitionProduced;

    public static Action<int> WoodConsumed;
    public static Action<int> IronOreConsumed;
    public static Action<int> SteelConsumed;
    public static Action<int> OilConsumed;
    public static Action<int> FuelConsumed;
    public static Action<int> LeadOreConsumed;
    public static Action<int> LeadConsumed;
    public static Action<int> AmmunitionConsumed;

    public static Predicate<int> WoodNeeded;
    public static Predicate<int> IronOreNeeded;
    public static Predicate<int> SteelNeeded;
    public static Predicate<int> OilNeeded;
    public static Predicate<int> FuelNeeded;
    public static Predicate<int> LeadOreNeeded;
    public static Predicate<int> LeadNeeded;
    public static Predicate<int> AmmunitionNeeded;

    private void Awake()
    {
        WoodProduced += ((int amount) => AddResource(ref wood, amount, woodCountText));
        IronOreProduced += ((int amount) => AddResource(ref ironOre, amount, ironOreCountText));
        SteelProduced += ((int amount) => AddResource(ref steel, amount, steelCountText));
        OilProduced += ((int amount) => AddResource(ref oil, amount, oilCountText));
        FuelProduced += ((int amount) => AddResource(ref fuel, amount, fuelCountText));
        LeadOreProduced += ((int amount) => AddResource(ref leadOre, amount, leadOreCountText));
        LeadProduced += ((int amount) => AddResource(ref lead, amount, leadCountText));
        AmmunitionProduced += ((int amount) => AddResource(ref ammunition, amount, ammunitionCountText));

        WoodConsumed += ((int amount) => SubtractResource(ref wood, amount, woodCountText));
        IronOreConsumed += ((int amount) => SubtractResource(ref ironOre, amount, ironOreCountText));
        SteelConsumed += ((int amount) => SubtractResource(ref steel, amount, steelCountText));
        OilConsumed += ((int amount) => SubtractResource(ref oil, amount, oilCountText));
        FuelConsumed += ((int amount) => SubtractResource(ref fuel, amount, fuelCountText));
        LeadOreConsumed += ((int amount) => SubtractResource(ref leadOre, amount, leadOreCountText));
        LeadConsumed += ((int amount) => SubtractResource(ref lead, amount, leadCountText));
        AmmunitionConsumed += ((int amount) => SubtractResource(ref ammunition, amount, ammunitionCountText));

        WoodNeeded += ((int amount) => CheckIfEnoughResources(ref wood, amount));
        IronOreNeeded += ((int amount) => CheckIfEnoughResources(ref ironOre, amount));
        SteelNeeded += ((int amount) => CheckIfEnoughResources(ref steel, amount));
        OilNeeded += ((int amount) => CheckIfEnoughResources(ref oil, amount));
        FuelNeeded += ((int amount) => CheckIfEnoughResources(ref fuel, amount));
        LeadOreNeeded += ((int amount) => CheckIfEnoughResources(ref leadOre, amount));
        LeadNeeded += ((int amount) => CheckIfEnoughResources(ref lead, amount));
        AmmunitionNeeded += ((int amount) => CheckIfEnoughResources(ref ammunition, amount));
    }

    private void Start()
    {
        UpdateResourceCountText(wood, woodCountText);
        UpdateResourceCountText(ironOre, ironOreCountText);
        UpdateResourceCountText(steel, steelCountText);
        UpdateResourceCountText(oil, oilCountText);
        UpdateResourceCountText(fuel, fuelCountText);
        UpdateResourceCountText(leadOre, leadOreCountText);
        UpdateResourceCountText(lead, leadCountText);
        UpdateResourceCountText(ammunition, ammunitionCountText);
    }

    private void AddResource(ref int resource, int amount, TextMeshProUGUI resourceCountText)
    {
        resource += amount;
        UpdateResourceCountText(resource, resourceCountText);
    }

    private void SubtractResource(ref int resource, int amount, TextMeshProUGUI resourceCountText)
    {
        resource -= amount;
        UpdateResourceCountText(resource, resourceCountText);
    }

    private bool CheckIfEnoughResources(ref int resource, int amount)
    {
        if (resource - amount < 0)
        {
            return false;
        }

        return true;
    }

    private void UpdateResourceCountText(int resource, TextMeshProUGUI resourceCountText)
    {
        resourceCountText.text = resource.ToString();
    }
}
