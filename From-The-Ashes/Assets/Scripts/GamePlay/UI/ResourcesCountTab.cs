using System.Collections.Generic;
using UnityEngine;

public class ResourcesCountTab : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ResourceCountField resourceCountFieldPrefab;
    [SerializeField] private List<ResourceCountField> resourceCountFields;

    public void FillInData(List<ResourceContainer> resources)
    {
        ClearData();

        foreach (ResourceContainer resource in resources)
        {
            ResourceCountField resourceCountField = Instantiate(resourceCountFieldPrefab, content);
            resourceCountField.Resource = resource.Resource;
            resourceCountField.ResourceCountText.text = resource.Quantity.ToString();
            resourceCountFields.Add(resourceCountField);
        }
    }

    public void ClearData()
    {
        for (int i = resourceCountFields.Count - 1; i >= 0; i--)
        {
            Destroy(resourceCountFields[i].gameObject);
            resourceCountFields.RemoveAt(i);
        }
    }
}
