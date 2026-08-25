using UnityEngine;
using TMPro;


public class WeaponHUD : MonoBehaviour
{

    public TextMeshProUGUI ammoText;

    public Weapon currentWeapon;


    void Update()
    {

        if(currentWeapon == null)
        {
            ammoText.text = "";
            return;
        }


        int ammo =
            currentWeapon.Ammo();

        int reserve =
            currentWeapon.ReserveAmmo();


        if(ammo <= 0)
        {
            ammoText.text =
                "RELOAD\n" +
                ammo + " / " + reserve;
        }
        else
        {
            ammoText.text =
                ammo + " / " + reserve;
        }

    }

}