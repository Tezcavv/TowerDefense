using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected static float weaknessMultiplier = 1.6f;
    protected static float resistanceMultiplier = 0.7f;

    //stats
    public int hp = 10;
    public Type resistanceType = Type.Normal;
    public float goldOnDeath = 30f;
    public int speed = 10;
    private float TimeToCrossATile => 10 / speed;

    //nodes for path finding
    private List<Node> walkableNodes;
    private List<Tile> pathToGoal;

    //events
    public UnityEvent<float> OnDeath;
    

    private void Start() {
        
        PopulateNodes();

    }

    void PopulateNodes() {
        walkableNodes = new List<Node>();
        foreach (Tile tile in Grid.Instance.WalkableTiles) {
            walkableNodes.Add(new Node(tile));
        }

    }

    //Se muore torna nella pool
    //Se muore guadagni gold

    public void GetDamage(int damage) {
        hp -= damage;

        if (hp <= 0) {
            Die();
        }

    }

    void Die() {
        OnDeath?.Invoke(goldOnDeath);
    }

    private void FixedUpdate() {
        if (true) {

        }
    }

    void FindPathToGoal() {



        //listanodiwalkabili
        //per ogni nodo gli 
    }

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
