﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script uses the Observer pattern to start watching a
/// Health object. This example shows how you can subscribe/unsubscribe
/// whenever you need it, without using OnEnable or OnDisable.
/// Technically we start observing in Awake, but we can call the
/// 'StartObservingHealth' method whenever we want. Think of it as
/// 'AcquireTarget' and 'LoseTarget' if we wanted to put it in a GameContext
/// </summary>
namespace Examples.Observer
{
    public class HitTextPopup : MonoBehaviour
    {
        [SerializeField] Health _health = null;
        [SerializeField] Text _textPopupUI = null;

        [SerializeField] string _hitText = "Hit!";
        [SerializeField] string _killText = "KILL";
        [SerializeField] float _textPopupDuration = 1;

        Coroutine _popupRoutine = null;


        private void OnEnable()
        {
            // notify when target is damaged
            _health.OnDamaged += DisplayHitText;
            _health.OnKilled += DisplayKilledText;
        }
        

        public void StopObservingHealth()
        {
            // no longer watch target
            _health.OnDamaged -= DisplayHitText;
            _health.OnKilled -= DisplayKilledText;
        }

        void DisplayHitText(int damaged)
        {
            string hitText = _hitText + " " + damaged.ToString();

            if (_popupRoutine != null)
                StopCoroutine(_popupRoutine);
            _popupRoutine = StartCoroutine(TextPopup(hitText));
        }

        IEnumerator TextPopup(string textToShow)
        {
            _textPopupUI.text = textToShow;
            yield return new WaitForSeconds(_textPopupDuration);
            _textPopupUI.text = string.Empty;
        }

        void DisplayKilledText()
        {
            if (_popupRoutine != null)
                StopCoroutine(_popupRoutine);
            _popupRoutine = StartCoroutine(TextPopup(_killText));
            StopObservingHealth();
        }
    }
}

