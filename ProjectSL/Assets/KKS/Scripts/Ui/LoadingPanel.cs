using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    private Animator loadAni;

    private void Awake()
    {
        loadAni = GetComponent<Animator>();
    } // Awake

    private void OnEnable()
    {
        int num = Random.Range(0, DataManager.Instance.itemDatas.Count);
        ItemData itemData = new ItemData(DataManager.Instance.itemDatas[num]);
        itemIcon.sprite = Resources.Load<Sprite>(itemData.itemIcon);
        itemName.text = itemData.itemName;
        itemDescription.text = itemData.description;
    } // OnEnable

    private void OnDisable()
    {
        loadAni.SetBool("isFadeOut", false);
    } // OnDisable

    public float FadeInLoadingPanel()
    {
        return loadAni.GetCurrentAnimatorStateInfo(0).length;
    } // FadeInLoadingPanel
    public void FadeOutLoadingPanel()
    {
        loadAni.SetBool("isFadeOut", true);
    } // FadeOutLoadingPanel
} // LoadingPanel
