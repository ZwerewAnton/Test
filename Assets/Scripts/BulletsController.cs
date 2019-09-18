using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public float speed = 15f;
    private int damage = 20;
    public GameObject fire;

    void Awake()
    {
        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if(tag == "PlayerBullet" && other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthController>().Hurt(damage);
            Destroy(this.gameObject);
        }
        if (tag == "EnemyBullet" && other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealthController>().Hurt(damage);
            speed = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


}
