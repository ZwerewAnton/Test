using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent pathfinder;
    GameObject target = null;
    CharController c_controller;
    public float maxRange = 5f, timeLeft = 2f;
    public GameObject bullet;
    bool stealth = true;
    float timeDead = 0f;
    Animator en_animator;

    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        en_animator = GetComponent<Animator>();
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target==null)
        {
            return;
        }
        else
        {
            c_controller = target.GetComponent<CharController>();
            stealth = c_controller.Stealth();
            timeDead = c_controller.TimeDead();
            timeLeft -= Time.deltaTime;
            if (!stealth && timeLeft <= 0 && timeDead <= 0)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                timeLeft = 2f;
            }
            pathfinder.SetDestination(target.transform.position);
            if (!stealth && timeDead <= 0)
            {

                var heading = target.transform.position - transform.position;
                transform.LookAt(target.transform);
                if (heading.sqrMagnitude < maxRange * maxRange)
                {
                    pathfinder.isStopped = true;
                    en_animator.SetBool("Move", false);
                }
                else
                {
                    en_animator.SetBool("Move", true);
                    pathfinder.isStopped = false;
                }
            }
            else
            {
                pathfinder.isStopped = true;
            }
        }
    }
}
