using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText; // �̸�
    [SerializeField] private TMP_Text levelText; // ����
    [SerializeField] private TMP_Text vigorText; // �����
    [SerializeField] private TMP_Text attunementText; // ���߷�
    [SerializeField] private TMP_Text enduranceText; // ������
    [SerializeField] private TMP_Text vitalityText; // ü��
    [SerializeField] private TMP_Text strengthText; // �ٷ�
    [SerializeField] private TMP_Text dexterityText; // �ⷮ
    [SerializeField] private TMP_Text hpText; // HP
    [SerializeField] private TMP_Text mpText; // MP
    [SerializeField] private TMP_Text stText; // ST
    [SerializeField] private List<TMP_Text> rightWeaponList; // �����չ��� ����Ʈ
    [SerializeField] private List<TMP_Text> leftWeaponList; // �޼չ��� ����Ʈ
    [SerializeField] private TMP_Text defenceText; // ����
    [SerializeField] private TMP_Text damageMultiplierText; // ���ݷ� ����
    [SerializeField] private TMP_Text possessionSoul; // �����ҿ�

    //! �κ��丮 �������ͽ� �г� �����ϴ� �Լ�
    public void ShowPlayerStatusPanel(StatusSaveData _playerStatus)
    {
        nameText.text = _playerStatus._playerStatusData.Name;
        levelText.text = _playerStatus._playerStatusData.Level.ToString();
        vigorText.text = _playerStatus._playerStatusData.AppliedVigor.ToString();
        attunementText.text = _playerStatus._playerStatusData.AppliedAttunement.ToString();
        enduranceText.text = _playerStatus._playerStatusData.AppliedEndurance.ToString();
        vitalityText.text = _playerStatus._playerStatusData.AppliedVitality.ToString();
        strengthText.text = _playerStatus._playerStatusData.AppliedStrength.ToString();
        dexterityText.text = _playerStatus._playerStatusData.AppliedDexterity.ToString();

        hpText.text = $"{_playerStatus._healthSystemData.HP} / {_playerStatus._healthSystemData.MaxHP}";
        mpText.text = $"{_playerStatus._healthSystemData.MP} / {_playerStatus._healthSystemData.MaxMP}";
        stText.text = $"{_playerStatus._healthSystemData.SP} / {_playerStatus._healthSystemData.MaxSP}";
    } // ShowPlayerStatusPanel

    //! �������ͽ� �г� �����ϴ� �Լ�
    public void ShowPlayerStatusPanel(StatusSaveData _playerStatus, List<WeaponSlot> _weaponSlots)
    {
        nameText.text = _playerStatus._playerStatusData.Name;
        levelText.text = _playerStatus._playerStatusData.Level.ToString();
        possessionSoul.text = Inventory.Instance.Soul.ToString();
        vigorText.text = _playerStatus._playerStatusData.AppliedVigor.ToString();
        attunementText.text = _playerStatus._playerStatusData.AppliedAttunement.ToString();
        enduranceText.text = _playerStatus._playerStatusData.AppliedEndurance.ToString();
        vitalityText.text = _playerStatus._playerStatusData.AppliedVitality.ToString();
        strengthText.text = _playerStatus._playerStatusData.AppliedStrength.ToString();
        dexterityText.text = _playerStatus._playerStatusData.AppliedDexterity.ToString();

        hpText.text = $"{_playerStatus._healthSystemData.HP} / {_playerStatus._healthSystemData.MaxHP}";
        mpText.text = $"{_playerStatus._healthSystemData.MP} / {_playerStatus._healthSystemData.MaxMP}";
        stText.text = $"{_playerStatus._healthSystemData.SP} / {_playerStatus._healthSystemData.MaxSP}";
        defenceText.text = GameManager.Instance.player.CombatStat.DefensePoint.ToString();
        damageMultiplierText.text = GameManager.Instance.player.CombatStat.DamageMultiplier.ToString();

        for (int i = 0; i < _weaponSlots.Count; i++)
        {
            if (_weaponSlots[i].Item != null)
            {
                if (i < 3)
                {
                    // �����չ��� ������ = �÷��̾� �⺻������ + ���ⵥ����
                    rightWeaponList[i].text = (GameManager.Instance.player.CombatStat.AttackPoint + _weaponSlots[i].Item.damage).ToString();
                }
                else
                {
                    // �޼չ��� ������ = �÷��̾� �⺻������ + ���ⵥ����
                    leftWeaponList[i - 3].text = (GameManager.Instance.player.CombatStat.AttackPoint + _weaponSlots[i].Item.damage).ToString();
                }
            }
            else
            {
                // ���Ⱑ ������ �⺻������ ǥ��
                if (i < 3)
                {
                    rightWeaponList[i].text = GameManager.Instance.player.CombatStat.AttackPoint.ToString();
                }
                else
                {
                    leftWeaponList[i - 3].text = GameManager.Instance.player.CombatStat.AttackPoint.ToString();
                }
            }
        }
    } // ShowPlayerStatusPanel
} // StatusPanel
