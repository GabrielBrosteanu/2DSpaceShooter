using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldActivated = false;
    public int lives = 2;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private GameObject _shieldEffect;
    [SerializeField]
    private GameObject[] _engines;
    

    //fire rate is 0.25s
    //has the amount of time between firing passed?
    //Time.time(de cat timp a inceput jocul)

    private float _fireAllowed = 0.0f;
    private int hitCount = 0;

    [SerializeField]
    public float _speed = 4.0f;


    private UImanager _uimanager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;






    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();
       
        if(_uimanager != null)
        {
            _uimanager.updateImage(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.startSpawning();
        }

        _audioSource = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shooting();
        }
            
    }


    private void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");


        if(canSpeedBoost==true)
        {
            transform.Translate(UnityEngine.Vector3.right * (_speed*1.5f) * horizontalInput * Time.deltaTime);
            transform.Translate(UnityEngine.Vector3.up * (_speed*1.5f) * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(UnityEngine.Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(UnityEngine.Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new UnityEngine.Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.34f)
        {
            transform.position = new UnityEngine.Vector3(transform.position.x, -4.34f, 0);
        }
        else if (transform.position.x > 10.7f)
        {
            transform.position = new UnityEngine.Vector3(-10.7f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.7f)
        {
            transform.position = new UnityEngine.Vector3(10.7f, transform.position.y, 0);
        }
    }

    private void Shooting()
    {
     
            if (Time.time > _fireAllowed)
            {
            _audioSource.Play();
                    if(canTripleShot == true)
                    {
                         Instantiate(_tripleShotPrefab, transform.position,Quaternion.identity);
                        _fireAllowed = Time.time + _fireRate;
                    }

                    else
                    {
                         Instantiate(_laserPrefab, transform.position + new UnityEngine.Vector3(0, 0.98f, 0), UnityEngine.Quaternion.identity);
                        _fireAllowed = Time.time + _fireRate;
                    }
                    
            }
        }

    public void Damage()
    {
       


        if(shieldActivated == true)
        {
            shieldActivated = false;
            _shieldEffect.SetActive(false);
            return;
        }

        hitCount++;

        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

       
     


            lives--;
            _uimanager.updateImage(lives);

        if (lives<1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uimanager.showTitleScreen();
            Destroy(this.gameObject);
        }
        
    }

    public void TripleShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDown());
    }

    public void SpeedBoostPowerUp()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDown());
    }

    public void enableShield()
    {
        shieldActivated = true;
        _shieldEffect.SetActive(true);
    }

    public IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
   
}


