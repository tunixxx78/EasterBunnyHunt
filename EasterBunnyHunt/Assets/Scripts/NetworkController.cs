using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        // connect to photon masterServer
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected" + PhotonNetwork.CloudRegion + "server");
    }

    private void Update()
    {
        
    }
}
