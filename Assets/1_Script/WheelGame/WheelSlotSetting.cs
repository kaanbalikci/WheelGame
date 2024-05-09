using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class WheelSlotSetting : MonoBehaviour
{
    //Slot Set Position Variables
    private float _radius = 205;
    private float _angleBetweenGaps = 45;
    private Vector3 _pivot = Vector3.zero;
    private int _numberOfGaps = 8;
    private int _startZRotation = -90;
    //////

    public SpriteAtlas Atlas;

    [HideInInspector] public List<RectTransform> Slots = new();

    //Item Variables
    [SerializeField] private List<ItemProperties> _itemProperties = new(); //Item Database
    [SerializeField] private ItemProperties _bombItem;

    private List<ItemProperties> _tempItemList = new();


    private void Awake()
    {
        Slots = GetComponentsInChildren<RectTransform>().ToList();
        Slots = Slots.Where(x => x.GetComponent<WheelSlot>()).ToList();
    }
    private void OnEnable()
    {
        WheelManager.instance.OnSetWheel += SetSlotProperties;
    }
    private void OnDisable()
    {
        WheelManager.instance.OnSetWheel -= SetSlotProperties;
    }
    private void Start()
    {
        SetSlotInWheel();
    }

    private void SetSlotInWheel()
    {
        for (int i = 0; i < _numberOfGaps; i++)
        {
            float angle = i * _angleBetweenGaps * Mathf.Deg2Rad;

            float x = _pivot.x + _radius * Mathf.Cos(angle);
            float y = _pivot.y + _radius * Mathf.Sin(angle);

            Slots[i].anchoredPosition = new Vector3(x, y, 0);
            Slots[i].eulerAngles = new Vector3(0, 0, (i * 45) + _startZRotation);
        }
    }
 
    public void SetSlotProperties(int _zoneCount) 
    {
        
        var zoneCount = _zoneCount;
        _tempItemList.Clear();

        if(zoneCount % 30 == 0) 
        {
            CheckItemsInList(false,true);
        }
        else if(zoneCount % 5 == 0)
        {

            CheckItemsInList();
        }
        else
        {

            CheckItemsInList(true);

        }
    }

    private void CheckItemsInList(bool isBomb = false ,bool isGolden = false)
    {
        var needCount = isBomb ? SetBomb() : 8;

        var shuffledList = _itemProperties.OrderBy(x => UnityEngine.Random.value).ToList();


        if (needCount == 8) 
        {
            shuffledList = isGolden ? shuffledList.Where(x => x.ItemType== ItemType.Tier3)
                .Take(needCount).ToList() : shuffledList.Take(needCount).ToList();
        }
        else
        {
            shuffledList = shuffledList.Take(needCount).ToList();

        }

        shuffledList = shuffledList.OrderBy(x => UnityEngine.Random.value).ToList();

        foreach (var item in shuffledList) 
        {
            _tempItemList.Add(item);
        }

        SetPropToSlot(_tempItemList);
    }

    private int SetBomb()
    {
        _tempItemList.Add(_bombItem);
        return 7;
    }

    private void SetPropToSlot(List<ItemProperties> lastList)
    {
        for (int i = 0; i < lastList.Count; i++)
        {
            Slots[i].GetComponent<WheelSlot>().SetItemProperties(lastList[i]);
        }
    }
}
