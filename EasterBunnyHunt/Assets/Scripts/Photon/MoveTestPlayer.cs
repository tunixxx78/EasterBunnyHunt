using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MoveTestPlayer : MonoBehaviour
{
    Rigidbody player;
    PhotonView myview;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();
        myview = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.AddForce(transform.right, ForceMode.Impulse);
        }
        
    }
}
