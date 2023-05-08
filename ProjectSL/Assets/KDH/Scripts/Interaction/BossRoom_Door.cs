using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom_Door : InteractionObject
{
    public List<Transform> doors = default;
    public List<Quaternion> doorsRotation = default;
    public float rotationSpeed;
    public bool isDoorOpened;

    public override void OnInteraction()
    {
        base.OnInteraction();
        base.StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        while (!isDoorOpened)
        {
            for (int i = 0; i < doors.Count; i++)
            {
                //Quaternion targetQuaternion = Quaternion.Euler(0f, doorsRotation[i], 0f);
                doors[i].rotation = Quaternion.Lerp(doors[i].rotation, doorsRotation[i], rotationSpeed * Time.deltaTime);

                if (doors[i].rotation == doorsRotation[i])
                {
                    isDoorOpened = true;
                }
            }

            yield return null;
        }
    }
}
