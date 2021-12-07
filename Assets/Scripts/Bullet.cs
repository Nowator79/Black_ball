using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject ParticalSmoke;

    public List<string> BlackLsit;
    private void OnTriggerEnter(Collider other)
    {
        if (BlackLsit.Contains(other.name))
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Kill();
        }
        gameObject.SetActive(false);
        Instantiate(ParticalSmoke, transform.position, transform.rotation);
        Invoke(nameof(DestroyBullet), 3);
    }
    private void DestroyBullet()
    {

        Destroy(gameObject);
    }
}
