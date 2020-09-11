using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapThrowUp : Trap
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void FixedUpdate()
    {
        if (GetActivated())
        {
            player.GetComponent<PlayerMovement>().ThrowUp();
            SetActivated(false);
        }
    }
}
