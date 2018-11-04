using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    UnityEngine.PostProcessing.PostProcessingBehaviour postProcessing;

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

    private void Start()
    {
        postProcessing.enabled = false;
    }

    void Update()
    {
        if (followTarget != null && shouldFollow)
            FollowTarget(Time.deltaTime);

        if(GameSceneManager.ActivePlayer.PlayerStats.State == PlayerStats.PlayerState.Dead)
        {
            if (postProcessing.enabled != true)
                postProcessing.enabled = true;
        }

        else
        {
            if (postProcessing.enabled != false)
                postProcessing.enabled = false;
        }
        
    }

    void FollowTarget(float deltaTime)
    {
        Vector3 newPosition = Vector2.MoveTowards(transform.position, followTarget.position, deltaTime * followSpeed);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
