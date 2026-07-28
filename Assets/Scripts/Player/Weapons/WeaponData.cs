using UnityEngine;


[CreateAssetMenu(
    fileName = "New Weapon",
    menuName = "Weapons/Weapon Data"
)]
public class WeaponData : ScriptableObject
{

    [Header("General")]
    public string weaponName;


    [Header("Damage")]
    public float damage = 25f;
    public float range = 100f;


    [Header("Fire")]
    public float fireRate = 0.2f;


    [Header("Ammo")]
    public int magazineSize = 12;
    public int maxAmmo = 48;


    [Header("Reload")]
    public float reloadTime = 1.5f;


    [Header("Effects")]
    public AudioClip shootSound;
    public ParticleSystem muzzleFlash;
}