using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : Enemy,IShoot,Damage
{
    public int life;
    public float maxDistance = 5f;
    public float moveSpeed = 20f;
    NavMeshAgent agent;
    public Vector2 randomPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform pointShoot;
    [SerializeField] private float timertoShoot;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
    }

    void Update()
    {
        Move();
        timer += Time.deltaTime;
        if (timer >= timertoShoot)
        {
            timer = 0;
            Shoot();
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
        
    public override void Move()
    {
        if (!agent.hasPath || agent.remainingDistance < 0.5f)
        {
            randomPoint = Random.insideUnitCircle.normalized * maxDistance;
            Vector3 targetPosition = new Vector3(randomPoint.x + transform.position.x, transform.position.y, transform.position.z);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, 1f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

        Vector3 moveDirection = agent.desiredVelocity.normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
    }

    public void Shoot()
    {
        Instantiate(bullet, pointShoot.position, pointShoot.rotation);
    }
    public int GetDamage(int damage)
    {
        return life - damage;
    }
}
