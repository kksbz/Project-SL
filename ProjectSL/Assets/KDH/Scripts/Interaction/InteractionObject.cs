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
            isEnterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == GData.PLAYER_MARK)
        {
            isEnterPlayer = false;
            OnInteraction();
        }
    }

    private void Update()
    {
        if (isEnterPlayer && Input.GetKeyDown(KeyCode.E))
        {
            OnInteraction();
        }
    }

    public virtual void OnInteraction()
    {
        isInteraction = true;
    }
}
