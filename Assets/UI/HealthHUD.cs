// /*
// HealthHUD.cs:
// управляет отображением здоровья игрока
// */
// 
// using UnityEngine;
// using UnityEngine.UI;
// using Unity.Game;
// 
// 
// namespace Unity.UI
// {
//     public class HealthHUD : MonoBehaviour
//     {
//         public Health playerHealth;
//         public Slider healthSlider;
// 
// 
//         void Start()
//         {
//             if(playerHealth == null)
//             {
//                 Debug.LogError("HealthHUD: Health is missing");
//                 return;
//             }
// 
// 
//             healthSlider.maxValue =
//                 playerHealth.MaxHealth;
// 
// 
//             healthSlider.value =
//                 playerHealth.CurrentHealth;
// 
// 
//             playerHealth.OnDamaged += UpdateHealth;
//             playerHealth.OnHealed += UpdateHealth;
//         }
// 
// 
// 
//         void UpdateHealth(float amount)
//         {
//             healthSlider.value =
//                 playerHealth.CurrentHealth;
//         }
// 
// 
// 
//         void OnDestroy()
//         {
//             if(playerHealth == null)
//                 return;
// 
// 
//             playerHealth.OnDamaged -= UpdateHealth;
//             playerHealth.OnHealed -= UpdateHealth;
//         }
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using Unity.Game;


namespace Unity.UI
{
    public class HealthHUD : MonoBehaviour
    {
        public Health playerHealth;
        public Slider healthSlider;


        void Start()
        {
            if (playerHealth == null)
            {
                Debug.LogError("HealthHUD: Health is missing");
                return;
            }


            healthSlider.maxValue = playerHealth.MaxHealth;
            healthSlider.value = playerHealth.CurrentHealth;


            playerHealth.OnDamaged += UpdateHealthDamage;
            playerHealth.OnHealed += UpdateHealthHeal;
        }



        void UpdateHealthDamage(float amount, GameObject source)
        {
            healthSlider.value =
                playerHealth.CurrentHealth;
        }



        void UpdateHealthHeal(float amount)
        {
            healthSlider.value =
                playerHealth.CurrentHealth;
        }



        void OnDestroy()
        {
            if(playerHealth == null)
                return;


            playerHealth.OnDamaged -= UpdateHealthDamage;
            playerHealth.OnHealed -= UpdateHealthHeal;
        }
    }
}
