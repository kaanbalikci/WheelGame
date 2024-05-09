using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGlowAnim : MonoBehaviour
{
    private void Start()
    {
        GetComponent<RectTransform>().DOLocalRotate(new Vector3(0, 0, 360), 3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart);
        transform.DOScale(-.15f, .75f).SetRelative().SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
