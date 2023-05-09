using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLogo : MonoBehaviour
{
    [SerializeField] private GameObject pressAnyKey;

    private void Start()
    {
        BgmManager.Instance.TitleBgmPlay();
    }

    public void ShowPressObj()
    {
        pressAnyKey.SetActive(true);

    }
} // TitleLogo
