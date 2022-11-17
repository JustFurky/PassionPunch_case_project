using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance;
    public List<GameObject> targetList = new List<GameObject>();
    [SerializeField] GameObject TargetReference;
    [SerializeField] PhotonView pv;
    string[] bulletTypes = { "Large", "Small", "Standart" };
    string[] bulletColors = { "Red", "Blue", "Green" };
    string nextColor;
    string nextType;
    float timer, MaxTime = 60;
    GameObject _createdTarget;
    private void Awake()
    {
        Instance = this;
        ChangeTargetsVariables();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= MaxTime)
        {
            timer = 0;
            ChangeTargetsVariables();
        }
    }

    public void startCommand()
    {
        createTargets();
    }

    [PunRPC]
    public void createTargets()
    {
        if (pv.Owner.IsMasterClient)
        {
            pv.Owner.NickName = "MasterLordCreatorAndWarrior" + PhotonNetwork.IsMasterClient;

            for (int i = 0; i < 30; i++)
            {
                _createdTarget = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TargetObject"), new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)), Quaternion.identity);
                targetList.Add(_createdTarget);
            }
            ChangeTargetsVariables();
            setVariables();
        }
    }

    void setVariables()
    {
        pv.RPC("ChangeTargetsVariables", RpcTarget.All);
    }

    [PunRPC]
    public void ChangeTargetsVariables()
    {
        setNexts();
        for (int i = 0; i < targetList.Count; i++)
        {
            targetList[i].GetComponent<IDamagable>().changePropertiesOfTarget(nextColor, nextType);
        }
    }
    void setNexts()
    {
        nextColor = bulletColors[Random.Range(0, bulletColors.Length - 1)];
        nextType = bulletTypes[Random.Range(0, bulletTypes.Length - 1)];
        StateUIUpdater.Instance.setTexts(nextColor, nextType);
    }
}
