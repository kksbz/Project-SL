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

    //! �÷��̾� �������ͽ� ���� �޾Ƽ� �гο� �����ִ� �Լ�
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
} // StatusPanel
