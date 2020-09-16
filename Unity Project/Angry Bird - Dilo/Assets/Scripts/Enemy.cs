using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };
    private bool _isHit = false;

    void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
            return;

        if(collision.collider.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        else if (collision.collider.tag == "Obstacle")
        {
            float dmg = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            health -= dmg;

            if(health<= 0)
            {
                _isHit = true;
                Destroy(gameObject);
            }
        }
    }


}
