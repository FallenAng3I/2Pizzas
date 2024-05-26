using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCountField : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceCountText;
    [SerializeField] public Resource resource;

    public Image ResourceIcon { get => resourceIcon; }
    public TextMeshProUGUI ResourceCountText { get => resourceCountText; }
}
