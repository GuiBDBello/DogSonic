using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, -1f));
    }
}
