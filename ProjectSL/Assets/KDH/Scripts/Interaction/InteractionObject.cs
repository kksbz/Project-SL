using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public bool isInteraction = default;
    public bool isEnterPlayer = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionText.text = "���� ���� : E Ű";
            UiManager.Instance.interactionBar.SetActive(true);
            isEnterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
            isEnterPlayer = false;
            OnInteraction();
        }
    }

    private void Update()
    {
        if (isEnterPlayer && Input.GetKeyDown(KeyCode.E))
        {
            OnInteraction();
            UiManager.Instance.interactionBar.SetActive(false);
            UiManager.Instance.interactionText.text = null;
        }
    }

    public virtual void OnInteraction()
    {
        isInteraction = true;
    }
}
