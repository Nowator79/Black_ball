using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Manager manager;
    
    private Rigidbody Rigidbody;
    public float SpeedMove;
    public float SpeedJump;
    public bool IsGnd;
    private bool IsHasControl;
    [SerializeField]
    private new ParticleSystem particleSystem;
    private AudioSource AudioSource;
    [SerializeField]
    private GameObject ParticalKill;
    private GameObject ObjParticalKill;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

        Rigidbody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
        IsGnd = true;
    }
    private void Update()
    {
        //Движение
        if (IsHasControl)
        {
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.C) && IsGnd)
            {
                Jump();
            }
        }
    }

    private void MoveRight(float speed = 1)
    {
        Move(new Vector3(speed * SpeedMove * Time.deltaTime, 0, 0));
    }
    private void MoveLeft(float speed = 1)
    {
        Move(new Vector3(-speed * SpeedMove * Time.deltaTime, 0, 0));
    }
    private void Move(Vector3 vectorMove)
    {
        Rigidbody.AddForce(vectorMove);
    }
    private void Jump(float speed = 1)
    {
        Rigidbody.AddForce(new Vector3(0, speed * SpeedJump, 0));
        IsGnd = false;
    }
    public void OnControl()
    {
        IsHasControl = true;
        Rigidbody.isKinematic = false;
    }
    public void OffControl()
    {
        IsHasControl = false;
        Rigidbody.isKinematic = true;
    }
    public void StopImpulse()
    {
        Rigidbody.AddForce(-Rigidbody.velocity, ForceMode.Impulse);
    }
    public void Kill()
    {
        gameObject.SetActive(false);
        ObjParticalKill = Instantiate(ParticalKill, transform.position, transform.rotation);
        Invoke(nameof(KillEnd), 2);
    }
    private void KillEnd()
    {
        Destroy(ObjParticalKill);
        gameObject.SetActive(true);
        manager.RestartLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGnd = true;
        if (Rigidbody.velocity.magnitude > 3)
        {
            particleSystem.Play();
            AudioSource.Play();
        }
        //float max = 10;
        //float min = 3;
        //Debug.Log(Rigidbody.velocity.magnitude);
    }
    private void OnCollisionStay(Collision collision)
    {
        IsGnd = true;
    }
}
