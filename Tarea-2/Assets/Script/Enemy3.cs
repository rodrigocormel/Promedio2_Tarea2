using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3 : Enemy, IShoot
{
    public GameObject player;
    public int life;
    public float safeDistance = 5f;
    NavMeshAgent agent;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pointShoot;
    [SerializeField] private float timertoShoot;
    float timer;
    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timertoShoot)
        {
            timer = 0;
            Shoot();
        }
        Move();
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Move()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < safeDistance)
            {

                Vector3 direction = transform.position - player.transform.position;
                direction = direction.normalized;


                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position + direction * 10f, out hit, 20f, NavMesh.AllAreas))
                {
                    destination = hit.position;
                }


                agent.SetDestination(destination);
            }
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
