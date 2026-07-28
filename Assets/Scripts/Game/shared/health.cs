/*
Health.cs: система здоровья игрока и объектов

Отвечает за:
- получение урона
- лечение
- смерть
- события для UI, анимаций и эффектов
*/

using UnityEngine;
using UnityEngine.Events;


namespace Unity.Game
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField]
        private float maxHealth = 100f;

        [SerializeField]
        private float criticalHealthRatio = 0.25f;


        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;


        public float CurrentHealth { get; private set; }


        private bool isDead;


        public float MaxHealth => maxHealth;


        public float GetRatio()
        {
            return CurrentHealth / maxHealth;
        }


        public bool IsCritical()
        {
            return GetRatio() <= criticalHealthRatio;
        }


        void Awake()
        {
            CurrentHealth = maxHealth;
        }


        public void Heal(float amount)
        {
            if (isDead)
                return;


            float previousHealth = CurrentHealth;


            CurrentHealth += amount;

            CurrentHealth = Mathf.Clamp(
                CurrentHealth,
                0f,
                maxHealth
            );


            float healedAmount =
                CurrentHealth - previousHealth;


            if (healedAmount > 0)
            {
                OnHealed?.Invoke(healedAmount);
            }
        }



        public void TakeDamage(
            float damage,
            GameObject damageSource
        )
        {
            if (isDead)
                return;


            float previousHealth = CurrentHealth;


            CurrentHealth -= damage;

            CurrentHealth = Mathf.Clamp(
                CurrentHealth,
                0f,
                maxHealth
            );


            float damageAmount =
                previousHealth - CurrentHealth;


            if (damageAmount > 0)
            {
                OnDamaged?.Invoke(
                    damageAmount,
                    damageSource
                );
            }


            CheckDeath();
        }



        public void Kill()
        {
            if (isDead)
                return;


            CurrentHealth = 0f;


            OnDamaged?.Invoke(
                maxHealth,
                null
            );


            CheckDeath();
        }



        private void CheckDeath()
        {
            if (CurrentHealth <= 0 && !isDead)
            {
                isDead = true;

                OnDie?.Invoke();
            }
        }
    }
}
