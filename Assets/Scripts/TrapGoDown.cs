using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGoDown : MonoBehaviour
{
    private bool activated;

    public void SetActivated(bool activated)
    {
        this.activated = activated;
    }
    
    private void FixedUpdate()
    {
        if (activated)
        {
            transform.position += Vector3.down;
        }
    }
}
