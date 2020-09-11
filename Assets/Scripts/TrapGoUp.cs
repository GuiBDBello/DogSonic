using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGoUp : Trap
{
    private void FixedUpdate()
    {
        if (GetActivated())
        {
            Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, 1f);
            SetActivated(false);
        }
    }
}
