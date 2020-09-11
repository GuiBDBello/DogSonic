using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPush : Trap
{
    public GameObject Block;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = Block.transform.position;
    }

    private void FixedUpdate()
    {
        if (GetActivated())
        {
            Vector3 newPosition = new Vector3(initialPosition.x + 30f, initialPosition.y, initialPosition.z);
            Block.transform.position = Vector3.Lerp(Block.transform.position, newPosition, 0.075f);
        }
    }
}
