using System.Collections;
using UnityEngine;

namespace Player_Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class RaycastGun : MonoBehaviour
    {
        public Camera playerCamera;
        public Transform laserOrigin;
        public float gunRange = 70f;
        public float fireRate = 0.1f;
        public float laserDuration = 0.01f;
 
        LineRenderer laserLine;
        float fireTimer;
 
        void Awake()
        {
            laserLine = GetComponent<LineRenderer>();
        }
 
        void Update()
        {
            fireTimer += Time.deltaTime;
            if(Input.GetButtonDown("Fire1") && fireTimer > fireRate)
            {
                fireTimer = 0;
                laserLine.SetPosition(0, laserOrigin.position);
                Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if(Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
                {
                    laserLine.SetPosition(1, hit.point);
                    // Destroy(hit.transform.gameObject);
                }
                else
                {
                    laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
                }
                StartCoroutine(ShootLaser());
            }
        }
 
        IEnumerator ShootLaser()
        {
            laserLine.enabled = true;
            yield return new WaitForSeconds(laserDuration);
            laserLine.enabled = false;
        }
    }
}