using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 5f;

    public GameObject enemyHitObj;
    public GameObject otherHitObj;

    ParticleSystem enemyHitParticle;
    ParticleSystem otherHitParticle;

    private void Start()
    {
        this.transform.position += transform.forward * 1f;

        enemyHitParticle = enemyHitObj.GetComponent<ParticleSystem>();
        otherHitParticle = otherHitObj.GetComponent<ParticleSystem>();

        StartCoroutine(DestroyObj());
    }

    void Update()
    {
        this.transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.tag == "Enemy")
        {
            ParticleSystem hit = Instantiate(enemyHitParticle, this.transform.position, Quaternion.identity);
            hit.Play();

            EnemyScript enemyScript = other.gameObject.GetComponent<EnemyScript>();

            enemyScript.enemyHealth--;

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Enemy")
        {
            ParticleSystem hit = Instantiate(otherHitParticle, this.transform.position, Quaternion.identity);
            hit.Play();
        }
    }

    public IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
