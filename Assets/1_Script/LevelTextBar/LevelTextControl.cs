using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextControl : MonoBehaviour
{
    [SerializeField] private Color[] _textColors;

    //Mask Image
    [SerializeField] private List<RectTransform> _maskImages = new();
    [SerializeField] private Color[] _maskImageZoneColor;

    private RectTransform _currentMaskImage;
    private RectTransform _nextMaskImage;

    private List<TMP_Text> _zoneCountTextList = new();

    private Color _currentZoneIndexColor;

    private void Awake()
    {
        _zoneCountTextList = GetComponentsInChildren<TMP_Text>().ToList();
    }
    private void OnEnable()
    {
        WheelManager.instance.OnLevelBarUpdate += NewZoneTextAnim;
        WheelManager.instance.OnStartNewZone += ChangeMaskImage;
    }
    void Start()
    {
        _currentMaskImage = _maskImages[0];
        _nextMaskImage = _maskImages[1];

        _currentZoneIndexColor = Color.white;

        for (int i = 0; i < _zoneCountTextList.Count; i++)
        {
            _zoneCountTextList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(100*i,0);
            _zoneCountTextList[i].text = (i+1).ToString();

            if ((i+1) % 30 == 0)
                _zoneCountTextList[i].color = _textColors[2];
            else if ((i+1) % 5 == 0)
                _zoneCountTextList[i].color = _textColors[1];
            else
                _zoneCountTextList[i].color = _textColors[0];
        }
    }

    public void NewZoneTextAnim()
    {
        var seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            SetMaskZoneImageColor(_nextMaskImage.GetComponent<Image>());

            _zoneCountTextList[WheelManager.instance.ZoneCount - 1].DOFade(.5f, .1f);
            _zoneCountTextList[WheelManager.instance.ZoneCount - 1].color = _currentZoneIndexColor;

            _currentZoneIndexColor = _zoneCountTextList[WheelManager.instance.ZoneCount].color;
            _zoneCountTextList[WheelManager.instance.ZoneCount].color = Color.black;


            _maskImages.ForEach(x => x.DOAnchorPosX(-100, .3f).SetRelative().SetEase(Ease.OutSine));
            _zoneCountTextList.ForEach(x => x.GetComponent<RectTransform>().DOAnchorPosX(-100, .3f).SetRelative().SetEase(Ease.OutSine));
        });
        seq.AppendInterval(.4f);
        seq.AppendCallback(() =>
        {
            //ChangeMaskImage();
            //UIManager.Instance.SpinButton.Button.interactable = true;
            //WheelManager.instance.StartNewZone();

            WheelManager.instance.OnStartNewZone?.Invoke();
        });
    }

    private void SetMaskZoneImageColor(Image maskImage)
    {
        var zoneCount = WheelManager.instance.ZoneCount+1;

        if (zoneCount % 30 == 0)
            maskImage.color = _maskImageZoneColor[2];
        else if (zoneCount % 5 == 0)
            maskImage.color = _maskImageZoneColor[1];
        else
            maskImage.color = _maskImageZoneColor[0];
    }

    private void ChangeMaskImage()
    {
        _currentMaskImage.DOAnchorPosX(200,0).SetRelative();
     
        RectTransform tempImage;

        tempImage = _currentMaskImage;
        _currentMaskImage = _nextMaskImage;
        _nextMaskImage = tempImage;

    }
}
