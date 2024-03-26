using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        // rotate the projectile towards the target
        transform.Rotate(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        // When target is null, it no longer exists and this
        // object has to be removed 
        if (target == null)
        {
            GameObject.Destroy(this);
        }
        // next, move the projectile towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // finally, check if the distance between this object and
        // the target is smaller than 0.2. If so, destroy this object.
        Enemy enemy = target.GetComponent<Enemy>();
        if (Vector2.Distance(transform.position, target.transform.position) < 0.2f)
        {
            GameObject.Destroy(this);
            enemy.Damage(damage);
        }
    }
}
