using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : Enemy,IShoot
{
    public GameObject player;
    public int life;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pointShoot;
    [SerializeField] private float timertoShoot;
    float timer;
    public float separationDistance = 2.0f;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timertoShoot)
        {
            timer = 0;
            Shoot();
        }
        Move();
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position - transform.forward * separationDistance;
            agent.SetDestination(targetPosition);
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, pointShoot.position, Quaternion.identity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Damage>() != null)
        {
            life -= collision.gameObject.GetComponent<Damage>().GetDamage();
        }
    }
}
