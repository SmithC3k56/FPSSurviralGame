using System;
using UnityEngine;


    public class EatItem: MonoBehaviour
    {
        [SerializeField] private float health = 20f;
        [SerializeField] private HealthScript _healthScript;

       

        private void OnTriggerEnter(Collider target)
        {
            // after we touch an enemy deactivate game object
            if(target.tag == Tags.PLAYER_TAG)
            {
                _healthScript.EatItem(this.health);
                gameObject.SetActive(false);
            }
        }
    }
