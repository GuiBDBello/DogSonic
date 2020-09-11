using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGoDown : Trap
{
    private void FixedUpdate()
    {
        if (GetActivated())
        {
            transform.position += Vector3.down;
            Destroy(gameObject, 5f);
        }
    }
}
