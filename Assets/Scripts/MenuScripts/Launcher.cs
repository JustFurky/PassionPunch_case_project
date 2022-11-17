using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using Random = UnityEngine.Random;
using System.IO;
using System.Threading.Tasks;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [Header("Text References")]
    [SerializeField]
    private TMP_InputField RoomNamePlayerInput;

    [SerializeField] private TMP_Text _roomName;

    [Header("Menu Panels")]
    [SerializeField]
    private GameObject MainMenuPanel;

    [SerializeField] private GameObject LobbyPanel;
    [SerializeField] private GameObject CreateGamePanel;
    [SerializeField] private GameObject RoomListPanel;

    [Header("Group Objects And Prefabs")]
    [SerializeField]
    private Transform RoomListGroupObject;
    [SerializeField] private GameObject RoomListButtonItemPrefab;
    [SerializeField] private Transform PlayerListGroupObjects;
    [SerializeField] private GameObject PlayerListTextItemPrefab;

    [SerializeField] private GameObject StartButtonObject;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(RoomNamePlayerInput.text))
        {
            return;
        }

        PhotonNetwork.CreateRoom(RoomNamePlayerInput.text);
        LobbyPanelActivate();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnJoinedRoom()
    {
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform playersOnLobby in PlayerListGroupObjects)
        {
            Destroy(playersOnLobby.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListTextItemPrefab, PlayerListGroupObjects).GetComponent<LobbyPlayerListTextItem>()
                .SetUp(players[i]);
        }
        LobbyPanelActivate();
        StartButtonObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartButtonObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateGamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in RoomListGroupObject)
        {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(RoomListButtonItemPrefab, RoomListGroupObject).GetComponent<RoomListButtonItem>()
                .SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListTextItemPrefab, PlayerListGroupObjects).GetComponent<LobbyPlayerListTextItem>()
            .SetUp(newPlayer);
    }
    //public bool init = false;
    //private async Task InstantýateCharacters() {
    //
    //
    //    if (init == false)
    //    {
    //        await Task.Delay(3000);
    //        init = true;
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            foreach (var player in PhotonNetwork.PlayerList)
    //            {
    //                Debug.Log(player);
    //                GameObject playerInstantiate = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
    //                DontDestroyOnLoad(playerInstantiate);
    //                playerInstantiate.GetComponent<PhotonView>().TransferOwnership(player);
    //            }
    //        } 
    //    }
    //}
    public void StartGame()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        PhotonNetwork.LoadLevel(1);
        if (PhotonNetwork.IsMasterClient)
        {
            PlayerPrefs.SetInt("MasterClient", 5000);
        }
        //_ = InstantýateCharacters(); 
        // }
    }

    public void CreateGamePanelActivateButton()
    {
        CreateGamePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        RoomListPanel.SetActive(false);
    }

    public void FindAGamePanelActivateButton()
    {
        CreateGamePanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        RoomListPanel.SetActive(true);
    }

    public void LobbyPanelActivate()
    {
        MainMenuPanel.SetActive(false);
        RoomListPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        CreateGamePanel.SetActive(false);
    }

    public void BackButtonOnGameRoomList()
    {
        MainMenuPanel.SetActive(true);
        RoomListPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        CreateGamePanel.SetActive(false);
    }

    public void LeaveRoomButton()
    {
        PhotonNetwork.LeaveRoom();
        MainMenuPanel.SetActive(false);
        RoomListPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        CreateGamePanel.SetActive(false);
    }
}