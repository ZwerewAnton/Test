using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform playerTransform = null;
    private Vector3 cameraOffset, newPos;
    GameObject[] player = null;
    public float smoothFactor = 0.5f;
    private float MIN_X = -7.5f, MAX_X = 7.5f, MIN_Y = 9.0f, MAX_Y=10f, MIN_Z=-7.5f, MAX_Z=20.5f;

    void LateUpdate()
    {
        if (playerTransform == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player");
            if(player.Length==0)
            {
                playerTransform = null;
            }
            else
            {
                playerTransform = player[0].transform;
                cameraOffset = transform.position - playerTransform.position;
            }
        }
        else
        {
            newPos = playerTransform.position + cameraOffset;
            newPos = new Vector3(
                Mathf.Clamp(newPos.x, MIN_X, MAX_X),
                Mathf.Clamp(newPos.y, MIN_Y, MAX_Y),
                Mathf.Clamp(newPos.z, MIN_Z, MAX_Z));
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        }

    }

}
