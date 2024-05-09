using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Action OnGameSuccess, OnGameFail;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void OnEnable()
    {
        OnGameSuccess += SuccessGame;
        OnGameFail += FailGame;
    }

    private void SuccessGame()
    {
        var money = PlayerPrefs.GetInt("TempMoney", 0);
        var coin = PlayerPrefs.GetInt("TempCoin", 0);

        PlayerPrefs.SetInt("Money", EconomyManager.instance.Money + money);
        PlayerPrefs.SetInt("Coin", EconomyManager.instance.Coin + coin);
    }

    private void FailGame()
    {
        PlayerPrefs.SetInt("TempCoin", 0);
        PlayerPrefs.SetInt("TempMoney", 0);
    }
}
