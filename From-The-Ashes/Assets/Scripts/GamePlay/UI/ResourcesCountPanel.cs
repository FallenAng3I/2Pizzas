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
        Storage.OnResourceAmountСhanged += UpdateResourceCount;

        foreach (StorageSlot storageSlot in Storage.Instance.StorageSlots)
        {
            UpdateResourceCount(storageSlot.Resource);
        }
    }

    private void UpdateResourceCount(Resource resource)
    {
        if (resourceCountFields.Any(ResourceCountField => ResourceCountField.resource == resource))
        {
            int count = Storage.Instance.GetResourceAmount(resource);
            ResourceCountField resourceCountField = resourceCountFields.Find(ResourceCountField => ResourceCountField.resource == resource);
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

    // Этот кусок кода работает криво и я не знаю, почему
    // По идее он должен создавать новое поле ресурса, но зачем-то он создаёт два: одно с ресурсом и одно пустое
    // При этом он не может засунуть спрайт в изображение и создаёт поле в неправильном месте
    private void CreateResourceCountField(Resource resource)
    {
        ResourceCountField resourceCountField = Instantiate(resourceCountFieldPrefab, contentTransform);
        resourceCountField.resource = resource;
        resourceCountField.ResourceIcon.sprite = resource.ResourceIcon;
        UpdateResourceCountText(Storage.Instance.GetResourceAmount(resource), resourceCountField);
    }
}
