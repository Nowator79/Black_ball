using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetector : MonoBehaviour
{
    private Turret Turret;
  
    private void Start()
    {
        Turret = transform.parent.GetComponent<Turret>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Turret.SetTarget(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Turret.MissTarget();
        }
    }
}
