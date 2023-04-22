using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected static float weaknessMultiplier = 1.6f;
    protected static float resistanceMultiplier = 0.7f;

    //stats
    public float initialHp = 10;
    private float currentHp;
    public Type resistanceType = Type.Normal;
    public int goldOnDeath = 30;
    public float speed = 1;
    private float TimeToCrossATile => 10 / speed;

    #region PathFinding Variables
    private List<Node> walkableNodes;
    private List<Node> openNodes;
    private List<Node> closedNodes;
    private List<Node> pathToGoal;

    private Node goalNode;
    private Node startNode;

    public Tile StartTile;
    #endregion
    
    public UnityEvent<int> OnDeath;
    public UnityEvent OnGoalReached;
    

    private void Start() {
        
    }


    public void Initialize() {
        currentHp = initialHp;
        PopulateNodes();
        transform.position = StartTile.transform.position + new Vector3(0, 1, 0);
        startNode = walkableNodes.First(node => node.tile.transform.position == StartTile.transform.position);
        FindPathToGoal();
        MoveToGoalFrom(startNode);
    }



    #region PathFinding
    void FindPathToGoal() {

        openNodes = new List<Node>();
        closedNodes = new List<Node>();
        pathToGoal = new List<Node>();

        goalNode = FindClosestGoal();

        startNode.h = Vector3.Distance(startNode.TilePosition, goalNode.TilePosition); //distanza assoluta dalla fine
        startNode.g = 0; //distanza percorsa dall'inizio
        startNode.f = startNode.h; //all'inizio è uguale per formula

        openNodes.Add(startNode);
        Search(startNode);

    }
    public void Search(Node parent) {


        //cerco tutti i nodi vicini
        List<Node> adjacentNodes = FindAdjacentNodes(parent);


        foreach (Node node in adjacentNodes) {

            //aggiungo il nodo alla lista di nodi cercabili
            openNodes.Add(node);
            node.h = Vector3.Distance(node.TilePosition, goalNode.TilePosition);
            node.g = parent.g + Vector3.Distance(node.TilePosition, parent.TilePosition);
            node.f = node.h + node.g;
            node.parent = parent;

            // controllo se tra i nodi trovati c'è il risultato
            if (node.tile.IsGoal) {
                AddCorrectPath(node);
                return;
            }

        }

        closedNodes.Add(parent);
        openNodes.Remove(parent);

        Search( openNodes.OrderBy(node => node.f).First() );

    }

    public void AddCorrectPath(Node node) {
        pathToGoal.Add(node);
        if (node.parent == null) {
            pathToGoal.Reverse();
            return;
        }
        AddCorrectPath(node.parent);


    }

    private List<Node> FindAdjacentNodes(Node parent) {

        List<Node> result = new List<Node>();
        foreach(Tile tile in parent.tile.AdjacentTiles) {
           
           
           Node tempNode = walkableNodes.FirstOrDefault(node =>
                                node.TilePosition == tile.transform.position
                             && !openNodes.Contains(node)
                             && !closedNodes.Contains(node));
            
            if (tempNode != null)
                result.Add(tempNode);

        }
        return result;
        
    }

    private Node FindClosestGoal() {
        //per adesso prende il primo che trova
        return walkableNodes.Find(node => node.tile.IsGoal);
    }

    private void MoveToGoalFrom(Node currentNode) {

        if (currentNode == goalNode) {
            OnGoalReached?.Invoke();
            Die(); //da cambiare
            return;
        }

        Node nextNode = pathToGoal[pathToGoal.IndexOf(currentNode)+1];
        Vector3 destination = new Vector3(nextNode.TilePosition.x,
                                            transform.position.y, 
                                            nextNode.TilePosition.z);
        transform.DOMove(destination, TimeToCrossATile).SetEase(Ease.Linear).OnComplete(() => MoveToGoalFrom(nextNode));
        
    }

    void PopulateNodes() {
        walkableNodes = new List<Node>();
        foreach (Tile tile in GridMap.Instance.WalkableTiles) {
            walkableNodes.Add(new Node(tile));
        }

    }

    //Se muore torna nella pool
    //Se muore guadagni gold
    #endregion
    public void GetDamage(float damage) {
        currentHp -= damage;

        if (currentHp <= 0) {
            Die();
            return;
        }

        gameObject.transform.DOPunchScale(transform.localScale*0.8f, 0.3f,2);

    }

    void Die() {
        OnDeath?.Invoke(goldOnDeath);
        Destroy(gameObject);
    }

    private void OnDestroy() {
        gameObject.transform.DOKill();
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
