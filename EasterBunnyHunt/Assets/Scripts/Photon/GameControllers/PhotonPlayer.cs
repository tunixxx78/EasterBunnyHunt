using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView pV;
    public GameObject myAvatar;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.gS.spawnPoints.Length);
        if(pV.IsMine)
        {
           myAvatar = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "playerAvatar"), GameSetup.gS.spawnPoints[spawnPicker].position, GameSetup.gS.spawnPoints[spawnPicker].rotation, 0);
        }
    }
}
