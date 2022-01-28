using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject battleButton, cancelButton;
    


    private void Awake()
    {
        lobby = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to photon master server!");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join but failed, there must not be oppen games available");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating new room!");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.multiplayerSetting.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create new room but failed, there must already be a room with same name!");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
