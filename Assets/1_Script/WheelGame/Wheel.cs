using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    private RectTransform _wheelRectTransform;
    private int _winingCount;

    
    [SerializeField] private int _slotCount;
    
    private void Awake()
    {
        _wheelRectTransform = GetComponent<RectTransform>();

    }

    void Start()
    {
        UIManager.Instance.SpinButton.Button.onClick.AddListener(()=>SpinWheel());
    }

    private void SpinWheel()
    {
        UIManager.Instance.ExitButton.interactable = false;

        var seq = DOTween.Sequence();

        _winingCount = Random.Range(0, _slotCount);

        Vector3 winingRotate = new Vector3(0,0,(-360/_slotCount) * _winingCount);
        seq.AppendInterval(.2f);
        seq.Append(_wheelRectTransform.DOLocalRotate(((new Vector3(0, 0, -360) * 5) + winingRotate + new Vector3(0,0,90)), 5, RotateMode.FastBeyond360).SetEase(Ease.OutQuint));
        seq.AppendCallback(() =>
        {
            DOVirtual.DelayedCall(.5f, () =>
            {
                ItemProperties temp = GetComponent<WheelSlotSetting>().Slots[_winingCount].GetComponent<WheelSlot>().GetItemProp();
                var x = GetComponent<WheelSlotSetting>().Slots[_winingCount].GetComponent<WheelSlot>().GetReward();
                WheelManager.instance.OnCardAnimStart?.Invoke(temp,x);
            },false);
        });
    }

    
         
}
