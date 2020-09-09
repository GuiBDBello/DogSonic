using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection;

    private void Start()
    {
        StartCoroutine("ChangeDirection");
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * Time.deltaTime);
    }

    IEnumerator ChangeDirection()
    {
        for(;;)
        {
            yield return new WaitForSeconds(1f);
            moveDirection *= -1;
        }
    }
}
