using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            bulletInstance.GetComponent<Rigidbody>().AddForce(transform.up * bulletSpeed);
        }
    }
}
