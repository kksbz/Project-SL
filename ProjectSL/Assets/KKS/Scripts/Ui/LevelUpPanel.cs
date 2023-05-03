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
            // 패널 초기화
            statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
            GetPlayerStatus();
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
        // 활성화될 때 플레이어의 현재스텟 가져옴
        GetPlayerStatus();
    } // OnEnable

    private void OnDisable()
    {
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
        PlayerStatus pStatus = GameManager.Instance.player.GetPlayerData();
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
    } // PlusBtClick

    //! 마이너스 버튼 클릭기능
    private void MinusBtClick()
    {
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
    } // MinusBtClick

    //! 결정 버튼 클릭기능
    private void DecisionBtClick()
    {
        // 선택한 스텟 레벨업 적용
        playerStatus[6] = playerStatus[6] + increaseNum;
        playerStatus[selectStatusNum] = playerStatus[selectStatusNum] + increaseNum;
        UiManager.Instance.soulBag.GetSoul(-sumWantSoul);

        PlayerStatus pStatus = GameManager.Instance.player.GetPlayerData();
        pStatus.Vigor = playerStatus[0];
        pStatus.Attunement = playerStatus[1];
        pStatus.Endurance = playerStatus[2];
        pStatus.Vitality = playerStatus[3];
        pStatus.Strength = playerStatus[4];
        pStatus.Dexterity = playerStatus[5];
        pStatus.Level = playerStatus[6];
        // 레벨업된 스텟들의 정보를 플레이어 데이터에 저장
        GameManager.Instance.player.LoadPlayerData(pStatus);

        // 패널 초기화
        statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
        GetPlayerStatus();
        increaseNum = 0;
        sumWantSoul = 0;
        hasSelectBt = false;
    } // DecisionBtClick
} // LevelUpPanel
