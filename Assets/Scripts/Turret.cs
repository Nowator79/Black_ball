using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject Target;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject TurretMuzzle;
    [SerializeField]
    private float rechargeTime;
    [SerializeField]
    private float FireForse = 300;
    private float rechargeTimer = 0;
    [SerializeField]
    private Transform firePoint;
    public void SetTarget(GameObject _Target)
    {
        this.Target = _Target;
    }
    public void MissTarget()
    {
        Target = null;
    }

    private void Update()
    {
        rechargeTimer += Time.deltaTime;
        if (Target)
        {
            TurretMuzzle.transform.LookAt(Target.transform);

            if (rechargeTimer > rechargeTime)
            {
                Fire();
                rechargeTimer = 0;
            }
        }
    }
    private void Fire()
    {
        GameObject _bullet = Instantiate(bullet);
        _bullet.transform.position = firePoint.position;
        _bullet.transform.rotation = firePoint.rotation;
        Rigidbody rb_bullet = _bullet.GetComponent<Rigidbody>();
        rb_bullet.AddForce(firePoint.TransformPoint(new Vector3(0, 0, FireForse)));
    }
}
