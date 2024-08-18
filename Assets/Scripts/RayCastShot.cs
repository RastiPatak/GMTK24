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

    [SerializeField] CharacterController3D charactercontrl;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFire && !charactercontrl.AbilityOnCooldown)
        {
            Debug.Log("Shot initiated");
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserLine.SetPosition(0, camera.transform.position - new Vector3(0.1f, 0.1f, -0.1f));

            var isHit = Physics.Raycast(rayOrigin, camera.transform.forward, out hit, weaponRange);
            if (isHit)
            {
                laserLine.SetPosition(1, hit.point);
                Debug.Log(hit);
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    
                    MonsterObject monsterObject = hit.rigidbody.GetComponent<MonsterObject>();
                    

                    int damage = Random.Range(1, 2);

                    monsterObject.lp -= damage;
                    monsterObject.Smaller();
                    Debug.Log(monsterObject.lp);
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
