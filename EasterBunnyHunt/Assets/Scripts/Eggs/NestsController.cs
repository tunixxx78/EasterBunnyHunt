using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NestsController : MonoBehaviour
{
    [SerializeField] PhotonView pV;
    [SerializeField] GameObject eggOne, eggTwo, eggThree, eggFour, eggFive, chickOne, chickTwo, chickThree, chickFour, chickFive;
    [SerializeField] TMP_Text hatchingTime;
    [SerializeField] float currentTime, startTime;
    public bool isHatching = false;
    public GameObject hatchingTimer, nestEgg;
    [SerializeField] int playerPhotonViewIdNro;
    [SerializeField] int currentPlayer;
    

    //public List<GameObject> eggsInNest;
    //public List<Transform> NestSpawnPoints;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        currentTime = startTime;
    }

    private void Update()
    {
        //if (pV.IsMine)
        {
            if (hatchingTimer.activeSelf == true)
            {
                currentTime -= 1 * Time.deltaTime;
                string tempTimer = string.Format("{0:00}", currentTime);
                hatchingTime.text = tempTimer;

                Debug.Log(currentTime);
            }


            if (currentTime <= 0.2)
            {
                

                if (eggOne.activeSelf == true)
                {
                    eggOne.GetComponent<MeshRenderer>().enabled = false;
                    chickOne.SetActive(true);
                    hatchingTimer.SetActive(false);
                    currentTime = startTime;
                }
                if (eggOne.activeSelf == true && eggTwo.activeSelf == true)
                {
                    eggTwo.GetComponent<MeshRenderer>().enabled = false;
                    chickTwo.SetActive(true);
                    hatchingTimer.SetActive(false);
                    currentTime = startTime;
                }
                if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == true)
                {
                    eggThree.GetComponent<MeshRenderer>().enabled = false;
                    chickThree.SetActive(true);
                    hatchingTimer.SetActive(false);
                    currentTime = startTime;
                }
                if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == true && eggFour.activeSelf == true)
                {
                    eggFour.GetComponent<MeshRenderer>().enabled = false;
                    chickFour.SetActive(true);
                    hatchingTimer.SetActive(false);
                    currentTime = startTime;
                }
                if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == true && eggFour.activeSelf == true && eggFive.activeSelf == true)
                {
                    eggFive.GetComponent<MeshRenderer>().enabled = false;
                    chickFive.SetActive(true);
                    hatchingTimer.SetActive(false);
                    currentTime = startTime;
                }
            }

            if (Input.GetKey(KeyCode.P))
            {
                hatchingTimer.SetActive(true);
            }
        }
        /*if(hatchingTimer.activeSelf == true)
        {
            currentTime -= 1 * Time.deltaTime;
            string tempTimer = string.Format("{0:00}", currentTime);
            hatchingTime.text = tempTimer;

            Debug.Log(currentTime);
        }
        

        if(currentTime <= 0.2)
        {
            FindObjectOfType<PlayerMovement>().MyHatchedEggs();

            if(eggOne.activeSelf == true)
            {
                eggOne.GetComponent<MeshRenderer>().enabled = false;
                chickOne.SetActive(true);
                hatchingTimer.SetActive(false);
                currentTime = startTime;
            }
            if (eggOne.activeSelf == true && eggTwo.activeSelf == true)
            {
                Debug.Log("TOINEN MUNA");
                eggTwo.GetComponent<MeshRenderer>().enabled = false;
                chickTwo.SetActive(true);
                hatchingTimer.SetActive(false);
                currentTime = startTime;
            }
            
        }

        if (Input.GetKey(KeyCode.P))
        {
            hatchingTimer.SetActive(true);
        }
        */
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<PhotonView>().ViewID == playerPhotonViewIdNro)
        {
            //currentPlayer = collider.GetComponent<PhotonView>().ViewID;

            if (collider.gameObject.tag == "Player" && collider.GetComponent<PlayerMovement>().isCarryingEgg == true)
            {

                if (eggOne.activeSelf == false)
                {
                    eggOne.SetActive(true);
                    hatchingTimer.SetActive(true);
                    Debug.Log(collider.gameObject + "Osuma");
                    collider.GetComponent<PlayerMovement>().eggOnGo.SetActive(false);
                    collider.GetComponent<PlayerMovement>().isCarryingEgg = false;
                }
                else if (eggOne.activeSelf == true && eggTwo.activeSelf == false)
                {
                    eggTwo.SetActive(true);
                    hatchingTimer.SetActive(true);
                    Debug.Log(collider.gameObject + "Osuma");
                    collider.GetComponent<PlayerMovement>().eggOnGo.SetActive(false);
                    collider.GetComponent<PlayerMovement>().isCarryingEgg = false;
                }
                else if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == false)
                {
                    eggThree.SetActive(true);
                    hatchingTimer.SetActive(true);
                    collider.GetComponent<PlayerMovement>().eggOnGo.SetActive(false);
                    collider.GetComponent<PlayerMovement>().isCarryingEgg = false;
                }
                else if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == true && eggFour.activeSelf == false)
                {
                    eggFour.SetActive(true);
                    hatchingTimer.SetActive(true);
                    collider.GetComponent<PlayerMovement>().eggOnGo.SetActive(false);
                    collider.GetComponent<PlayerMovement>().isCarryingEgg = false;
                }
                else if (eggOne.activeSelf == true && eggTwo.activeSelf == true && eggThree.activeSelf == true && eggFour.activeSelf == true && eggFive.activeSelf == false)
                {
                    eggFive.SetActive(true);
                    hatchingTimer.SetActive(true);
                    collider.GetComponent<PlayerMovement>().eggOnGo.SetActive(false);
                    collider.GetComponent<PlayerMovement>().isCarryingEgg = false;
                }


            }
        }
        
    }


}
