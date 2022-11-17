using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ScoreBoardUI : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text _userName;
    [SerializeField] TMP_Text _score;


    Player _player;
    public void Initialize(Player player)
    {
        _userName.text = player.NickName;
        this._player = player;
        updateStats();
    }
    private void updateStats()
    {
        if (_player.CustomProperties.TryGetValue("score", out object score))
        {
            _score.text = score.ToString();
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == _player)
        {
            if (changedProps.ContainsKey("score"))
            {
                updateStats();
            }
        }

    }
}
