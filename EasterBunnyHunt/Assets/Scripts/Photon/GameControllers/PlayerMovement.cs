using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView pV;
    private CharacterController myCC;
    public float movementSpeed, jumpForce;
    public float rotationSpeed;
    [SerializeField] Camera myCamera;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();

    }

    private void Update()
    {
        if (pV.IsMine)
        {
            BasicMovement();
            BasicRotation();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
        }
    }

    void BasicMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            myCC.Move(transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myCC.Move(-transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myCC.Move(-transform.forward * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            myCC.Move(transform.right * Time.deltaTime * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            myCC.Move(transform.up * Time.deltaTime * jumpForce);
        }
        if (Input.GetKey(KeyCode.Return))
        {
            myCC.Move(-transform.up * Time.deltaTime * jumpForce);
        }
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
