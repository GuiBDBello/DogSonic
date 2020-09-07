using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public AudioClip destroySound;

    public float radius = 10.0F;
    public float power = 100.0F;

    public void Shatter()
    {
        AudioController.instance.PlayOneShot(destroySound);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, transform.position, radius, 10.0f);
        }

        Destroy(gameObject);
    }
}
