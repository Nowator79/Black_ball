using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickTest : BtnClick
{
    protected override void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (Active)
            {
                manager.MoveForTestLevel();
            }
        }
    }
}
