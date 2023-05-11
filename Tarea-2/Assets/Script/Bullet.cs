using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class Bullet : MonoBehaviour, Damage
{

    protected Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void  Update()
   {
        Destroy(gameObject, 4f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    public int GetDamage()
    {
        return damage;
    }
}
