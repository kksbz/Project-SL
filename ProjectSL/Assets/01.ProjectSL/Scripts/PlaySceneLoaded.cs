using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneLoaded : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BgmManager.Instance.EnvironMentPlay();
    }

}
