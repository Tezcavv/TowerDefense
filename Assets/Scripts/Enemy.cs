using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    protected static float weaknessMultiplier = 1.6f;
    protected static float resistanceMultiplier = 0.7f;

    protected int hp = 10;
    protected Type resistanceType = Type.Normal;
    protected float goldOnDeath = 30f;



    //largamente migliorabile
    public float EffectivenessMultiplier(Type enemyDamageType) {

        if(enemyDamageType == Type.Normal || resistanceType==Type.Normal) {
            return 1;
        }
        
        if(enemyDamageType == resistanceType) {
            return 1;
        }

        switch (enemyDamageType) {
            case Type.Fire: {
                if(resistanceType == Type.Water)return resistanceMultiplier;
                if (resistanceType == Type.Grass) return weaknessMultiplier;
                break;
            }
            case Type.Grass: {
                if(resistanceType == Type.Fire)return resistanceMultiplier;
                if (resistanceType == Type.Water) return weaknessMultiplier;
                break;
            }
            case Type.Water: {
                if(resistanceType==Type.Grass) return resistanceMultiplier;
                if (resistanceType == Type.Fire) return weaknessMultiplier;
                break;
            }
        }

        return 1;
    }
}
