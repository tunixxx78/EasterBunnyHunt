using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CollectibleEgg : MonoBehaviour
{
    [SerializeField] PhotonView pV;
    

    private void Start()
    {
        pV = GetComponent<PhotonView>();

    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && collider.GetComponent<PlayerMovement>().isCarryingEgg == false)
        {
            Destroy(this.gameObject);
        }
    }
}
