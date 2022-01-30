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
        if (pV.IsMine)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                BasicJumping();
            }
            BasicMovement();


            BasicRotation();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z;

        
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
        }

        

        velocity.y += gravityValue * 2 * Time.deltaTime;
        myCC.Move(velocity * Time.deltaTime);
    }
    /*
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
    */
    void BasicMovement()
    {

        myCC.Move(movement * movementSpeed * Time.deltaTime);
        /*
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
       
        */

    }

    void BasicJumping()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravityValue);

        Debug.Log("NYT HYPITÄÄN!");
    }

    void BasicRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }
}
