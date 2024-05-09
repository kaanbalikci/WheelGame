using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Transform _rewardParent;
    [SerializeField] private GameObject _rewardImagePrefab;

    [SerializeField] private List<RewardImage> _rewardImageList = new();

    private void Start()
    {
        UIManager.Instance.ExitButton.onClick.AddListener(() =>
        {
            GameManager.instance.OnGameSuccess?.Invoke();
        });
    }

    public RewardImage SetRewardImage(ItemProperties itemProperties)
    {
        foreach (var item in _rewardImageList)
        {
            if (item.CurrentItemProperties == itemProperties)
            {
                return IncreaseRewardImage(item);
            }
        }
        return SpawnNewRewardImage(itemProperties);
    }

    private RewardImage SpawnNewRewardImage(ItemProperties itemProperties)
    {
        var newReward = Instantiate(_rewardImagePrefab, _rewardParent);
        RewardImage newImage = newReward.GetComponent<RewardImage>();
        _rewardImageList.Add(newImage);
        newImage.CurrentItemProperties = itemProperties;

        return SetImageToParent(newImage);
    }

    private RewardImage IncreaseRewardImage(RewardImage rewardImage)
    {
        return SetImageToParent(rewardImage);
    }

    private RewardImage SetImageToParent(RewardImage rewardImage)
    {

        if (rewardImage.CurrentItemProperties.ItemType == ItemType.Coin)
        {
            rewardImage.GetComponent<Transform>().SetSiblingIndex(1);
            rewardImage.SaveStat("TempCoin");
            
        }
        else if (rewardImage.CurrentItemProperties.ItemType == ItemType.Money)
        {
            rewardImage.GetComponent<Transform>().SetSiblingIndex(0);
            rewardImage.SaveStat("TempMoney");
        }
        else
        {
            rewardImage.GetComponent<Transform>().SetSiblingIndex(2);
        }

        return rewardImage;
    }

}
