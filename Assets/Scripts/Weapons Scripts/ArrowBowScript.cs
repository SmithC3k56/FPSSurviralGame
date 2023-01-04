using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript : MonoBehaviour
{
    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivate_Timer = 3f;

    public float damage = 50f;

    private void Awake(){
        myBody = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        Invoke("DeactivateGameObject",deactivate_Timer);
    }
    public void Lauch(Camera mainCamera){
        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }
    private void DeactivateGameObject(){
        if(gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        // after we touch an enemy deactivate game object
        if(target.tag == Tags.ENEMY_TAG){
            target.GetComponent<HealthScript>().ApplyDamage(damage);
            // gameObject.SetActive(false);
        }
    }
}
