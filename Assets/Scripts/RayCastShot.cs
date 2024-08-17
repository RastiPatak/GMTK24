using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShot : MonoBehaviour
{
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 10f;

    private Camera camera;
    private float nextFire;
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire)
        {
            Debug.Log("Shot initiated");
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserLine.SetPosition(0, camera.transform.position - new Vector3(0.1f, 0.1f, -0.1f));
            if (Physics.Raycast(rayOrigin, camera.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + camera.transform.forward * weaponRange);
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}
