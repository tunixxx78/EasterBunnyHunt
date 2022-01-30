using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView pV;
    public GameObject myCharacter;
    public int characterValue;

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        if (pV.IsMine)
        {
            pV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.pI.mySellectedCharacter);
        }

    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.pI.allCharacters[whichCharacter], transform.position, transform.rotation, transform);

    }
}
