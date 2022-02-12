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

    //public List<GameObject> eggsInNest;
    //public List<Transform> NestSpawnPoints;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        currentTime = startTime;
    }

    private void Update()
    {
        if(hatchingTimer.activeSelf == true)
        {
            currentTime -= 1 * Time.deltaTime;
            string tempTimer = string.Format("{0:00}", currentTime);
            hatchingTime.text = tempTimer;

            Debug.Log(currentTime);
        }
        

        if(currentTime <= 0.2)
        {
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
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && collider.GetComponent<PlayerMovement>().isCarryingEgg == true)
        {

            if(eggOne.activeSelf == false)
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
            
            
        }
    }
}
