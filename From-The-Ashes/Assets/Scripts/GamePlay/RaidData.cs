using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Raids/RaidData", fileName = "New RaidData")]
public class RaidData : ScriptableObject
{
    [SerializeField] private string raidDescription;
    [SerializeField] private Sprite raidSprite;
    [Header("Resources")]
    [SerializeField] private List<ResourceContainer> cost;
    [SerializeField] private List<ResourceContainer> reward;
    [SerializeField] private SpecialItem specialReward;

    public string RaidDescription { get => raidDescription; }
    public Sprite RaidSprite { get => raidSprite; }
    public List<ResourceContainer> Cost { get => cost; }
    public List<ResourceContainer> Reward { get => reward; }
    public SpecialItem SpecialReward { get => specialReward; }
}
