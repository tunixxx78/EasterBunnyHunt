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
            if(pV.ViewID == 1001)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "playerAvatar"), GameSetup.gS.spawnPoints[0].position, GameSetup.gS.spawnPoints[spawnPicker].rotation, 0);
            }
            if(pV.ViewID == 2001)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "playerAvatar"), GameSetup.gS.spawnPoints[1].position, GameSetup.gS.spawnPoints[spawnPicker].rotation, 0);
            }
            if (pV.ViewID == 3001)
            {
                myAvatar = PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "playerAvatar"), GameSetup.gS.spawnPoints[2].position, GameSetup.gS.spawnPoints[spawnPicker].rotation, 0);
            }

        }
    }
}
