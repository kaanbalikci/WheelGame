using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Button ExitButton;

    public SpinButton SpinButton;
    public RewardPanel RewardPanel;

    public TMP_Text MoneyText;
    public TMP_Text CoinText;

    public Volume Volume;

    [SerializeField] private Image _blackScreen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnEnable()
    {
        WheelManager.instance.OnLevelBarUpdate += () => { ExitButton.interactable = true; };
        GameManager.instance.OnGameFail += BlackScreen;
        GameManager.instance.OnGameSuccess += BlackScreen;
    }
    void Start()
    {
        SpinButton.Show();
        
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Z)) 
        {
            var x = PlayerPrefs.GetInt("TempCoin");
            Debug.Log(x);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            var x = PlayerPrefs.GetInt("TempMoney");
            Debug.Log(x);
        }
    }
    public void BlackScreen()
    {
        _blackScreen.gameObject.SetActive(true);

        _blackScreen.DOFade(1, .5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }

}
