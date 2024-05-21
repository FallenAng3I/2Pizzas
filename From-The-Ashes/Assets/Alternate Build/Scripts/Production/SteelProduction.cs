using System;
using UnityEngine;

public class SteelProduction : Production
{
    [SerializeField] private int ironForSteel;

    public static Action ProduceSteel;

    public override void InvokeAction()
    {
        ProduceSteel.Invoke();
    }

    protected override void Produce(int quantity)
    {
        bool enoughResources = NewResources.IronOreNeeded.Invoke(ironForSteel * quantity);

        if (enoughResources)
        {
            NewResources.IronOreConsumed.Invoke(ironForSteel * quantity);
            NewResources.SteelProduced.Invoke(quantity);
        }
    }

    protected override void OnEnable()
    {
        ProduceSteel += ProduceOnClick;
    }

    protected override void OnDisable()
    {
        ProduceSteel -= ProduceOnClick;
    }
}
