using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    float followSpeed;

    [SerializeField]
    Camera followCamera;

    [SerializeField]
    Transform followTarget = null;
    bool shouldFollow = true;

    public bool Follow
    {
        get { return shouldFollow; }
        set { shouldFollow = true; }
    }

    public Transform Target
    {
        get { return followTarget; }
        set { followTarget = value; }
    }

    void Update()
    {
        if (followTarget != null && shouldFollow)
            FollowTarget(Time.deltaTime);
    }

    void FollowTarget(float deltaTime)
    {
        Vector3 newPosition = Vector2.MoveTowards(transform.position, followTarget.position, deltaTime * followSpeed);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
