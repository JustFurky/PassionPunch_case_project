using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class LobbyPlayerListTextItem : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _playerNickName;
    private Player _player;
    public void SetUp(Player player)
    {
        _player = player;
        _playerNickName.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (_player==otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
