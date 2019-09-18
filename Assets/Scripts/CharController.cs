using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CharController : MonoBehaviour
{
    private Vector3 moveDirection, rotateDirection;
    public float rotationSpeed = 10f, speed, movementSpeed = 1.0f;
    public float allowRotation = 0.1f;
    float timeLeft = 0f, timeDead = 0f;
    public JoystickController joystickController;
    public RotationController rotationController;
    Text fragsUI;
    int frags = 0;
    public PlayerHealthController pHC;
    float nextShootTime;
    public GameObject bullet;
    private bool longShoot = false, quickShoot = false, needShoot = false, stealth = false, isDead = false;
    GameObject closestEnemy = null;
    private CharacterController ch_controller;
    private Animator ch_animator;

    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        joystickController = GameObject.FindObjectOfType<JoystickController>();
        rotationController = GameObject.FindObjectOfType<RotationController>();
        fragsUI = GameObject.FindGameObjectWithTag("Frags").GetComponent<Text>();
        fragsUI.text = "Frags: " + frags.ToString();
        pHC = gameObject.GetComponent<PlayerHealthController>();
    }
    
    void Update()
    {
        timeDead -= Time.deltaTime;
        isDead = pHC.IsDead();
        stealth = false;
        if (!isDead && timeDead <= 0)
        {
            ch_animator.SetBool("IsDead", false);
            moveDirection.x = joystickController.Horizontal();
            moveDirection.z = joystickController.Vertical();
            rotateDirection.x = rotationController.Horizontal();
            rotateDirection.z = rotationController.Vertical();
            InputDecider();
            MovementManager();
            longShoot = rotationController.LongShoot();
            quickShoot = rotationController.QuickShoot();
            timeLeft -= Time.deltaTime;
            if (longShoot || (needShoot && timeLeft <= 0))
            {

                Shoot();
                needShoot = false;
            }
            if (quickShoot)
            {
                FindClosestEnemy();
                timeLeft = 0.3f;
                needShoot = true;
            }
        }
        if(isDead)
        {
            frags++;
            fragsUI.text = "Frags: " + frags.ToString();
            timeDead = 3f;
            ch_animator.Play("Dead");
        }
    }

    private void LateUpdate()
    {
        if (isDead)
        {
            transform.position = Vector3.zero;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);     
    }

    void MovementManager()
    {
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            ch_animator.SetBool("Move", true);
        }
        else
        {
            ch_animator.SetBool("Move", false);
        }
        ch_controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
    }

    void RotationManager()
    {

        if (rotateDirection.x == 0)
        {
            rotateDirection.x = moveDirection.x;
        }

        if (rotateDirection.z == 0)
        {
            rotateDirection.z = moveDirection.z;
        }

        if (needShoot)
        {
            if(closestEnemy != null)
                transform.LookAt(closestEnemy.transform);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateDirection), rotationSpeed);
        }
    }

    void InputDecider()
    {
        speed = new Vector2(moveDirection.x, moveDirection.z).sqrMagnitude;

        if(speed > allowRotation || rotateDirection.x != 0 || rotateDirection.z != 0 || needShoot)
        {
            RotationManager();
        }
    }

    public void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = enemy;
            }
        }     
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Grass")
        {
            stealth = true;
        }
    }
    public bool Stealth()
    {
        return stealth;
    }
    public float TimeDead()
    {
        return timeDead;
    }
}
