using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] PlayerCharacter _playerCharacter;
    [SerializeField] ItemData _itemData;
    [SerializeField] Collider _damageCollider;
    public float _currentWeaponDamage;
    [SerializeField] PlayerStatus _ownerStats;
    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        _damageCollider.gameObject.SetActive(true);
        _damageCollider.isTrigger = true;
        _damageCollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        _damageCollider.enabled = true;
    }
    public void DisableDamageCollider()
    {
        _damageCollider.enabled = false;
    }
    public void WeaponInit(PlayerCharacter playerCharacter, PlayerStatus playerStatus, ItemData weaponData = null )
    {
        _playerCharacter = playerCharacter;
        _itemData = weaponData;
        _ownerStats = playerStatus;

        SetDamage();
    }
    public void SetDamage(float damageMultiplier = 1f)
    {
        float statDamage = _ownerStats.AppliedStrength;
        float weaponDamage = 0f;
        if (_itemData != null)
            weaponDamage = _itemData.damage * _playerCharacter.CombatStat.DamageMultiplier;
        else
            weaponDamage = 0f;
        _currentWeaponDamage = (statDamage + weaponDamage) * damageMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        GData.IDamageable damageable = other.gameObject.GetComponent<GData.IDamageable>();
        if (damageable != null) 
        {
            if(other.tag != "Player")
                damageable.TakeDamage(_playerCharacter.gameObject, _currentWeaponDamage);
        }
    }
}
