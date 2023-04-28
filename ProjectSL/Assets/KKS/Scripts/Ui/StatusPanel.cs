using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText; // 이름
    [SerializeField] private TMP_Text levelText; // 레벨
    [SerializeField] private TMP_Text vigorText; // 생명력
    [SerializeField] private TMP_Text attunementText; // 집중력
    [SerializeField] private TMP_Text enduranceText; // 지구력
    [SerializeField] private TMP_Text vitalityText; // 체력
    [SerializeField] private TMP_Text strengthText; // 근력
    [SerializeField] private TMP_Text dexterityText; // 기량
    [SerializeField] private TMP_Text hpText; // HP
    [SerializeField] private TMP_Text mpText; // MP
    [SerializeField] private TMP_Text stText; // ST

    //! 플레이어 스테이터스 정보 받아서 패널에 보여주는 함수
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
