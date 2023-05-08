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
    [SerializeField] private List<TMP_Text> rightWeaponList; // 오른손무기 리스트
    [SerializeField] private List<TMP_Text> leftWeaponList; // 왼손무기 리스트
    [SerializeField] private TMP_Text defenceText; // 방어력
    [SerializeField] private TMP_Text damageMultiplierText; // 공격력 배율
    [SerializeField] private TMP_Text possessionSoul; // 보유소울

    //! 인벤토리 스테이터스 패널 갱신하는 함수
    public void ShowPlayerStatusPanel(StatusSaveData _playerStatus)
    {
        nameText.text = _playerStatus._playerStatusData.Name;
        levelText.text = _playerStatus._playerStatusData.Level.ToString();
        vigorText.text = _playerStatus._playerStatusData.Vigor.ToString();
        attunementText.text = _playerStatus._playerStatusData.Attunement.ToString();
        enduranceText.text = _playerStatus._playerStatusData.Endurance.ToString();
        vitalityText.text = _playerStatus._playerStatusData.Vitality.ToString();
        strengthText.text = _playerStatus._playerStatusData.Strength.ToString();
        dexterityText.text = _playerStatus._playerStatusData.Dexterity.ToString();

        hpText.text = $"{_playerStatus._healthSystemData.HP} / {_playerStatus._healthSystemData.MaxHP}";
        mpText.text = $"{_playerStatus._healthSystemData.MP} / {_playerStatus._healthSystemData.MaxMP}";
        stText.text = $"{_playerStatus._healthSystemData.SP} / {_playerStatus._healthSystemData.MaxSP}";
    } // ShowPlayerStatusPanel

    //! 스테이터스 패널 갱신하는 함수
    public void ShowPlayerStatusPanel(StatusSaveData _playerStatus, List<WeaponSlot> _weaponSlots)
    {
        nameText.text = _playerStatus._playerStatusData.Name;
        levelText.text = _playerStatus._playerStatusData.Level.ToString();
        possessionSoul.text = Inventory.Instance.Soul.ToString();
        vigorText.text = _playerStatus._playerStatusData.Vigor.ToString();
        attunementText.text = _playerStatus._playerStatusData.Attunement.ToString();
        enduranceText.text = _playerStatus._playerStatusData.Endurance.ToString();
        vitalityText.text = _playerStatus._playerStatusData.Vitality.ToString();
        strengthText.text = _playerStatus._playerStatusData.Strength.ToString();
        dexterityText.text = _playerStatus._playerStatusData.Dexterity.ToString();

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
                    // 오른손무기 데미지 = 플레이어 기본데미지 + 무기데미지
                    rightWeaponList[i].text = (GameManager.Instance.player.CombatStat.AttackPoint + _weaponSlots[i].Item.damage).ToString();
                }
                else
                {
                    // 왼손무기 데미지 = 플레이어 기본데미지 + 무기데미지
                    leftWeaponList[i - 3].text = (GameManager.Instance.player.CombatStat.AttackPoint + _weaponSlots[i].Item.damage).ToString();
                }
            }
            else
            {
                // 무기가 없으면 기본데미지 표시
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
