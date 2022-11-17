using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class TargetScript : MonoBehaviour, IDamagable
{
    [SerializeField] PhotonView pv;
    public string _color;
    public string _type;
    public Vector3 _createPosition;

    public void ShootByPlayer(string color, string bulletType)
    {

        pv.RPC("newPosition", RpcTarget.All);
    }
    [PunRPC]
    public void newPosition()
    {
        transform.position = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
    }

    public void changePropertiesOfTarget(string color, string bulletType)
    {
        _color = color;
        _type = bulletType;
    }
}
