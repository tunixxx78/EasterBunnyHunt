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

    public bool isCarryingEgg = false;

    [SerializeField] GameObject playerAvatar, secondCamera;

    public GameObject eggOnGo;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        myCamera = GetComponentInChildren<Camera>();
        /*
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
            secondCamera.SetActive(false);
        }
        */
    }

    private void Update()
    {
        
        if (!pV.IsMine)
        {
            
            secondCamera.SetActive(false);
            myCamera.enabled = false;
        }
        
        if (pV.IsMine)
        {
            
            //secondCamera.SetActive(true);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                BasicJumping();
            }
            if (Input.GetKey(KeyCode.W))
            {
                //transform.rotation = Quaternion.LookRotation(myCamera.transform.forward, myCamera.transform.up * Time.deltaTime);
                BasicMovement();

            }
            if (Input.GetKey(KeyCode.A))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, -90, 0);
                BasicMovement();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 90, 0);
                BasicMovement();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 180, 0);
                BasicMovement();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            


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

    private void OnTriggerEnter(Collider collider)
    {
 
        if(collider.gameObject.tag == "Egg")
            {
            eggOnGo.SetActive(true);
            isCarryingEgg = true;
            Debug.Log("Muna Löydetty ja kerätty");
            }

    }
}
