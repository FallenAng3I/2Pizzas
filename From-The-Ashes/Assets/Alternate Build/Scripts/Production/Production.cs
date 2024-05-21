using System.Collections;
using UnityEngine;

public abstract class Production : MonoBehaviour
{
    [Header("Click Production")]
    [SerializeField] protected int clickProductionQuantity;
    [SerializeField] protected int clickProductionQuantityIncrease;

    protected bool passiveProductionUpgraded;
    [HideInInspector] public bool passiveProductionEnabled = true;

    [Header("Passive Production")]
    [SerializeField] protected int passiveProductionTime;
    [SerializeField] protected int passiveProductionQuantity;
    [SerializeField] protected int passiveProductionQuantityIncrease;

    protected IEnumerator ProductionCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(passiveProductionTime);
            if (passiveProductionUpgraded && passiveProductionEnabled) Produce(passiveProductionQuantity);
        }
    }

    protected void Start()
    {
        StartCoroutine(ProductionCycle());
    }

    protected void ProduceOnClick()
    {
        Produce(clickProductionQuantity);
    }

    public abstract void InvokeAction();

    protected abstract void Produce(int quantity);

    protected abstract void OnEnable();

    protected abstract void OnDisable();

    public void UpgradeClick()
    {
        clickProductionQuantity += clickProductionQuantityIncrease;// ћожно использовать другую форму апгрейда, например, удваивать количество продукта
    }

    public void UpgradePassive()
    {
        if (!passiveProductionUpgraded)
        {
            passiveProductionUpgraded = true;
        }
        else
        {
            passiveProductionQuantity += passiveProductionQuantityIncrease; // ћожно использовать другую форму апгрейда, например, удваивать количество продукта
        }
    }

    public void TogglePassiveProduction()
    {
        passiveProductionEnabled = !passiveProductionEnabled;
    }
}
