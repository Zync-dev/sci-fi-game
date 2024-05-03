using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        this.transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }
}
