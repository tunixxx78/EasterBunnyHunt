using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


// This script will be added to any multiplayer scene

public class GameSetupController : MonoBehaviour
{
    private void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating player");
        var x = Random.Range(0, 3);
        PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "photonPlayer"), new Vector3(x, 0, 0), Quaternion.identity);
    }
}
