using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("== Visual ==")]
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _weaponTypeText;

    #region References
    [Header("== References ==")]
    private WeaponSystem _weaponSystem;
    
    #endregion

    private void Start()
    {
        _weaponSystem = GetComponent<WeaponSystem>();
    }

    private void Update()
    {
        //_weaponIcon = _weaponSprite;
        _weaponTypeText.text = $"WeaponType: {_weaponSystem.WeaponType}";
        _ammoText.SetText(_weaponSystem.BulletsLeft + " / " + _weaponSystem.MagazineSize);
    }
}
