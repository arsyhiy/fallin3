using UnityEngine;


namespace Unity.Player
{
    public class PlayerWeaponsManager : MonoBehaviour
    {

        [Header("Weapons")]
        public Weapon[] weapons;


        public int currentWeapon = 0;


        private Weapon activeWeapon;

        PlayerInputHandler inputHandler;


        // void Start()
        // {
        //     EquipWeapon(currentWeapon);
        //     if(inputHandler == null)
        //     {
        //         Debug.LogError(
        //             "PlayerInputHandler not found!"
        //         );
        // }


        //     inputHandler = GetComponent<PlayerInputHandler>();
        // }
        // void Start()
        // {
        //     inputHandler = GetComponent<PlayerInputHandler>();

        //     if (inputHandler == null)
        //     {
        //         Debug.LogError(
        //             "PlayerInputHandler not found!"
        //         );
        //     }


        //     EquipWeapon(currentWeapon);
        // }

void Start()
{
    inputHandler = GetComponent<PlayerInputHandler>();

    if(inputHandler == null)
    {
        Debug.LogError(
            "PlayerInputHandler not found!"
        );

        return;
    }

    EquipWeapon(currentWeapon);
}


        void Update()
        {

            // стрельба
            if (inputHandler.GetFireInputHeld())
            {
                Debug.Log("INPUT OK");
                Shoot();
            }


            // выбор оружия
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EquipWeapon(0);
            }


            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipWeapon(1);
            }

        }



        void Shoot()
        {
            if (activeWeapon == null)
                return;


            
            Debug.Log("WEAPON MANAGER SHOOT");

            // передаем игрока как источник урона
            activeWeapon.Shoot(gameObject);

        }



        void EquipWeapon(int index)
        {

            if (index < 0 || index >= weapons.Length)
            {
                Debug.LogWarning(
                    "Weapon index doesn't exist: " + index
                );

                return;
            }



            // выключаем старое оружие
            if (activeWeapon != null)
            {
                activeWeapon.gameObject.SetActive(false);
            }



            // выбираем новое
            currentWeapon = index;

            activeWeapon = weapons[index];



            if (activeWeapon != null)
            {
                activeWeapon.gameObject.SetActive(true);


                Debug.Log(
                    "Equipped weapon: "
                    + activeWeapon.name
                );
            }

        }

    }

}