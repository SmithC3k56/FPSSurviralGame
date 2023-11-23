using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField] private GameObject boar_Prefab, cannibal_Prefab;

    public Transform[] cannibal_SpawnPoints, boar_SpawnPoints;

    [SerializeField] private int cannibal_Enemy_Count, boar_Enemy_Count;

    private int initial_Cannibal_Count, initial_Boar_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    [SerializeField] private GameObject HPItem, MPItem;
    [SerializeField] private int itemCount;
    private int initial_Item_Count;
    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        initial_Cannibal_Count = cannibal_Enemy_Count;
        initial_Boar_Count = boar_Enemy_Count;
        initial_Item_Count = itemCount;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    

    private void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    private void SpawnEnemies(){
        SpawnCannibals();
        SpawnBoars();
        

    }

    private void SpawnCannibals(){
        int index = 0;
        for(int i =0; i< cannibal_Enemy_Count; i++){

            if(index >= cannibal_SpawnPoints.Length){
                index = 0;
            }

            Instantiate(cannibal_Prefab, cannibal_SpawnPoints[index].position, Quaternion.identity);

            index++;
        }

        cannibal_Enemy_Count = 0;

    }

    private void SpawnBoars(){
        int index = 0;
        for(int i =0; i< boar_Enemy_Count; i++){

            if(index >= boar_SpawnPoints.Length){
                index = 0;
            }
            
            Instantiate(boar_Prefab, boar_SpawnPoints[index].position, Quaternion.identity);

            index++;
        }

        boar_Enemy_Count = 0;
    }
    public void SpawnItems(Vector3 location)
    {
        Vector3 newLocation = new Vector3(location.x, 22, location.z);
        int index = 0;
        for(int i =0; i< itemCount; i++){

            if(index >= boar_SpawnPoints.Length){
                index = 0;
            }
            int randomNumber = Random.Range(1, 4);
            switch (randomNumber)
            {
                case 1:
                    Instantiate(HPItem, newLocation, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(MPItem, newLocation, Quaternion.identity);
                    break;
                default:
                    break;
            }

            index++;
        }

        itemCount = 0;
    }

    IEnumerator CheckToSpawnEnemies(){
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);
        SpawnCannibals();

        SpawnBoars();

        StartCoroutine("CheckToSpawnEnemies");

    }

    public void EnemyDied(bool cannibal){
        
        if(cannibal){
            // SpawnItems(cannibal_Prefab.transform.position);
            cannibal_Enemy_Count++;
            if(cannibal_Enemy_Count > initial_Cannibal_Count){
                cannibal_Enemy_Count = initial_Cannibal_Count;
            }
        }
        else{
            boar_Enemy_Count++;
            // SpawnItems(boar_Prefab.transform.position);
            if(boar_Enemy_Count > initial_Boar_Count){
                boar_Enemy_Count = initial_Boar_Count;
            }
        }
    }

    public void StopSpawning(){
        StopCoroutine("CheckToSpawnEnemies");
    }

}
