// // using UnityEngine;
// // using Unity.Game;
// // using Unity.Player;

// // public class Weapon : MonoBehaviour
// // {
// //     [Header("Weapon")]
// //     public float damage = 25f;

// //     public float fireRate = 0.2f;

// //     public float range = 100f;


// //     private float nextShot;


// //     public void Shoot(GameObject owner)
// //     {
// //         if (Time.time < nextShot)
// //             return;


// //         nextShot = Time.time + fireRate;



// //         Ray ray = Camera.main.ScreenPointToRay(
// //             new Vector3(
// //                 Screen.width / 2,
// //                 Screen.height / 2
// //             )
// //         );


// //         if (Physics.Raycast(
// //             ray,
// //             out RaycastHit hit,
// //             range))
// //         {

// //             Debug.Log(
// //                 "Hit: " + hit.collider.name
// //             );


// //             Health health =
// //                 hit.collider.GetComponentInParent<Health>();


// //             if (health != null)
// //             {
// //                 health.TakeDamage(
// //                     damage,
// //                     owner
// //                 );
// //             }
// //         }
// //     }
// // }

// using System.Collections;
// using UnityEngine;
// using Unity.Game;


// public class Weapon : MonoBehaviour
// {
//     [Header("Weapon Stats")]
//     public float damage = 25f;
//     public float fireRate = 0.2f;
//     public float range = 100f;


//     [Header("Ammo")]
//     public int magazineSize = 12;
//     public int reserveAmmo = 48;

//     private int currentAmmo;


//     [Header("Effects")]
//     public AudioSource audioSource;
//     public AudioClip shootSound;

//     public ParticleSystem muzzleFlash;


//     [Header("Reload")]
//     public float reloadTime = 1.5f;

//     private bool isReloading;


//     private float nextShot;


//     void Start()
//     {
//         currentAmmo = magazineSize;
//     }


//     public void Shoot(GameObject owner)
//     {
//         if (isReloading)
//             return;


//         if (Time.time < nextShot)
//             return;


//         if (currentAmmo <= 0)
//         {
//             Debug.Log("Need reload");
//             return;
//         }


//         nextShot = Time.time + fireRate;

//         currentAmmo--;


//         PlayShootEffects();


//         Ray ray = Camera.main.ScreenPointToRay(
//             new Vector3(
//                 Screen.width / 2,
//                 Screen.height / 2
//             )
//         );


//         if (Physics.Raycast(
//             ray,
//             out RaycastHit hit,
//             range))
//         {

//             Debug.Log(
//                 "Hit: " + hit.collider.name
//             );


//             Health health =
//                 hit.collider.GetComponentInParent<Health>();


//             if (health != null)
//             {
//                 health.TakeDamage(
//                     damage,
//                     owner
//                 );
//             }
//         }
//     }



//     public void Reload()
//     {
//         if (isReloading)
//             return;


//         if (currentAmmo == magazineSize)
//             return;


//         if (reserveAmmo <= 0)
//             return;


//         StartCoroutine(ReloadRoutine());
//     }



//     IEnumerator ReloadRoutine()
//     {
//         isReloading = true;


//         Debug.Log("Reloading...");


//         yield return new WaitForSeconds(
//             reloadTime
//         );


//         int neededAmmo =
//             magazineSize - currentAmmo;


//         int ammoToReload =
//             Mathf.Min(
//                 neededAmmo,
//                 reserveAmmo
//             );


//         currentAmmo += ammoToReload;

//         reserveAmmo -= ammoToReload;


//         isReloading = false;


//         Debug.Log("Reload complete");
//     }



//     void PlayShootEffects()
//     {
//         if (audioSource != null &&
//             shootSound != null)
//         {
//             audioSource.PlayOneShot(
//                 shootSound
//             );
//         }


//         if (muzzleFlash != null)
//         {
//             muzzleFlash.Play();
//         }
//     }



//     public int GetCurrentAmmo()
//     {
//         return currentAmmo;
//     }


//     public int GetReserveAmmo()
//     {
//         return reserveAmmo;
//     }
// }

using System.Collections;
using UnityEngine;
using Unity.Game;


public class Weapon : MonoBehaviour
{

    public WeaponData data;


    private int currentAmmo;
    private int reserveAmmo;


    private float nextShot;

    private bool reloading;



    void Start()
    {
        currentAmmo = data.magazineSize;
        reserveAmmo = data.maxAmmo;
    }



    public void Shoot(GameObject owner)
    {

    Debug.Log("REAL WEAPON SHOOT");
        if(reloading)
            return;


        if(Time.time < nextShot)
            return;


        if(currentAmmo <= 0)
        {
            Debug.Log("Reload needed");
            return;
        }


        nextShot =
            Time.time + data.fireRate;


        currentAmmo--;
        Debug.Log(
       "Ammo: " + currentAmmo
    );



        PlayEffects();



        Ray ray =
            Camera.main.ScreenPointToRay(
                new Vector3(
                    Screen.width / 2,
                    Screen.height / 2
                )
            );


        if(Physics.Raycast(
            ray,
            out RaycastHit hit,
            data.range))
        {
                Debug.Log(
        "Hit: " + hit.collider.name
    );


            Health health =
                hit.collider
                .GetComponentInParent<Health>();


            if(health != null)
            {
                health.TakeDamage(
                    data.damage,
                    owner
                );
            }

        }

    }



    public void Reload()
    {

        if(reloading)
            return;


        StartCoroutine(
            ReloadRoutine()
        );

    }



    IEnumerator ReloadRoutine()
    {

        reloading = true;


        yield return new WaitForSeconds(
            data.reloadTime
        );


        int amount =
            data.magazineSize - currentAmmo;


        amount =
            Mathf.Min(
                amount,
                reserveAmmo
            );


        currentAmmo += amount;
        reserveAmmo -= amount;


        reloading = false;

    }



    void PlayEffects()
    {

        if(data.shootSound)
        {
            AudioSource.PlayClipAtPoint(
                data.shootSound,
                transform.position
            );
        }


        if(data.muzzleFlash)
        {
            data.muzzleFlash.Play();
        }

    }


    public int Ammo()
    {
        return currentAmmo;
    }


    public int ReserveAmmo()
    {
        return reserveAmmo;
    }

}