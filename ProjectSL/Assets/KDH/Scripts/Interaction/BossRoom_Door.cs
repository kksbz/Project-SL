using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom_Door : InteractionObject
{
    public List<Transform> doors = default;
    public List<Quaternion> doorsOpenRotation = default;
    public List<Quaternion> doorsCloseRotation = default;
    public float rotationSpeed;
    public bool isDoorOpened;

    public override void OnInteraction()
    {
        base.OnInteraction();
        if (isDoorOpened)
        {
            base.StartCoroutine(CloseDoor());
        }
        else
        {
            base.StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        while (!isDoorOpened)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                //Quaternion targetQuaternion = Quaternion.Euler(0f, doorsRotation[i], 0f);
                doors[i].rotation = Quaternion.Lerp(doors[i].rotation, doorsOpenRotation[i], rotationSpeed * Time.deltaTime);

                if (doors[i].rotation == doorsOpenRotation[i])
                {
                    isDoorOpened = true;
                }
            }

            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        while (isDoorOpened)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].rotation = Quaternion.Lerp(doors[i].rotation, doorsCloseRotation[i], rotationSpeed * Time.deltaTime);
                if (doors[i].rotation == doorsCloseRotation[i])
                {
                    isDoorOpened = false;
                }
            }
            yield return null;
        }
    }
}
