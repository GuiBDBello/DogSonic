using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;
    public float timeToAutoDestroy = 5f;

    private GameObject player;
    private float timeRoaming = 0f;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }
    
    private void FixedUpdate()
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (playerDistance < 20f)
        {
            timeRoaming = 0f;
            gameObject.transform.LookAt(Vector3.Lerp(gameObject.transform.position, player.transform.position, 0.0005f));
        }
        else timeRoaming += Time.deltaTime;

        if (timeRoaming >= timeToAutoDestroy) Destroy(gameObject);

        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
