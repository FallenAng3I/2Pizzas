using UnityEngine;

public class SteelProduction : Production
{
    [SerializeField] private int ironForSteel;

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.IronOreNeeded.Invoke(ironForSteel * quantity);

        if (enoughResources)
        {
            NewResources.IronOreConsumed.Invoke(ironForSteel * quantity);
            NewResources.SteelProduced.Invoke(quantity);
        }
    }
}
