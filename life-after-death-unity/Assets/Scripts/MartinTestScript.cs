using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinTestScript : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    FollowCamera followCam;

    void Start()
    {
        followCam.Target = target;
    }
}
