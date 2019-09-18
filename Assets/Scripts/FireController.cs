using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    float timeLife;
    void Start()
    {
        timeLife = 0.3f;
    }
    private void Update()
    {
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            Destroy(this);
        }
    }
}
