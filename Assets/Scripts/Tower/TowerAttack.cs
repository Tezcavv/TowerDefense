using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float damage = 1;
    public float speed = 1;

    public Enemy targetEnemy;

    public void Shoot(Enemy target, Tower tower) {
        damage = tower.AttackDamage;
        targetEnemy = target;
        speed = tower.AttackSpeed*6;

        //Vector3 targetPostition = new Vector3(target.transform.position.x,
        //                              this.transform.position.y,
        //                              target.transform.position.z);
        transform.DOLookAt(target.transform.position, 0.1f);
    }

    private void Update() {

        if (!targetEnemy || !targetEnemy.gameObject.activeInHierarchy) {
            Destroy(gameObject);
            return;
        }                           
        
        //TODO migliorare il contatto, al momento va verso l'origine del nemico (male se è ai piedi)
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetEnemy.transform.position) < 0.2f) {
            targetEnemy.GetDamage(damage);
            Destroy(gameObject);

        }

    }
}
