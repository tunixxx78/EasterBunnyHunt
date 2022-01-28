using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView pV;
    //Rigidbody myCC;
    private CharacterController myCC;
    public float movementSpeed, jumpForce, groundDistance;
    public float rotationSpeed;
    [SerializeField] Camera myCamera;

    public LayerMask groundMask;
    public Transform groundCheck;
    [SerializeField] bool isGrounded;
    Vector3 movement, velocity;
    float gravityValue = -9.81f;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();

    }

    private void Update()
    {
        isGrounded = myCC.isGrounded;
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
        }

        

        velocity.y += gravityValue * Time.deltaTime;
        myCC.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (pV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BasicJumping();
            }
        BasicMovement(movement);
            
            
        BasicRotation();
        }
    }

    void BasicMovement(Vector3 direction)
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
       
        

    }

    void BasicJumping()
    {
        myCC.Move(transform.up * Time.deltaTime * jumpForce * -gravityValue);

        Debug.Log("NYT HYPITÄÄN!");
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
