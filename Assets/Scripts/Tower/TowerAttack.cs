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
    }

    private void Update() {

        if (!targetEnemy || !targetEnemy.gameObject.activeInHierarchy) {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetEnemy.transform.position) < 0.2f) {
            targetEnemy.GetDamage(damage);
            Destroy(gameObject);

        }

    }
}
