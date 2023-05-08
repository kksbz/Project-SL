using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [Header("레벨업 버튼 모음")]
    [SerializeField] private Button plusBt; // 플러스 버튼
    [SerializeField] private Button minusBt; // 마이너스 버튼
    [SerializeField] private Button decisionBt; // 결정 버튼
    [SerializeField] private Button cancelBt; // 취소 버튼
    [SerializeField] private Button vigorBt; // 생명력 버튼
    [SerializeField] private Button attunementBt; // 집중력 버튼
    [SerializeField] private Button enduranceBt; // 지구력 버튼
    [SerializeField] private Button vitalityBt; // 체력 버튼
    [SerializeField] private Button strengthBt; // 근력 버튼
    [SerializeField] private Button dexterityBt; // 기량 버튼

    [Header("레벨업 텍스트 모음")]
    [SerializeField] private TMP_Text playerNameText; // 플레이어 이름 텍스트
    [SerializeField] private TMP_Text levelText; // 현재 레벨 텍스트
    [SerializeField] private TMP_Text resultLevelText; // 결과 레벨 텍스트
    [SerializeField] private TMP_Text soulText; // 현재 보유소울 텍스트
    [SerializeField] private TMP_Text resultSoulText; // 결과 보유소울 텍스트
    [SerializeField] private TMP_Text wantSoulText; // 필요한 소울 텍스트
    [SerializeField] private TMP_Text[] statusTexts; // 현재 스텟 텍스트 배열
    [SerializeField] private TMP_Text[] resultStatusTexts; // 결과 스텟 텍스트 배열
    [SerializeField] private Image[] statusImages; // 스텟 이미지 배열

    [Header("능력치 텍스트 모음")]
    [SerializeField] private TMP_Text[] abilityTexts; // 능력치 텍스트 배열
    [SerializeField] private TMP_Text[] resultAbilityTexts; // 결과 능력치 텍스트 배열

    [SerializeField] private GameObject warningPanel; // 경고창
    [SerializeField] private TMP_Text warningText; // 경고창 텍스트
    [SerializeField] private GameObject selectPanel; // NPC대화 선택 패널
    [SerializeField] private GameObject levelUpPanel; // 레벨업 패널
    [SerializeField] private GameObject potionUpPanel; // 포션강화 패널
    private string pWarningMessage = "레벨업에 필요한 소울이 부족합니다";
    private string mWarningMessage = "현재 레벨 미만으로 감소할수없습니다";
    private int[] playerStatus; // 플레이어 현재스텟 배열
    private int selectStatusNum; // 선택한 스텟 번호
    private int increaseNum = 0; // +, - 증감숫자
    private int sumWantSoul = 0; // 필요한 소울 합계
    private bool hasSelectBt = false; // 레벨업할 스텟을 선택한 상태 체크
    void Start()
    {
        // 플러스 버튼
        plusBt.onClick.AddListener(() =>
        {
            PlusBtClick();
        });

        // 마이너스 버튼
        minusBt.onClick.AddListener(() =>
        {
            MinusBtClick();
        });

        // 결정 버튼
        decisionBt.onClick.AddListener(() =>
        {
            DecisionBtClick();
        });

        // 취소 버튼
        cancelBt.onClick.AddListener(() =>
        {
            if (selectStatusNum < 0)
            {
                return;
            }
            // 패널 초기화
            statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
            selectStatusNum = -1;
            GetPlayerStatus();
            GetAbilityStatus();
            increaseNum = 0;
            sumWantSoul = 0;
            hasSelectBt = false;
        });

        // 생명력 버튼
        vigorBt.onClick.AddListener(() =>
        {
            SelectStatus(0);
        });

        // 집중력 버튼
        attunementBt.onClick.AddListener(() =>
        {
            SelectStatus(1);
        });

        // 지구력 버튼
        enduranceBt.onClick.AddListener(() =>
        {
            SelectStatus(2);
        });

        // 체력 버튼
        vitalityBt.onClick.AddListener(() =>
        {
            SelectStatus(3);
        });

        // 근력 버튼
        strengthBt.onClick.AddListener(() =>
        {
            SelectStatus(4);
        });

        // 기량 버튼
        dexterityBt.onClick.AddListener(() =>
        {
            SelectStatus(5);
        });
    } // Start

    private void OnEnable()
    {
        selectStatusNum = -1;
        // 활성화될 때 플레이어의 현재스텟 가져옴
        GetPlayerStatus();
        // 능력치 패널 텍스트 할당
        GetAbilityStatus();
    } // OnEnable

    private void OnDisable()
    {
        GameManager.Instance.player.StateMachine.ResetInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        potionUpPanel.SetActive(false);
        levelUpPanel.SetActive(false);
        selectPanel.SetActive(true);
        gameObject.SetActive(false);
    } // OnDisable

    private void Update()
    {
        if (warningPanel.activeInHierarchy == true)
        {
            if (Input.anyKeyDown)
            {
                warningPanel.SetActive(false);
            }
        }
    } // Update

    private void GetPlayerStatus()
    {
        playerStatus = new int[7];
        PlayerStatus pStatus = GameManager.Instance.player.GetPlayerData()._playerStatusData;
        playerStatus[0] = pStatus.Vigor;
        playerStatus[1] = pStatus.Attunement;
        playerStatus[2] = pStatus.Endurance;
        playerStatus[3] = pStatus.Vitality;
        playerStatus[4] = pStatus.Strength;
        playerStatus[5] = pStatus.Dexterity;
        playerStatus[6] = pStatus.Level;

        // 패널 텍스트 표시 초기화
        playerNameText.text = pStatus.Name;
        levelText.text = playerStatus[6].ToString();
        resultLevelText.text = playerStatus[6].ToString();
        soulText.text = Inventory.Instance.Soul.ToString();
        resultSoulText.text = Inventory.Instance.Soul.ToString();
        wantSoulText.text = "0";
        // 스텟 텍스트 표시 초기화
        for (int i = 0; i < statusTexts.Length; i++)
        {
            statusTexts[i].text = playerStatus[i].ToString();
            resultStatusTexts[i].text = playerStatus[i].ToString();
        }
    } // GetPlayerStatus

    private void SelectStatus(int _num)
    {
        if (hasSelectBt == true)
        {
            return;
        }
        // 스텟버튼 선택이펙트 알파값 초기화
        foreach (var image in statusImages)
        {
            image.color = new Color(1, 1, 1, 0);
        }
        selectStatusNum = _num;
        // 선택한 스텟의 선택이펙트 보여주기
        statusImages[selectStatusNum].color = new Color(1, 1, 1, 1);
    } // SelectStatus

    //! 플러스 버튼 클릭기능
    private void PlusBtClick()
    {
        if (selectStatusNum < 0)
        {
            return;
        }
        hasSelectBt = true;
        increaseNum += 1;
        // 다음 레벨업에 필요한 경험치량을 경험치데이터테이블에서 가져옴
        int value = 0;
        DataManager.Instance.experienceDatas.TryGetValue(((playerStatus[6] + increaseNum) - 1), out value);
        sumWantSoul += value;
        if (sumWantSoul > Inventory.Instance.Soul)
        {
            sumWantSoul -= value;
            increaseNum -= 1;
            // 경고창 팝업
            warningPanel.SetActive(true);
            warningText.text = pWarningMessage;
            return;
        }
        wantSoulText.text = sumWantSoul.ToString();
        resultSoulText.text = (Inventory.Instance.Soul - sumWantSoul).ToString();

        levelText.text = playerStatus[6].ToString();
        resultLevelText.text = (playerStatus[6] + increaseNum).ToString();
        soulText.text = Inventory.Instance.Soul.ToString();

        statusTexts[selectStatusNum].text = playerStatus[selectStatusNum].ToString();
        resultStatusTexts[selectStatusNum].text = (playerStatus[selectStatusNum] + increaseNum).ToString();
        GetAbilityStatus();
    } // PlusBtClick

    //! 마이너스 버튼 클릭기능
    private void MinusBtClick()
    {
        if (selectStatusNum < 0)
        {
            return;
        }
        if (increaseNum == 0)
        {
            // 경고창 팝업
            warningPanel.SetActive(true);
            warningText.text = mWarningMessage;
            return;
        }
        int level = playerStatus[6];
        increaseNum -= 1;
        levelText.text = level.ToString();

        resultLevelText.text = (level + increaseNum).ToString();
        soulText.text = Inventory.Instance.Soul.ToString();

        // 다음 레벨업에 필요한 경험치량을 경험치데이터테이블에서 가져옴
        int value = 0;
        DataManager.Instance.experienceDatas.TryGetValue((level + increaseNum), out value);
        sumWantSoul -= value;
        wantSoulText.text = sumWantSoul.ToString();
        resultSoulText.text = (Inventory.Instance.Soul - sumWantSoul).ToString();

        statusTexts[selectStatusNum].text = playerStatus[selectStatusNum].ToString();
        resultStatusTexts[selectStatusNum].text = (playerStatus[selectStatusNum] + increaseNum).ToString();
        GetAbilityStatus();
    } // MinusBtClick

    //! 결정 버튼 클릭기능
    private void DecisionBtClick()
    {
        // 선택한 스텟 레벨업 적용
        playerStatus[6] = playerStatus[6] + increaseNum;
        playerStatus[selectStatusNum] = playerStatus[selectStatusNum] + increaseNum;
        UiManager.Instance.soulBag.GetSoul(-sumWantSoul);

        StatusSaveData pStatus = GameManager.Instance.player.GetPlayerData();
        pStatus._playerStatusData.Vigor = playerStatus[0];
        pStatus._playerStatusData.Attunement = playerStatus[1];
        pStatus._playerStatusData.Endurance = playerStatus[2];
        pStatus._playerStatusData.Vitality = playerStatus[3];
        pStatus._playerStatusData.Strength = playerStatus[4];
        pStatus._playerStatusData.Dexterity = playerStatus[5];
        pStatus._playerStatusData.Level = playerStatus[6];
        // 레벨업된 스텟들의 정보를 플레이어 데이터에 저장
        GameManager.Instance.player.LoadPlayerData(pStatus);

        // 패널 초기화
        statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
        selectStatusNum = -1;
        GetPlayerStatus();
        GetAbilityStatus();
        increaseNum = 0;
        sumWantSoul = 0;
        hasSelectBt = false;

        // 레벨업 확정하면 자동저장 슬롯에 플레이어 데이터 저장
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
    } // DecisionBtClick

    //! 능력치 패널 텍스트 할당하는 함수
    private void GetAbilityStatus()
    {
        PlayerStatus pStatus = GameManager.Instance.player.GetPlayerData()._playerStatusData;
        if (selectStatusNum == 5)
        {
            // 공격력 배율 텍스트 갱신
            abilityTexts[10].text = GameManager.Instance.player.CombatStat.DamageMultiplier.ToString();
            resultAbilityTexts[10].text = DataManager.Instance.statusLevelData[pStatus.Dexterity + increaseNum].damageMultiplier.ToString();
        }
        else if (selectStatusNum == 4)
        {
            // 오른손, 왼손 공격력 텍스트 갱신
            for (int i = 4; i < abilityTexts.Length - 1; i++)
            {
                if (Inventory.Instance.weaponSlotList[i - 4].Item != null)
                {
                    abilityTexts[i].text = (GameManager.Instance.player.CombatStat.AttackPoint + Inventory.Instance.weaponSlotList[i - 4].Item.damage).ToString();
                    resultAbilityTexts[i].text = (DataManager.Instance.statusLevelData[pStatus.Strength + increaseNum].damage + Inventory.Instance.weaponSlotList[i - 4].Item.damage).ToString();
                }
                else
                {
                    abilityTexts[i].text = GameManager.Instance.player.CombatStat.AttackPoint.ToString();
                    resultAbilityTexts[i].text = DataManager.Instance.statusLevelData[pStatus.Strength + increaseNum].damage.ToString();
                }
            }
        }
        else if (selectStatusNum == 3)
        {
            // 방어력 텍스트 갱신
            abilityTexts[selectStatusNum].text = GameManager.Instance.player.CombatStat.DefensePoint.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vitality + increaseNum].defense.ToString();
        }
        else if (selectStatusNum == 2)
        {
            // 스테미너 텍스트 갱신
            abilityTexts[selectStatusNum].text = GameManager.Instance.player.HealthSys.MaxSP.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Endurance + increaseNum].st.ToString();
        }
        else if (selectStatusNum == 1)
        {
            // MP 텍스트 갱신
            abilityTexts[selectStatusNum].text = GameManager.Instance.player.HealthSys.MaxMP.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Attunement + increaseNum].mp.ToString();
        }
        else if (selectStatusNum == 0)
        {
            // HP 텍스트 갱신
            abilityTexts[selectStatusNum].text = GameManager.Instance.player.HealthSys.MaxHP.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vigor + increaseNum].hp.ToString();
        }
        else
        {
            // 현재 어빌리티 텍스트
            abilityTexts[0].text = GameManager.Instance.player.HealthSys.MaxHP.ToString();
            abilityTexts[1].text = GameManager.Instance.player.HealthSys.MaxMP.ToString();
            abilityTexts[2].text = GameManager.Instance.player.HealthSys.MaxSP.ToString();
            abilityTexts[3].text = GameManager.Instance.player.CombatStat.DefensePoint.ToString();
            abilityTexts[10].text = GameManager.Instance.player.CombatStat.DamageMultiplier.ToString();

            // 결과 어빌리티 텍스트
            resultAbilityTexts[0].text = GameManager.Instance.player.HealthSys.MaxHP.ToString();
            resultAbilityTexts[1].text = GameManager.Instance.player.HealthSys.MaxMP.ToString();
            resultAbilityTexts[2].text = GameManager.Instance.player.HealthSys.MaxSP.ToString();
            resultAbilityTexts[3].text = GameManager.Instance.player.CombatStat.DefensePoint.ToString();
            resultAbilityTexts[10].text = GameManager.Instance.player.CombatStat.DamageMultiplier.ToString();

            // 오른손, 왼손 공격력 텍스트
            for (int i = 4; i < abilityTexts.Length - 1; i++)
            {
                if (Inventory.Instance.weaponSlotList[i - 4].Item != null)
                {
                    abilityTexts[i].text = (GameManager.Instance.player.CombatStat.AttackPoint + Inventory.Instance.weaponSlotList[i - 4].Item.damage).ToString();
                    resultAbilityTexts[i].text = (GameManager.Instance.player.CombatStat.AttackPoint + Inventory.Instance.weaponSlotList[i - 4].Item.damage).ToString();
                }
                else
                {
                    abilityTexts[i].text = GameManager.Instance.player.CombatStat.AttackPoint.ToString();
                    resultAbilityTexts[i].text = GameManager.Instance.player.CombatStat.AttackPoint.ToString();
                }
            }
        }
    } // GetAbilityStatus
} // LevelUpPanel
