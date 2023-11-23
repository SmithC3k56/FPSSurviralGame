using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    public bool is_Player, is_Boar, is_Cannibal;

    private bool is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats player_Stats;


    private void Awake()
    {
        if(is_Boar || is_Cannibal){
             Debug.Log("Cannibal died");
            enemy_Anim = GetComponent<EnemyAnimator>();
            navAgent = GetComponent<NavMeshAgent>();
            enemy_Controller = GetComponent<EnemyController>();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if(is_Player){
            player_Stats = GetComponent<PlayerStats>();
        }
    }

    public void EatItem(float amount)
    {
        health += amount;
    }
    public void ApplyDamage(float damage){
      // if died, do not apply dammage
        if(is_Dead)return;

        health -= damage;
        if(is_Player){
            player_Stats.Display_HealthStats(health);
            // show the state( display the health UI value)
        }

        if(is_Boar || is_Cannibal){
          
            if(enemy_Controller.Enemy_State == EnemyState.PATROL){
                enemy_Controller.chase_Distance = 50f;
            }
        }
        if(health <= 0f){
            PlayerDied();
        }
    
    }
    private void PlayerDied(){
        if(is_Cannibal){
           
            EnemyManager.instance.SpawnItems(gameObject.transform.position);
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            // GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            enemy_Controller.enabled = false;
            navAgent.enabled = false;
            enemy_Anim.enabled = false;

            StartCoroutine(DeadSound());

            //EnemyManager spawn more enemies
            EnemyManager.instance.EnemyDied(true);
        }

        if(is_Boar){
            EnemyManager.instance.SpawnItems(gameObject.transform.position);
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;

            enemy_Anim.Dead();

            StartCoroutine(DeadSound());
            //EnemyManager spawn more enemies

            EnemyManager.instance.EnemyDied(false);

        }

        if(is_Player){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for(int i = 0; i < enemies.Length; i++){
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            // call enemy manager to stop spawning enemies
            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

            //start Coroutine
            //PlayerManager.instance.GameOver();
        }

        if(tag == Tags.PLAYER_TAG){
            Invoke("RestartGame",3f);
        }else{
            Invoke("TurnOffGameObject",3f);
        }

    }

    private void RestartGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Menu 3D");
    }

    private void TurnOffGameObject(){
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound(){
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
    }


}
