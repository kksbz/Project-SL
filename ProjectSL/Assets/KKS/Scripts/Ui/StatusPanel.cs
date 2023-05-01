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
    [SerializeField] private TMP_Text possessionSoul; // �����ҿ�

    //! �κ��丮 �������ͽ� �г� �����ϴ� �Լ�
    public void ShowPlayerStatusPanel(PlayerStatus _playerStatus, HealthSystem _healthSystem)
    {
        nameText.text = _playerStatus.Name;
        levelText.text = _playerStatus.Level.ToString();
        vigorText.text = _playerStatus.Vigor.ToString();
        attunementText.text = _playerStatus.Attunement.ToString();
        enduranceText.text = _playerStatus.Endurance.ToString();
        vitalityText.text = _playerStatus.Vitality.ToString();
        strengthText.text = _playerStatus.Strength.ToString();
        dexterityText.text = _playerStatus.Dexterity.ToString();

        hpText.text = $"{_healthSystem.HP} / {_healthSystem.MaxHP}";
        mpText.text = $"{_healthSystem.MP} / {_healthSystem.MaxMP}";
        stText.text = $"{_healthSystem.SP} / {_healthSystem.MaxSP}";
    } // ShowPlayerStatusPanel

    //! �������ͽ� �г� �����ϴ� �Լ�
    public void ShowPlayerStatusPanel(PlayerStatus _playerStatus, HealthSystem _healthSystem, List<WeaponSlot> _rightWeaponList, List<WeaponSlot> _leftWeaponList)
    {
        nameText.text = _playerStatus.Name;
        levelText.text = _playerStatus.Level.ToString();
        possessionSoul.text = Inventory.Instance.Soul.ToString();
        vigorText.text = _playerStatus.Vigor.ToString();
        attunementText.text = _playerStatus.Attunement.ToString();
        enduranceText.text = _playerStatus.Endurance.ToString();
        vitalityText.text = _playerStatus.Vitality.ToString();
        strengthText.text = _playerStatus.Strength.ToString();
        dexterityText.text = _playerStatus.Dexterity.ToString();

        hpText.text = $"{_healthSystem.HP} / {_healthSystem.MaxHP}";
        mpText.text = $"{_healthSystem.MP} / {_healthSystem.MaxMP}";
        stText.text = $"{_healthSystem.SP} / {_healthSystem.MaxSP}";

        for (int i = 0; i < _rightWeaponList.Count; i++)
        {
            if (_rightWeaponList[i].Item != null)
            {
                rightWeaponList[i].text = _rightWeaponList[i].Item.damage.ToString();
            }
            else
            {
                rightWeaponList[i].text = "0";
            }
        }

        for (int i = 0; i < _leftWeaponList.Count; i++)
        {
            if (_leftWeaponList[i].Item != null)
            {
                leftWeaponList[i].text = _leftWeaponList[i].Item.damage.ToString();
            }
            else
            {
                leftWeaponList[i].text = "0";
            }
        }
    } // ShowPlayerStatusPanel
} // StatusPanel
