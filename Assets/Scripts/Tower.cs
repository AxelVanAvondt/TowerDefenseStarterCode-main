using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 1f; // Range within which the tower can detect and attack enemies
    public float attackRate = 1f; // How often the tower attacks (attacks per second)
    public int attackDamage = 1; // How much damage each attack does
    public float attackSize = 1f; // How big the bullet looks 

    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 

    private float nextAttackTime; // Time of the next attack

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= nextAttackTime)
        {
            // Find all enemies within attack range
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

            // Attack the first enemy found
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    // Shoot at the enemy
                    Shoot(enemy.gameObject);
                    break; // Stop searching for enemies after shooting one
                }
            }

            // Update the time of the next attack
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    // Method to shoot at the enemy
    void Shoot(GameObject enemy)
    {
        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set bullet properties
        Projectile projectile = bullet.GetComponent<Projectile>();
        projectile.damage = attackDamage;
        projectile.target = enemy.transform;

        // Set bullet scale
        bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);
    }

    // Draw the attack range in the editor for easier debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

//public class Tower : MonoBehaviour
//{
//    public float attackRange = 1f; // Range within which the tower can detect and attack enemies
//    public float attackRate = 1f; // How often the tower attacks (attacks per second)
//    public int attackDamage = 1; // How much damage each attack does
//    public float attackSize = 1f; // How big the bullet looks 

//    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
//    public TowerType type; // the type of this tower 

//    // Draw the attack range in the editor for easier debugging
//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;

//        Gizmos.DrawWireSphere(transform.position, attackRange);

//    }
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}