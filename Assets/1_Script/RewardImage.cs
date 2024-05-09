using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardImage : MonoBehaviour
{
    public ItemProperties CurrentItemProperties;

    [SerializeField] private TMP_Text _rewardText;

    private string _saveName;
    private bool canSave;

    private int _rewardCount;
    public int RewardCount
    {
        get
        {
            return _rewardCount;
        }
        set
        {
            _rewardCount = value;
            _rewardText.text = _rewardCount.ToString();
        }
    }

    void Awake()
    {
        RewardCount = 0;
    }

    public void UpdateText(int newReward)
    {
        var sumReward = newReward + RewardCount;

        if (canSave)
        {
            PlayerPrefs.SetInt(_saveName, sumReward);

            var x = PlayerPrefs.GetInt(_saveName);
        }

        DOTween.To(() => RewardCount, x => RewardCount = x, sumReward, .6f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _rewardText.transform.DOScale(.2f, .03f).SetRelative().SetLoops(2, LoopType.Yoyo);
        });
    }

    public void SaveStat(string prefName)
    {
        _saveName = prefName;
        canSave = true;
        //var reward = _tempNewReward + RewardCount;
        //Debug.Log(reward);
        //PlayerPrefs.SetInt(prefName, reward);

        //var x = PlayerPrefs.GetInt(prefName);
        //Debug.Log(x);
    }
}
