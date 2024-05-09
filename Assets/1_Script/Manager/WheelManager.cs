using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelManager : MonoBehaviour
{
    public static WheelManager instance;

    [SerializeField]private int _zoneCount;

    public int ZoneCount 
    {
        get
        {
            return _zoneCount;
        }
        set
        {
            _zoneCount = value;
            OnSetWheel?.Invoke(ZoneCount);
        } 
    }

    public Action<int> OnSetWheel,OnTakeReward;
    public Action OnLevelBarUpdate, OnStartNewZone;
    public Action<ItemProperties,int> OnCardAnimStart;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }

    private void OnEnable()
    {
        OnStartNewZone += StartNewZone;
    }
    private void Start()
    {
        ZoneCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            ZoneCount++;
            Debug.Log(ZoneCount);
        }
    }

    public void StartNewZone()
    {
        ZoneCount++;
    }
}
