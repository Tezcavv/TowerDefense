using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum Type { Fire, Grass, Water, Normal }

public class Tower : MonoBehaviour
{

    //tower basic stats
    [SerializeField] private float rangeRadius = 4f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackSpeed = 0.6f;
    [SerializeField] private Type damageType = Type.Normal;

    //cost/upgrade-centric fields
    [SerializeField] private int baseCost;
    [SerializeField] private float costToUpgrade;
    [SerializeField] private Tower upgradedTower;

    //target fields
    private Enemy targetEnemy;

    //attack fields
    private float timer;
    [SerializeField] TowerAttack projectile;
    [SerializeField] GameObject unitOnTop;


    #region Properties
    public float RangeRadius => rangeRadius;
    public float AttackDamage => attackDamage;
    public float AttackSpeed => attackSpeed;
    public Type DamageType => damageType;
    public float CostToUpgrade => costToUpgrade;

    public int BaseCost => baseCost;

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


        List<Collider> colliders = Physics.OverlapSphere(transform.position, rangeRadius, 1 << 7).ToList();

        if (colliders.Count <= 0) {
            targetEnemy = null;
            return; 
        }

        if (targetEnemy && colliders.Contains(targetEnemy.gameObject.GetComponent<Collider>()))
            return;

        targetEnemy = GetClosestCollider(colliders).GetComponent<Enemy>();
  
    }

    Collider GetClosestCollider(List<Collider> colliders) {

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

        //the unit will turn to the enemy when attacking
        
        Vector3 targetPostition = new Vector3(targetEnemy.transform.position.x,
                                              this.transform.position.y,
                                              targetEnemy.transform.position.z);
        unitOnTop.transform.DOLookAt(targetPostition, 0.1f);

        TowerAttack attack = Instantiate(projectile, unitOnTop.transform.position,transform.rotation);
        attack.Shoot(targetEnemy,this);

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
