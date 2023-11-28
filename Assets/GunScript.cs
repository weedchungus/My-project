using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public float fireRate = 15f;


    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloatTime = 1f;
    private bool isReloading = false;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading....");
        yield return new WaitForSeconds(reloatTime);
        currentAmmo = maxAmmo;
        isReloading = false;

    }
    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
