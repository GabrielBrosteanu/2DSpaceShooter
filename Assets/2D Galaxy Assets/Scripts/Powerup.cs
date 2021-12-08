using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;//0->tripleshot,1->speed boost,2->shield powerup
    [SerializeField]
    private AudioClip music;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5.85)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Powerup collided with:" + other.name);

            //acces the player
            Player ship = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(music, Camera.main.transform.position, 1f);

            if(ship!=null)
            {
                //enable triple shot

                if (powerupID == 0)
               { 
                    ship.TripleShotPowerUpOn();
                    StartCoroutine(ship.TripleShotPowerDown());
                }
                else if(powerupID == 1)
                {
                    ship.SpeedBoostPowerUp();
                    StartCoroutine(ship.SpeedBoostPowerDown());
                }
                else
                {
                    ship.enableShield();
                }

            }

            
            //destroy the powerup
            Destroy(this.gameObject);
        }

    }
}
