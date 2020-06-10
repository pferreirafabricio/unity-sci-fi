using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _Speed = 3.5f;
    private float _Gravity = 9.81f;

    [SerializeField]
    private GameObject _Muzzle_Flash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _shootSound;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private UIManager _uiManager;

    public bool getCoin = false;

    [SerializeField]
    private GameObject _weapon;

	// Use this for initialization
	void Start ()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentAmmo == 0)
        {
           
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(reloadAmmo());
            }
        }

        if (Input.GetMouseButton(0) && (currentAmmo > 0))
        {
            Shoot();
        }
        else
        {
            
            _Muzzle_Flash.SetActive(false);
            _shootSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        CalculateMoviment();
	}

    void Shoot()
    {
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);

        if (_shootSound.isPlaying == false)
        {
            _shootSound.Play();
        }

        _Muzzle_Flash.SetActive(true);

        Ray rayOrigin = GameObject.Find("Main Camera").GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            Destructable crate = hitInfo.transform.GetComponent<Destructable>();

            if (crate != null)
            {
                crate.DestroyCrate();
            }

            /*if (hitInfo.transform.tag == "crate")
            {
                
            }*/
            
        }
    }

    void CalculateMoviment()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");


        Vector3 Direction = new Vector3(HorizontalInput, 0, VerticalInput);
        Vector3 Velocity = Direction * _Speed;
        Velocity.y -= _Gravity;

        Velocity = transform.transform.TransformDirection(Velocity);

        _controller.Move(Velocity * Time.deltaTime);
    }

    IEnumerator reloadAmmo()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
