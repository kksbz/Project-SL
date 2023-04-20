using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UiManager.Instance.optionBar.SetActive(!UiManager.Instance.optionBar.activeSelf);
        }
    }
}
