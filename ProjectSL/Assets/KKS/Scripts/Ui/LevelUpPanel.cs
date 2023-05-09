using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
    [Header("������ ��ư ����")]
    [SerializeField] private Button plusBt; // �÷��� ��ư
    [SerializeField] private Button minusBt; // ���̳ʽ� ��ư
    [SerializeField] private Button decisionBt; // ���� ��ư
    [SerializeField] private Button cancelBt; // ��� ��ư
    [SerializeField] private Button vigorBt; // ����� ��ư
    [SerializeField] private Button attunementBt; // ���߷� ��ư
    [SerializeField] private Button enduranceBt; // ������ ��ư
    [SerializeField] private Button vitalityBt; // ü�� ��ư
    [SerializeField] private Button strengthBt; // �ٷ� ��ư
    [SerializeField] private Button dexterityBt; // �ⷮ ��ư

    [Header("������ �ؽ�Ʈ ����")]
    [SerializeField] private TMP_Text playerNameText; // �÷��̾� �̸� �ؽ�Ʈ
    [SerializeField] private TMP_Text levelText; // ���� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text resultLevelText; // ��� ���� �ؽ�Ʈ
    [SerializeField] private TMP_Text soulText; // ���� �����ҿ� �ؽ�Ʈ
    [SerializeField] private TMP_Text resultSoulText; // ��� �����ҿ� �ؽ�Ʈ
    [SerializeField] private TMP_Text wantSoulText; // �ʿ��� �ҿ� �ؽ�Ʈ
    [SerializeField] private TMP_Text[] statusTexts; // ���� ���� �ؽ�Ʈ �迭
    [SerializeField] private TMP_Text[] resultStatusTexts; // ��� ���� �ؽ�Ʈ �迭
    [SerializeField] private Image[] statusImages; // ���� �̹��� �迭

    [Header("�ɷ�ġ �ؽ�Ʈ ����")]
    [SerializeField] private TMP_Text[] abilityTexts; // �ɷ�ġ �ؽ�Ʈ �迭
    [SerializeField] private TMP_Text[] resultAbilityTexts; // ��� �ɷ�ġ �ؽ�Ʈ �迭

    [SerializeField] private GameObject warningPanel; // ���â
    [SerializeField] private TMP_Text warningText; // ���â �ؽ�Ʈ
    [SerializeField] private GameObject selectPanel; // NPC��ȭ ���� �г�
    [SerializeField] private GameObject levelUpPanel; // ������ �г�
    [SerializeField] private GameObject potionUpPanel; // ���ǰ�ȭ �г�
    private string pWarningMessage = "�������� �ʿ��� �ҿ��� �����մϴ�";
    private string mWarningMessage = "���� ���� �̸����� �����Ҽ������ϴ�";
    private int[] playerStatus; // �÷��̾� ���罺�� �迭
    private int selectStatusNum; // ������ ���� ��ȣ
    private int increaseNum = 0; // +, - ��������
    private int sumWantSoul = 0; // �ʿ��� �ҿ� �հ�
    private bool hasSelectBt = false; // �������� ������ ������ ���� üũ
    void Start()
    {
        // �÷��� ��ư
        plusBt.onClick.AddListener(() =>
        {
            PlusBtClick();
        });

        // ���̳ʽ� ��ư
        minusBt.onClick.AddListener(() =>
        {
            MinusBtClick();
        });

        // ���� ��ư
        decisionBt.onClick.AddListener(() =>
        {
            DecisionBtClick();
        });

        // ��� ��ư
        cancelBt.onClick.AddListener(() =>
        {
            if (selectStatusNum < 0)
            {
                return;
            }
            // �г� �ʱ�ȭ
            statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
            selectStatusNum = -1;
            GetPlayerStatus();
            GetAbilityStatus();
            increaseNum = 0;
            sumWantSoul = 0;
            hasSelectBt = false;
        });

        // ����� ��ư
        vigorBt.onClick.AddListener(() =>
        {
            SelectStatus(0);
        });

        // ���߷� ��ư
        attunementBt.onClick.AddListener(() =>
        {
            SelectStatus(1);
        });

        // ������ ��ư
        enduranceBt.onClick.AddListener(() =>
        {
            SelectStatus(2);
        });

        // ü�� ��ư
        vitalityBt.onClick.AddListener(() =>
        {
            SelectStatus(3);
        });

        // �ٷ� ��ư
        strengthBt.onClick.AddListener(() =>
        {
            SelectStatus(4);
        });

        // �ⷮ ��ư
        dexterityBt.onClick.AddListener(() =>
        {
            SelectStatus(5);
        });
    } // Start

    private void OnEnable()
    {
        selectStatusNum = -1;
        // Ȱ��ȭ�� �� �÷��̾��� ���罺�� ������
        GetPlayerStatus();
        // �ɷ�ġ �г� �ؽ�Ʈ �Ҵ�
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

        // �г� �ؽ�Ʈ ǥ�� �ʱ�ȭ
        playerNameText.text = pStatus.Name;
        levelText.text = playerStatus[6].ToString();
        resultLevelText.text = playerStatus[6].ToString();
        soulText.text = Inventory.Instance.Soul.ToString();
        resultSoulText.text = Inventory.Instance.Soul.ToString();
        wantSoulText.text = "0";
        // ���� �ؽ�Ʈ ǥ�� �ʱ�ȭ
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
        // ���ݹ�ư ��������Ʈ ���İ� �ʱ�ȭ
        foreach (var image in statusImages)
        {
            image.color = new Color(1, 1, 1, 0);
        }
        selectStatusNum = _num;
        // ������ ������ ��������Ʈ �����ֱ�
        statusImages[selectStatusNum].color = new Color(1, 1, 1, 1);
    } // SelectStatus

    //! �÷��� ��ư Ŭ�����
    private void PlusBtClick()
    {
        if (selectStatusNum < 0)
        {
            return;
        }
        hasSelectBt = true;
        increaseNum += 1;
        // ���� �������� �ʿ��� ����ġ���� ����ġ���������̺��� ������
        int value = 0;
        DataManager.Instance.experienceDatas.TryGetValue(((playerStatus[6] + increaseNum) - 1), out value);
        sumWantSoul += value;
        if (sumWantSoul > Inventory.Instance.Soul)
        {
            sumWantSoul -= value;
            increaseNum -= 1;
            // ���â �˾�
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

    //! ���̳ʽ� ��ư Ŭ�����
    private void MinusBtClick()
    {
        if (selectStatusNum < 0)
        {
            return;
        }
        if (increaseNum == 0)
        {
            // ���â �˾�
            warningPanel.SetActive(true);
            warningText.text = mWarningMessage;
            return;
        }
        int level = playerStatus[6];
        increaseNum -= 1;
        levelText.text = level.ToString();

        resultLevelText.text = (level + increaseNum).ToString();
        soulText.text = Inventory.Instance.Soul.ToString();

        // ���� �������� �ʿ��� ����ġ���� ����ġ���������̺��� ������
        int value = 0;
        DataManager.Instance.experienceDatas.TryGetValue((level + increaseNum), out value);
        sumWantSoul -= value;
        wantSoulText.text = sumWantSoul.ToString();
        resultSoulText.text = (Inventory.Instance.Soul - sumWantSoul).ToString();

        statusTexts[selectStatusNum].text = playerStatus[selectStatusNum].ToString();
        resultStatusTexts[selectStatusNum].text = (playerStatus[selectStatusNum] + increaseNum).ToString();
        GetAbilityStatus();
    } // MinusBtClick

    //! ���� ��ư Ŭ�����
    private void DecisionBtClick()
    {
        // ������ ���� ������ ����
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
        // �������� ���ݵ��� ������ �÷��̾� �����Ϳ� ����
        GameManager.Instance.player.LoadPlayerData(pStatus);
        GameManager.Instance.player.equipmentController._onChangedAbilityStat(GameManager.Instance.player.equipmentController);


        // �г� �ʱ�ȭ
        statusImages[selectStatusNum].color = new Color(1, 1, 1, 0);
        selectStatusNum = -1;
        GetPlayerStatus();
        GetAbilityStatus();
        increaseNum = 0;
        sumWantSoul = 0;
        hasSelectBt = false;

        // ������ Ȯ���ϸ� �ڵ����� ���Կ� �÷��̾� ������ ����
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
    } // DecisionBtClick

    //! �ɷ�ġ �г� �ؽ�Ʈ �Ҵ��ϴ� �Լ�
    private void GetAbilityStatus()
    {
        PlayerStatus pStatus = GameManager.Instance.player.GetPlayerData()._playerStatusData;
        if (selectStatusNum == 5)
        {
            // ���ݷ� ���� �ؽ�Ʈ ����
            abilityTexts[10].text = DataManager.Instance.statusLevelData[pStatus.Dexterity].damageMultiplier.ToString();
            resultAbilityTexts[10].text = DataManager.Instance.statusLevelData[pStatus.Dexterity + increaseNum].damageMultiplier.ToString();
        }
        else if (selectStatusNum == 4)
        {
            // ������, �޼� ���ݷ� �ؽ�Ʈ ����
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
            // ���� �ؽ�Ʈ ����
            abilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vitality].defense.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vitality + increaseNum].defense.ToString();
        }
        else if (selectStatusNum == 2)
        {
            // ���׹̳� �ؽ�Ʈ ����
            abilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Endurance].st.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Endurance + increaseNum].st.ToString();
        }
        else if (selectStatusNum == 1)
        {
            // MP �ؽ�Ʈ ����
            abilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Attunement].mp.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Attunement + increaseNum].mp.ToString();
        }
        else if (selectStatusNum == 0)
        {
            // HP �ؽ�Ʈ ����
            abilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vigor].hp.ToString();
            resultAbilityTexts[selectStatusNum].text = DataManager.Instance.statusLevelData[pStatus.Vigor + increaseNum].hp.ToString();
        }
        else
        {
            // ���� �����Ƽ �ؽ�Ʈ
            abilityTexts[0].text = DataManager.Instance.statusLevelData[pStatus.Vigor].hp.ToString();
            abilityTexts[1].text = DataManager.Instance.statusLevelData[pStatus.Attunement].mp.ToString();
            abilityTexts[2].text = DataManager.Instance.statusLevelData[pStatus.Endurance].st.ToString();
            abilityTexts[3].text = DataManager.Instance.statusLevelData[pStatus.Vitality].defense.ToString();
            abilityTexts[10].text = DataManager.Instance.statusLevelData[pStatus.Dexterity].damageMultiplier.ToString();

            // ��� �����Ƽ �ؽ�Ʈ
            resultAbilityTexts[0].text = DataManager.Instance.statusLevelData[pStatus.Vigor].hp.ToString();
            resultAbilityTexts[1].text = DataManager.Instance.statusLevelData[pStatus.Attunement].mp.ToString();
            resultAbilityTexts[2].text = DataManager.Instance.statusLevelData[pStatus.Endurance].st.ToString();
            resultAbilityTexts[3].text = DataManager.Instance.statusLevelData[pStatus.Vitality].defense.ToString();
            resultAbilityTexts[10].text = DataManager.Instance.statusLevelData[pStatus.Dexterity].damageMultiplier.ToString();

            // ������, �޼� ���ݷ� �ؽ�Ʈ
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
