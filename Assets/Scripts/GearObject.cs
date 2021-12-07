using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearObject : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject Rotor;

    private Vector3 RotationRotor;
    private void Update()
    {
        RotationRotor = new Vector3(0, 0, speed * Time.deltaTime + RotationRotor.z);
        Rotor.transform.rotation = Quaternion.Euler(RotationRotor);
    }
}
