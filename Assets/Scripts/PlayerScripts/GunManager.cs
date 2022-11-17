using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using HashTable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class GunManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Camera _characterCamera;
    [SerializeField] GunScript[] Weapons;
    [SerializeField] string[] BulletColor;
    [SerializeField] PhotonView _playerPhotonViewer;
    [HideInInspector] public int WeaponsCount;
    private string _selectedColor;
    private string _selectedBullet;
    private int _itemIndex;
    private int _bulletIndex;
    private int _previousBulletIndex = -1;
    private int _previousItemIndex = -1;
    private void Start()
    {
        if (_playerPhotonViewer.IsMine)
        {
            EquipItems(0);
            ChangeBulletColor(0);
        }
    }
    private void Update()
    {
        if (!_playerPhotonViewer.IsMine)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Weapons[_itemIndex].Fire(_selectedColor, _selectedBullet);
        }
        //Weapon & Bullet Select For Player Input
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItems(i);
                break;
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (_bulletIndex >= BulletColor.Length - 1)
                ChangeBulletColor(0);
            else
                ChangeBulletColor(_bulletIndex + 1);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (_bulletIndex <= BulletColor.Length - 1)
                ChangeBulletColor(BulletColor.Length - 1);
            else
                ChangeBulletColor(_bulletIndex - 1);
        }

    }

    private void ChangeBulletColor(int _index)
    {
        if (_index == _previousBulletIndex)
            return;
        _bulletIndex = _index;
        _selectedColor = BulletColor[_bulletIndex];
        if (_previousBulletIndex != -1)
        {
            _selectedColor = BulletColor[_previousBulletIndex];
        }
        _previousBulletIndex = _bulletIndex;
        StateUIUpdater.Instance.setGunColor(_selectedColor);
        if (_playerPhotonViewer.IsMine)
        {
            HashTable hash = new HashTable();
            hash.Add("BulletIndex", _bulletIndex);
            hash.Add("ItemIndex", _itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    private void EquipItems(int _index)
    {
        if (_index == _previousItemIndex)
            return;
        _itemIndex = _index;
        _selectedBullet = Weapons[_itemIndex].name;
        Weapons[_itemIndex].ItemObject.SetActive(true);
        if (_previousItemIndex != -1)
        {
            Weapons[_previousItemIndex].ItemObject.SetActive(false);
        }
        _previousItemIndex = _itemIndex;
        if (_playerPhotonViewer.IsMine)
        {
            HashTable hash = new HashTable();
            hash.Add("ItemIndex", _itemIndex);
            hash.Add("BulletIndex", _bulletIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps)
    {
        if (!_playerPhotonViewer.IsMine && targetPlayer == _playerPhotonViewer.Owner)
        {
            EquipItems((int)changedProps["ItemIndex"]);
            ChangeBulletColor((int)changedProps["BulletIndex"]);
        }
    }
}
