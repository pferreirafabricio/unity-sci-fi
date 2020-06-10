using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (player.getCoin == true)
                    {
                        player.getCoin = false;
                        UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                        if (uiManager != null)
                        {
                            uiManager.coinImage.SetActive(false);
                        }

                        AudioSource _shopSuccessful = this.GetComponent<AudioSource>();
                        _shopSuccessful.Play();
                        player.EnableWeapon();
                    }
                    else
                    {
                        Debug.Log("Get out of here!");
                    }
                }
            }
        }
    }
}
