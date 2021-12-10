using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Manager manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
            manager.LevelSuccess();
        }
    }
}
