using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;

    public GameObject coinImage;

    public void UpdateAmmo(int Count)
    {
        _ammoText.text = "Ammo: " + Count;
    }

}
