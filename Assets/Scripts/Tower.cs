using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Fire, Grass, Water, Normal }

public class Tower : MonoBehaviour
{

    //tower basic stats
    protected float rangeRadius = 4f;
    protected int attack = 5;
    protected float rateOfFire = 0.6f;
    protected Type damageType = Type.Normal;

    //upgrade-centric fields
    protected float costToUpgrade;
    protected Tower upgradedTower;



}
