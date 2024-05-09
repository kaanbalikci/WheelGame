using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private int _coin;
    [SerializeField] private int _money;

    public int Coin 
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
            UIManager.Instance.CoinText.text = _coin.ToString();
        }
    }

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            UIManager.Instance.MoneyText.text = _money.ToString();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    void Start()
    {
        var newMoney = PlayerPrefs.GetInt("Money");
        var newCoin = PlayerPrefs.GetInt("Coin");

        DOTween.To(() => Money, x => Money = x, newMoney, .6f).SetEase(Ease.Linear);
        DOTween.To(() => Coin, x => Coin = x, newCoin, .6f).SetEase(Ease.Linear);
    }

    public bool CheckCoin(int needCoin)
    {
        return Coin - needCoin <= 0 ? false : true;
    }

    public void DecreaseCoin(int needCoin)
    {
        Coin -= needCoin;
    }

}
