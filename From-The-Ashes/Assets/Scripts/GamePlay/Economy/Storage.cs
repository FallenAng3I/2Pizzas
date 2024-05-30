using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
[Serializable]
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

public class Storage : MonoBehaviour
{
    [SerializeField] private List<StorageSlot> storageSlots;
    public List<StorageSlot> StorageSlots { get => storageSlots; }

    public static event Action<Resource> OnResourceAmount—hanged;

    // —ËÌ„ÎÚÓÌ, ‚ ·Û‰Û˘ÂÏ ‚ÓÁÏÓÊÌÓ ËÒÔÓÎ¸ÁÓ‚‡ÌËÂ ·ÓÎÂÂ „Ë·ÍÓ„Ó ScriptableObject
    public static Storage Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddResource(Resource resource, int quantity)
    {
        if (quantity < 0) return;

        if (storageSlots.Any(StorageSlot => StorageSlot.Resource == resource))
        {
            storageSlots.Find(StorageSlot => StorageSlot.Resource == resource).Quantity += quantity;
            OnResourceAmount—hanged?.Invoke(resource);
        }
        else
        {
            CreateStorageSlot(resource, quantity);
        }
    }

    public int GetResourceAmount(Resource resource)
    {
        if (storageSlots.Any(StorageSlot => StorageSlot.Resource == resource))
        {
            return storageSlots.Find(StorageSlot => StorageSlot.Resource == resource).Quantity;
        }
        else
        {
            CreateStorageSlot(resource, 0);
            return 0;
        }
    }

    public void SubtractResource(Resource resource, int quantity)
    {
        if (storageSlots.Any(StorageSlot => StorageSlot.Resource == resource))
        {
            storageSlots.Find(StorageSlot => StorageSlot.Resource == resource).Quantity -= quantity;
            OnResourceAmount—hanged?.Invoke(resource);
        }
        else
        {
            CreateStorageSlot(resource, 0);
        }
    }

    public void CreateStorageSlot(Resource resource, int quantity)
    {
        StorageSlot newSlot = new StorageSlot(resource, quantity);
        storageSlots.Add(newSlot);
        OnResourceAmount—hanged?.Invoke(resource);
    }

    public void RemoveExcessSlots()
    {
        foreach (StorageSlot slot in storageSlots.ToList())
        {
            if (slot.Resource == null)
            {
                storageSlots.Remove(slot);
            }
            else
            {
                List<StorageSlot> duplicates = storageSlots.FindAll(StorageSlot => StorageSlot.Resource == slot.Resource);
                if (duplicates.Count > 1)
                {
                    Resource resource = slot.Resource;
                    int quantity = 0;

                    foreach (StorageSlot duplicate in duplicates.ToList())
                    {
                        quantity += duplicate.Quantity;
                        storageSlots.Remove(duplicate);
                    }

                    StorageSlot aggregatedSlot = new StorageSlot(resource, quantity);
                    storageSlots.Add(aggregatedSlot);
                }
            }
        }
    }
}
