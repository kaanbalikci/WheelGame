using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(menuName = "Wheel/Wheel Properties")]
public class WheelProperties : ScriptableObject
{
    public enum WheelType { Bronze, Silver, Golden}
    public WheelType wheelType;

    [Space(10)]
    [Header("Name")]
    public string WheelName;
    public Color WheelNameColor;

    [Space(10)]
    [Header("Wheel Image Names")]
    public string WheelIndicatorName;
    public string WheelSpinImagesName;
}
