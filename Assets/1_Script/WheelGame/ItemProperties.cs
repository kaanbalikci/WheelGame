using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,
    Money,
    Tier1,
    Tier2,
    Tier3,
    Bomb
}

[CreateAssetMenu(menuName = "Wheel/Wheel Slot Properties")]
public class ItemProperties : ScriptableObject
{
    [Header("ITEM TYPE")]
    public ItemType ItemType;

    [Space(20)]
    [Header("Item Properties")]
    public string SlotImageName;
    public int BaseRewardCount;
    public int IncreaseRewardCount;

    [Space(20)]
    [Header("Card Properties")]
    public Color OutlineColor;
    public bool IsGlowOpen;


}
