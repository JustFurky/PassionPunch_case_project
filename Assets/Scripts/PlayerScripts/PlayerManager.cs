using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView _photonView;

    public bool isMasterClient = false;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            CreateContoller(); 
        }
        if (PhotonNetwork.IsMasterClient&&PlayerPrefs.GetInt("MasterClient")==5000)
        {
            if (_photonView.IsMine)
            {
                TargetManager.Instance.startCommand();
                PlayerPrefs.SetInt("MasterClient", 5001); 
            }
        }
    }
    private void CreateContoller()
    {
       // if (PhotonNetwork.IsMasterClient)
       // {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(Random.Range(0,50),0, Random.Range(0, 50)), Quaternion.identity); 
        //}
    }
}
