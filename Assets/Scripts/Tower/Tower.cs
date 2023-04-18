using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Type { Fire, Grass, Water, Normal }

public class Tower : MonoBehaviour
{

    //tower basic stats
    [SerializeField] private float rangeRadius = 4f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackSpeed = 0.6f;
    [SerializeField] private Type damageType = Type.Normal;

    //upgrade-centric fields
    [SerializeField] private float costToUpgrade;
    [SerializeField] private Tower upgradedTower;

    //target fields
    private Enemy targetEnemy;

    //attack fields
    private float timer;


    #region Properties
    public float RangeRadius => rangeRadius;
    public float AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public Type DamageType => damageType;
    public float CostToUpgrade => costToUpgrade;

    #endregion

    private void Start() {
        timer = 0;
    }

    private void Update() {
        timer += Time.deltaTime;
        if(targetEnemy && timer >= 1 / attackSpeed) {
            timer = 0;
            Attack(targetEnemy);
        }
    }



    private void FixedUpdate() {
        ManageTargetPriority();
    }

    #region Target Acquisition

    void ManageTargetPriority() {


        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeRadius, 1 << 7);


        if (colliders.Length <= 0) {
            return;
        }
        if (!targetEnemy) {
            targetEnemy = GetClosestCollider(colliders).GetComponent<Enemy>();
        }
    }

    Collider GetClosestCollider(Collider[] colliders) {

        Collider closest = colliders[0];

        foreach (Collider coll in colliders) {

            if (Vector3.Distance(transform.position, coll.gameObject.transform.position)
                < Vector3.Distance(transform.position, closest.gameObject.transform.position)) {
                closest = coll;
            }

        }

        return closest;
    }

    #endregion

    #region Attack
    private void Attack(Enemy targetEnemy) {

    }
    #endregion

    //private void OnMouseDown() {

    //    //per la prima volte che viene selezionata una torre
    //    if (!GameManager.Instance.selectedTower) {
    //        GameManager.Instance.selectedTower = this;
    //    }

    //    Tower selectedTower = GameManager.Instance.selectedTower;

    //    if (selectedTower.gameObject != gameObject) {
    //        selectedTower.gameObject.GetComponent<LineRenderer>().enabled= false;
    //    }

    //    GetComponent<LineRenderer>().enabled = true;
    //    GameManager.Instance.selectedTower = this;

    //}

    
}
