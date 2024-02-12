using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This is a generic health class that acts as the 'Subject' in our Observer pattern.
/// Other classes can watch for Damaged, Healed, and Killed events and respond appropriately.
/// We can also optionally send out the amount associated with being Damaged/Healed if we want, which
/// could be useful for UI systems, pop-up damage text, etc.
/// </summary>
namespace Examples.Observer
{
    public class Health : MonoBehaviour
    {
        public event Action<int> OnDamaged;
        public event Action<int> OnHealed;
        public event Action OnKilled;

        [SerializeField] int _startingHealth = 100;
        public int StartingHealth //This is a Property
        {
            get { return _startingHealth; }
        }

        [SerializeField] int _maxHealth = 100;
        public int MaxHealth
        {
            get { return _maxHealth; }
        }
       

        int _currentHealth;
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                // ensure we can't go above our max health
                if(value > _maxHealth)
                {
                    value = _maxHealth;
                }
                _currentHealth = value;
            }
        }

        private void Awake()
        {
            CurrentHealth = _startingHealth;
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            OnHealed?.Invoke(amount);
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            OnDamaged?.Invoke(amount);

            if(CurrentHealth <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            OnKilled?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

