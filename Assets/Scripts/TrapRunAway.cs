using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRunAway : Trap
{
    public GameObject Finish;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = Finish.transform.position;
    }

    private void FixedUpdate()
    {
        if (GetActivated())
        {
            Vector3 newPosition = new Vector3(initialPosition.x + 50f, initialPosition.y, initialPosition.z + 50f);
            Finish.transform.position = Vector3.Lerp(Finish.transform.position, newPosition, 0.2f);
        }
    }
}
