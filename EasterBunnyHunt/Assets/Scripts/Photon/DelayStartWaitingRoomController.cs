using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonViews;
    [SerializeField] int multiplayerSceneINdex, menuSceneIndex, playerCount, roomSize, minPlayersToStart;
    [SerializeField] TMP_Text roomCountDisplay, timerToStartDisplay;

    bool readyToCountDown, readyToStart, startingGame;
    float timerToStartGame, notFullGameTimer, fullGameTimer;

    [SerializeField] float maxWaitTime, maxFullGameWaitTime;

    private void Start()
    {
        myPhotonViews = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = playerCount + ":" + roomSize;

        if(playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }
        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
        if(PhotonNetwork.IsMasterClient)
        {
            myPhotonViews.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if(timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();

    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if(playerCount <= 1)
        {
            ResetTimer();
        }
        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;

        if(timerToStartGame <= 0f)
        {
            if (startingGame)
                return;

                StartGame();
        }
    }

    void ResetTimer()
    {
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    public void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneINdex);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
