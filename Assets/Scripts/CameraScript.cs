using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;
    public float Zoom;
    public float DragSpeed = 1;
    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Player)
        {
            Vector3 vector;
            vector = Vector3.Lerp(transform.position, Player.position, DragSpeed);
            vector = new Vector3(vector.x, vector.y, Player.transform.position.z - Math.Abs(Zoom));
            transform.position = vector;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                Player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
