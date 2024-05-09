using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WheelSlot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private TMP_Text _slotRewardText;

    private ItemProperties ItemProperties;
    private int _reward;

    private SpriteAtlas _atlas;

    private void Awake()
    {
        _atlas = GetComponentInParent<WheelSlotSetting>().Atlas;
    }
    public void SetItemProperties(ItemProperties newItem)
    {
        ItemProperties = newItem;
        _reward = Random.Range(ItemProperties.BaseRewardCount, ItemProperties.BaseRewardCount * WheelManager.instance.ZoneCount);

        if (_reward > 0)
        {
            _slotRewardText.text = "x"+_reward.ToString();
        }
        else _slotRewardText.text = "";

        _slotImage.sprite = _atlas.GetSprite(ItemProperties.SlotImageName);
    }

    public ItemProperties GetItemProp()
    {
        return ItemProperties;
    }
    public int GetReward()
    {
        return _reward;
    }
}
