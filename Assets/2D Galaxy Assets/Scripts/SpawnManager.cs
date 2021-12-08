using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameManager _gameManager;


    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
       

    }

    public void startSpawning()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(PowerupSpawn());
    }


    // Update is called once per frame
  
    IEnumerator EnemySpawn()
    {
        while(_gameManager.gameOver == false)
        {
            Instantiate(Enemy, new Vector3(Random.Range(-10.7f, 10.7f), 4.34f), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawn()
    {
        while (_gameManager.gameOver == false) { 
        int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup],new Vector3(Random.Range(-10.7f, 10.7f), 4.34f,0), Quaternion.identity);
            yield return new WaitForSeconds(20.0f);
        }
    }


}
