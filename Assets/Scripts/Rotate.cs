using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, -0.5f));
    }
}
