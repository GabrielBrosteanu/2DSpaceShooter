using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject Player;
    private UImanager _uimanager;
    
    private void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    void Update()
    {
        if(gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(Player,Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uimanager.hideTitleScreen();
            }
        }
    }
}
