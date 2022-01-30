using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    private PhotonView pV;

    public GameObject battleButton, cancelButton, panel, selectCharacterText, startGameText, timerText;

    [SerializeField] Animator canvasAnimator;
    [SerializeField] float waitTime, currentTime;
    public TMP_Text TimerText, enteringGameText, playerAmountText;
    public bool canStartTimer = false;

    private void Awake()
    {
        lobby = this;
    }

    private void Start()
    {
        pV = GetComponent<PhotonView>();
        PhotonNetwork.ConnectUsingSettings();
        TimerText.text = waitTime.ToString();
        waitTime = PhotonRoom.room.startingTime;
        currentTime = waitTime;
    }

    private void Update()
    {

        playerAmountText.text = PhotonRoom.room.playersInRoom + " : " + MultiplayerSetting.multiplayerSetting.maxPlayers;

        if (canStartTimer)
        {
            

            currentTime -= 1 * Time.deltaTime;
            

            if(currentTime <= 0)
            {
                TimerText.text = "0";
            }
        }

        string tempTimer = string.Format("{0:00}", currentTime);
        TimerText.text = tempTimer;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to photon master server!");
        PhotonNetwork.AutomaticallySyncScene = true;
        //battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        //StartCoroutine(NowReallyStart());
        PhotonNetwork.JoinRandomRoom();
        canvasAnimator.SetBool("UiPanelMove", true);
        //canStartTimer = true;
    }

    IEnumerator NowReallyStart()
    {
        yield return new WaitForSeconds(waitTime);

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
        canvasAnimator.SetBool("UiPanelMove", false);
    }

    public void ReadyToStartGame()
    {
        battleButton.SetActive(true);
        selectCharacterText.SetActive(false);
        startGameText.SetActive(true);
        
    }
}
