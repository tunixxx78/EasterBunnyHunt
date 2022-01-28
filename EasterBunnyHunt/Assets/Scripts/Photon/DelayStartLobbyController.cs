using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject delayStartButton, delayCancelButton;
    [SerializeField] int roomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);

    }

    public void QuickStart()
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("DelaydStart");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room!");

        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating room now!");

        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room ... try again");
        CreateRoom();
    }

    public void QuickCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
