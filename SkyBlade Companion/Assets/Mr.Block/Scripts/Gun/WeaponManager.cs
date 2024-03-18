using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private float _weaponSwapDelay = 0.2f;

    private Coroutine _shuffleCoroutine;
    private static GameObject _currentActiveWeapon;

    private Flamethrower _flamethrower;

    private void Start()
    {
        _currentActiveWeapon = GetActiveWeapon();
        _flamethrower = GetFlamethrowerComponent();
    }

    private void Update()
    {
        if (IsMacheteRotating())
        {
            DisableActiveWeapon();
        }
        else
        {
            EnableActiveWeapon();
        }

        WeaponShuffle();
    }

    private void WeaponShuffle()
    {
        float scrollDelta = Input.mouseScrollDelta.y;

        if ((scrollDelta != 0) && !IsMacheteRotating() && (_flamethrower == null || !_flamethrower.IsOnCooldown))
        {
            if (_shuffleCoroutine != null)
            {
                StopCoroutine(_shuffleCoroutine);
            }

            _shuffleCoroutine = StartCoroutine(StartShuffleAfterDelay());
        }
    }

    private void SwapWeapons()
    {
        System.Random range = new System.Random();

        int n = _weapons.Length;

        while (n > 1)
        {
            n--;
            int k = range.Next(n + 1);

            if (k != n)
            {
                GameObject temp = _weapons[k];
                _weapons[k] = _weapons[n];
                _weapons[n] = temp;
            }
        }

        for (int i = 0; i < _weapons.Length; i++)
        {
            GameObject weapon = _weapons[i];

            if (weapon.activeSelf == false)
            {
                weapon.SetActive(true);

                _currentActiveWeapon = weapon;
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

    private IEnumerator StartShuffleAfterDelay()
    {
        yield return new WaitForSeconds(_weaponSwapDelay);

        SwapWeapons();
    }

    public void EnableActiveWeapon()
    {
        if (_currentActiveWeapon != null && !IsMacheteRotating())
        {
            _currentActiveWeapon.SetActive(true);
        }
    }

    public void DisableActiveWeapon()
    {
        if (_currentActiveWeapon != null && IsMacheteRotating())
        {
            _currentActiveWeapon.SetActive(false);
        }
    }

    private bool IsMacheteRotating()
    {
        MacheteController macheteController = GameObject.Find("Machete").GetComponent<MacheteController>();

        return macheteController != null && macheteController.IsRotating;
    }

    private GameObject GetActiveWeapon()
    {
        foreach (GameObject weapon in _weapons)
        {
            if (IsValidWeapon(weapon))
            {
                return weapon;
            }
        }

        return null;
    }

    private bool IsValidWeapon(GameObject weapon)
    {
        return weapon != null && weapon.activeSelf;
    }

    private Flamethrower GetFlamethrowerComponent()
    {
        foreach (GameObject weapon in _weapons)
        {
            Flamethrower flamethrowerComponent = weapon.GetComponent<Flamethrower>();
            if (flamethrowerComponent != null)
            {
                return flamethrowerComponent;
            }
        }
        return null;
    }
}
