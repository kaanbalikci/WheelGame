using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardControl : MonoBehaviour
{
    private RectTransform _rectTransform;

    [Header("Card Visual")]
    [SerializeField] private Image _cardIconImage;
    [SerializeField] private Image _cardOutlineImage;
    [SerializeField] private GameObject _cardGlow;
    [SerializeField] private SpriteAtlas _cardIconAtlas;
    [SerializeField] private Color _cardBaseColor;

    [Space(10)]
    [SerializeField] private Image[] _miniCards;
    [SerializeField] private float _spreadRadius;

    [Space(10)]
    [Header("Buttons")]
    [SerializeField] private Button _giveUpButton;
    [SerializeField] private Button _reviveButton;
    [SerializeField] private TMP_Text _reviveCoinText;
    private int reviveCount;

    [HideInInspector] public ItemProperties CurrentItem;

    private int _currentReward;
    private Transform _tempParent;
    private RewardImage _currentImage;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _cardBaseColor = GetComponent<Image>().color;
    }
    private void OnEnable()
    {
        WheelManager.instance.OnCardAnimStart += GetItemObject;
        _giveUpButton.onClick.AddListener(() => GiveUpButton());
        _reviveButton.onClick.AddListener(() => ReviveButton());
    }
    private void GetItemObject(ItemProperties item, int reward)
    {
        _currentReward = reward;
        CurrentItem = item;
        SetCard();
    }
    private void SetCard()
    {
        _cardIconImage.sprite = _cardIconAtlas.GetSprite(CurrentItem.SlotImageName);
        _cardGlow.SetActive(CurrentItem.IsGlowOpen);
        _cardOutlineImage.color = CurrentItem.OutlineColor;
        ShowCard();
    }

    private void ShowCard()
    {
        var seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            _rectTransform.DOAnchorPos(Vector2.zero, .4f).SetEase(Ease.OutBack);
            if (CurrentItem.ItemType == ItemType.Bomb)
            {
                BombEffect();
                seq.Kill();
            }
        });
        seq.AppendInterval(.45f);
        seq.AppendCallback(() =>
        {
            MinicardSpawn();
        });
        seq.AppendInterval(.85f);
        seq.AppendCallback(() =>
        {
            MiniCardMoveToRewardList();
        });
        seq.AppendInterval(.6f);
        seq.AppendCallback(() =>
        {
            _currentImage.UpdateText(_currentReward);
        });
        seq.AppendInterval(1f);
        seq.Append(_rectTransform.DOAnchorPos(new Vector2(0, -1000), .5f).SetEase(Ease.OutBack));
        seq.AppendCallback(() =>
        { 
            WheelManager.instance.OnLevelBarUpdate?.Invoke();
        });

    }

    private void MinicardSpawn()
    {
        foreach (var miniCard in _miniCards)
        {
            miniCard.sprite = _cardIconImage.sprite;

            Vector3 randomDir = Random.insideUnitCircle.normalized * _spreadRadius;
            Vector3 targetPosition = Vector3.zero + randomDir;

            miniCard.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0);

            miniCard.transform.DOScale(Vector3.one, .5f).From(Vector3.zero);
            miniCard.GetComponent<RectTransform>().DOAnchorPos(targetPosition, .8f);
        }
    }
    private void MiniCardMoveToRewardList()
    {
        _currentImage = UIManager.Instance.RewardPanel.SetRewardImage(CurrentItem);
        _currentImage.GetComponent<Image>().sprite = _cardIconImage.sprite;

        DOVirtual.DelayedCall(.05f, () =>
        {
            _tempParent = _miniCards[0].transform.parent;

            foreach (var miniCard in _miniCards)
            {
                miniCard.transform.SetParent(_currentImage.transform);
                miniCard.transform.DOMove(_currentImage.transform.position, .55f).OnComplete(() =>
                {
                    miniCard.transform.DOScale(Vector3.zero, .1f);
                    miniCard.transform.SetParent(_tempParent);
                });
            }
        });
    }

    private void BombEffect()
    {
        transform.parent.GetComponent<Image>().DOFade(.99f, .3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GetComponent<Image>().color = Color.red;
            _giveUpButton.gameObject.SetActive(true);
            _reviveButton.gameObject.SetActive(true);
            _reviveCoinText.text = (75 * (reviveCount + 1)).ToString();
            _reviveButton.interactable = EconomyManager.instance.CheckCoin(75 * (reviveCount + 1));
        });

    }

    private void GiveUpButton()
    {
        GameManager.instance.OnGameFail?.Invoke();
    }
    private void ReviveButton()
    {
        transform.parent.GetComponent<Image>().DOFade(0f, .05f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GetComponent<Image>().color = _cardBaseColor;
            _giveUpButton.gameObject.SetActive(false);
            _reviveButton.gameObject.SetActive(false);
            EconomyManager.instance.DecreaseCoin(75 * (reviveCount + 1));
            _rectTransform.DOAnchorPos(new Vector2(0, -1000), .5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                WheelManager.instance.OnLevelBarUpdate?.Invoke();
            });
        });
    }

}
