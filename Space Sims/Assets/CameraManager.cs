using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private GameObject FollowTarget;

    public void CameraFocus(GameObject focusTarget)
    {
        if(focusTarget != FollowTarget)
        {
            ClearFollow();
        }
        transform.position = new Vector3(focusTarget.transform.position.x,focusTarget.transform.position.y,transform.position.z);
    }

    public void CameraFollow(GameObject followtarget)
    {
        FollowTarget = followtarget;
    }

    public void ClearFollow()
    {
        FollowTarget = null;
    }

    private void LateUpdate()
    {
        if(FollowTarget != null)
        {
            CameraFocus(FollowTarget);
        }
    }
}
