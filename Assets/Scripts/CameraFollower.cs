using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    int cameraOffset = -10;


   

    private void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (0,0,cameraOffset) ;
    }
}


