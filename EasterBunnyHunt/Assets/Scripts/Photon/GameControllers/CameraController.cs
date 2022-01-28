using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    PhotonView pV;
    Camera myCamera;
    public Transform cameraTarget;
    [SerializeField] float smoothSpeed = .125f;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        myCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
        }
    }

    private void LateUpdate()
    {
        /*
        Vector3 desiredPosition = cameraTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(cameraTarget);
        */
    }
}
