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

    public GameObject HitSound;

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

            GameObject hitSoundObj = Instantiate(HitSound);
            hitSoundObj.transform.position = this.transform.position;
            hitSoundObj.GetComponent<AudioSource>().Play();

            Destroy(this.gameObject);
        }
        if(other.tag == "Boss")
        {
            ParticleSystem hit = Instantiate(enemyHitParticle, this.transform.position, Quaternion.identity);
            hit.Play();

            GameObject hitSoundObj = Instantiate(HitSound);
            hitSoundObj.transform.position = this.transform.position;
            hitSoundObj.GetComponent<AudioSource>().Play();

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
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }
}
