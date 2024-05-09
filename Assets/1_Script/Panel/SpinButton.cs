using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButton : Panel
{
    [HideInInspector] public Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Button.interactable = true;

        WheelManager.instance.OnStartNewZone += SetButtonActive;
    }

    private void Start()
    {
        Button.onClick.AddListener(() =>
        {
            Button.interactable = false;
        });
    }

    private void SetButtonActive()
    {
        Button.interactable = true;
    }
}
