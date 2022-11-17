using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class RoomListButtonItem : MonoBehaviour
{
    [SerializeField] private TMP_Text ButtonText;
    public RoomInfo _info;
    public void SetUp(RoomInfo info)
    {
        _info = info;
        ButtonText.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(_info);
    }
}
