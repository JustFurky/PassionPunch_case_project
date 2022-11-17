using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ScoreBoardManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _scoreBoardMain;
    [SerializeField] private GameObject _scoreBoardObject;

    Dictionary<Player, ScoreBoardUI> _scoreBoardItems = new Dictionary<Player, ScoreBoardUI>();
    void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreBoardItemForPlayer(player);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreBoardItemForPlayer(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreBoardItem(otherPlayer);
    }
    private void AddScoreBoardItemForPlayer(Player player)
    {
        ScoreBoardUI scoreBoardUIItem = Instantiate(_scoreBoardObject, _scoreBoardMain).GetComponent<ScoreBoardUI>();
        scoreBoardUIItem.Initialize(player);
        _scoreBoardItems[player] = scoreBoardUIItem;
    }
    private void RemoveScoreBoardItem(Player player)
    {
        Destroy(_scoreBoardItems[player].gameObject);
        _scoreBoardItems.Remove(player);
    }
}
