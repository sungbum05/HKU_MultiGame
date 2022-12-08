using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

public class CameraManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            CinemachineVirtualCamera FollowCam = 
                FindObjectOfType<CinemachineVirtualCamera>();

            FollowCam.Follow = transform;
            FollowCam.LookAt = transform;
        }
    }
}
