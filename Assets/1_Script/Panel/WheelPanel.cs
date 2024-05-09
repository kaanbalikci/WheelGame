using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WheelPanel : Panel
{
    [SerializeField] private WheelProperties[] _wheelProperties;

    //Wheel Visual Objects
    [Space(10)]
    [SerializeField] private SpriteAtlas _wheelSprites;
    [SerializeField] private Image _wheelSpinImage;
    [SerializeField] private Image _wheelIndicatorImage;
    [SerializeField] private TMP_Text _wheelNameText;
    [SerializeField] private Button _spinButton;


    private void OnEnable()
    {
        WheelManager.instance.OnSetWheel += (int i) =>
        {
            if (i % 30 == 0)
                SetWheelVisual(_wheelProperties[2]);
            else if (i % 5 == 0)
                SetWheelVisual(_wheelProperties[1]);
            else
                SetWheelVisual(_wheelProperties[0]);
        };
    }

    private void SetWheelVisual(WheelProperties wheelProperties)
    {
        _wheelSpinImage.sprite = _wheelSprites.GetSprite(wheelProperties.WheelSpinImagesName);
        _wheelIndicatorImage.sprite = _wheelSprites.GetSprite(wheelProperties.WheelIndicatorName);
        _wheelNameText.text = wheelProperties.WheelName;
        _wheelNameText.color = wheelProperties.WheelNameColor;
        _spinButton.gameObject.SetActive(true);

       
    }
}
