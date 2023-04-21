using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 정보")]
    [SerializeField] private int itemID; // 아이템 데이터 맵핑에 사용될 ID
    public GameObject pickupArea; // 아이템 획득가능 영역오브젝트
    public ItemData itemData; // 아이템 데이터

    void Awake()
    {
        foreach (string[] _itemData in DataManager.Instance.itemDatas)
        {
            // 아이템데이터 테이블에서 자기 ID의 아이템데이터를 가져옴
            int id = 0;
            int.TryParse(_itemData[0], out id);
            //Debug.Log($"{_itemData[0]} = {id}의 데이터타입 : {id.GetType()}");
            if (id == itemID)
            {
                itemData = new ItemData(_itemData);
                break;
            }
        }
    } // Start

    // 아이템 삭제 시 아이템 데이터도 삭제
    void OnDestroy()
    {
        itemData = null;
    } // OnDestroy
} // Item
