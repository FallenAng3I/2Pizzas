using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountField : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceCountText;

    public Resource Resource
    {
        get => resource;
        set
        {
            if (resource == null)
            {
                resource = value;
                resourceImage.sprite = resource.ResourceIcon;
            }
        }
    }
    public TextMeshProUGUI ResourceCountText { get => resourceCountText;  }
}