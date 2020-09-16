using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    public float explosionRadius = 10f;
    public float explosionPower = 100f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explosion(rb, explosionPower * 100, collision.gameObject.transform.position, explosionRadius, collision.gameObject.GetComponent<Rigidbody2D>());
    }

    private void Explosion(Rigidbody2D rb, float force, Vector3 pos, float rad, Rigidbody2D colRb)
    {
        Vector2 dir = rb.transform.position - pos;
        float calc = 1 - (dir.magnitude / rad);
        Debug.Log("boom");
        if (calc <= 0)
        {
            calc = 0;
        }
        rb.AddForce(dir.normalized * force * calc);

        if (colRb == null)
            return;
        else
        {
            colRb.AddForce(-dir.normalized * force * calc);
        }
        // Kode Explosion
        // https://feenikxfire.wordpress.com/2016/06/17/ball-ball-tnt-2d-explosions/
    }

    public override void OnTap()
    {

    }
}
