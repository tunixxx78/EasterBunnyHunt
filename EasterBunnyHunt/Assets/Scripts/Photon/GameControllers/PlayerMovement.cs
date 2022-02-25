using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView pV;
    //Rigidbody myCC;

    [SerializeField] Animator playerAnimator;
    private CharacterController myCC;
    public float movementSpeed, jumpForce, groundDistance, ammoSpeed, coolTimerTime;
    public float rotationSpeed;
    [SerializeField] Camera myCamera;

    public LayerMask groundMask;
    public Transform groundCheck, ammoSpawnPoint;
    [SerializeField] bool isGrounded;
    Vector3 movement, velocity;
    float gravityValue = -9.81f, originalMovementSpeed;

    public bool isCarryingEgg = false;

    [SerializeField] GameObject playerAvatar, secondCamera, finalResultText;

    public GameObject eggOnGo;

    [SerializeField] TMP_Text resultText;

    public int eggsHatched = 0;

    [SerializeField] List<GameObject> nests;

    Vector3 dir;

    private void Awake()
    {
        GameObject nest = GameObject.Find("Basket");
        nests.Add(nest);
        GameObject nest2 = GameObject.Find("Basket (1)");
        nests.Add(nest2);
        GameObject nest3 = GameObject.Find("Basket (2)");
        nests.Add(nest3);
        GameObject nest4 = GameObject.Find("Basket (3)");
        nests.Add(nest4);
    }

    private void Start()
    {
        StartCoroutine(GetAnimator(2));
        pV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        myCamera = GetComponentInChildren<Camera>();
        playerAnimator = GetComponentInChildren<Animator>();
        originalMovementSpeed = movementSpeed;
        
        if (!pV.IsMine)
        {
            myCamera.enabled = false;
            secondCamera.SetActive(false);
        }
        
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
            
            secondCamera.SetActive(true);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                BasicJumping();
            }
            if (Input.GetKey(KeyCode.W))
            {
                //transform.rotation = Quaternion.LookRotation(myCamera.transform.forward, myCamera.transform.up * Time.deltaTime);
                dir = myCamera.transform.forward;
                dir.y = 0;
                dir.Normalize();
                transform.rotation = Quaternion.LookRotation(dir, transform.up * Time.deltaTime);
                playerAnimator.SetBool("Run", true);
                BasicMovement();

            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 0, 0);
                playerAnimator.SetBool("Run", false);
            }
                if (Input.GetKey(KeyCode.A))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, -90, 0);
                playerAnimator.SetBool("Run", true);
                BasicMovement();
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 90, 0);
                playerAnimator.SetBool("Run", true);
                BasicMovement();
            }

            if (Input.GetKey(KeyCode.S))
            {
                playerAvatar.transform.localRotation = Quaternion.Euler(0, 180, 0);
                playerAnimator.SetBool("Run", true);
                BasicMovement();
            }
         
            
            if(eggsHatched >= 5)
            {
                
                for (int i = 0; i < nests.Count; i++)
                {
                    nests[i].GetComponent<NestsController>().gameIsOver = true;
                }
                
                resultText.text = "YOU WIN THIS ROUND!";
                finalResultText.SetActive(true);
            }

            if (eggsHatched < 5 && WhoHasLosed.gameHasEnded == true/*FindObjectOfType<NestsController>().gameIsOver == true*/)
            {
                resultText.text = "YOU LOSE THIS ROUND!";
                finalResultText.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                BasicShoot();
                playerAnimator.SetTrigger("Shoot");
            }

            

            //BasicRotation();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 z2 = z * myCamera.transform.forward;
        Vector3 x2 = x * myCamera.transform.right;

        movement = (z2 + x2).normalized;

        //movement = transform.right * x + transform.forward * z;
        

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
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            myCC.Move(movement * (movementSpeed / 2) * Time.deltaTime);
        }
        else

        {
            myCC.Move(movement * movementSpeed * Time.deltaTime);
        }

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
            FindObjectOfType<EggsSpawner>().SpawnEggs();
            Destroy(collider.gameObject);

            }
        if(collider.gameObject.tag == "playerAmmo")
        {

            movementSpeed = 1;
            StartCoroutine(ReturnOriginalSpeed(coolTimerTime));
            Debug.Log("OSUMAAA TULEEEEEEEEE");
        }

    }
    private void BasicShoot()
    {
        Vector3 bulletDir = myCamera.transform.forward;
        
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "PlayerAmmo"), ammoSpawnPoint.position, transform.rotation, 0);

        
    }

   IEnumerator GetAnimator(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        playerAnimator = GetComponentInChildren<Animator>();
    }
    IEnumerator ReturnOriginalSpeed(float slowMotionTime)
    {
        yield return new WaitForSeconds(slowMotionTime);

        movementSpeed = originalMovementSpeed;
    }
}
