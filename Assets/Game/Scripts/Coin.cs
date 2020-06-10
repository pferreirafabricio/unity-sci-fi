using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    //public Player player;

    [SerializeField]
    private AudioClip _getCoinSound;

    private UIManager _uiManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //player = GameObject.Find("Player").GetComponent<Player>();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                

                if (player != null)
                {
                    player.getCoin = true;
                    AudioSource.PlayClipAtPoint(_getCoinSound, transform.position, 0.5f);

                    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                    if (_uiManager != null)
                    {
                        _uiManager.coinImage.SetActive(true);
                    }

                    Destroy(this.gameObject, 0.5f);
                }
            }
        }
    }

}
