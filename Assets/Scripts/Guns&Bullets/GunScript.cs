using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunScript : MonoBehaviour
{
    public ItemInfo Info;
    public GameObject ItemObject;
    public abstract void Fire(string bulletColor, string bulletType);
}
