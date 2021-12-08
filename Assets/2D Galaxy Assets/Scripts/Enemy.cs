using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private AudioClip _clip;

    private UImanager _uimanager;
    private AudioSource _audioSource;
  

   

    // Start is called before the first frame update
    void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -4.9f)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D objToCollideWith)
    {

       
        if (objToCollideWith.tag == "Laser")
        {
            if(objToCollideWith.transform.parent != null)
            {
                Destroy(objToCollideWith.transform.parent.gameObject);
            }
           
            Destroy(objToCollideWith.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uimanager.updateScore();
            Destroy(this.gameObject);

        }
            else if(objToCollideWith.tag == "Player")
        {
            Player player = objToCollideWith.GetComponent<Player>();

            if(player!=null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

         
        }
        
    }
}
