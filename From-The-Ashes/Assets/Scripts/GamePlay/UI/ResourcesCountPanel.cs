using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesCountPanel : MonoBehaviour
{
    [SerializeField] private List<ResourceCountField> resourceCountFields;

    [SerializeField] private ResourceCountField resourceCountFieldPrefab;
    [SerializeField] private Transform contentTransform;

    [SerializeField] private bool useDecimalPrefixes;

    void Start()
    {
        Storage.OnResourceAmountÑhanged += UpdateResourceCount;

        foreach (StorageSlot storageSlot in Storage.Instance.StorageSlots)
        {
            UpdateResourceCount(storageSlot.Resource);
        }
    }

    private void UpdateResourceCount(Resource resource)
    {
        if (resourceCountFields.Any(ResourceCountField => ResourceCountField.Resource == resource))
        {
            int count = Storage.Instance.GetResourceAmount(resource);
            ResourceCountField resourceCountField = resourceCountFields.Find(ResourceCountField => ResourceCountField.Resource == resource);
            UpdateResourceCountText(count, resourceCountField);
        }
        else
        {
            CreateResourceCountField(resource);
        }
    }

    private void UpdateResourceCountText(int count, ResourceCountField resourceCountField)
    {
        if (useDecimalPrefixes)
        {
            if (count > 999)
            {
                if (count > 999999)
                {
                    resourceCountField.ResourceCountText.text = $"{count / 1000000}M";
                    return;
                }

                resourceCountField.ResourceCountText.text = $"{count / 1000}K";
                return;
            }
        }

        resourceCountField.ResourceCountText.text = $"{count}";
    }

    private void CreateResourceCountField(Resource resource)
    {
        ResourceCountField resourceCountField = Instantiate(resourceCountFieldPrefab, contentTransform);
        resourceCountField.Resource = resource;
        resourceCountFields.Add(resourceCountField);
        UpdateResourceCount(resource);
    }
}
