using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerLeftArm;
    public GameObject playerRightArm;
    public PlayerCharacter player;

    //! ȭ��� �̿�� �� �ε��ϴ� �Լ�
    public void LoadBonfire(BonfireData bonfire)
    {
        StartCoroutine(LoadBonfireScene(bonfire));
    } // LoadBonfire
    //! ȭ��� �̿�� �� �ε��ϴ� �ڷ�ƾ�Լ�

    //! ������ ���� �� �� �ε��ϴ� �Լ�
    public void NewGamePlay(int num)
    {
        StartCoroutine(NewGameLoadScene(num));
    } // NewGamePlay

    //! ���̺굥���� �ε� �� �ش� �� �ε��ϴ� �Լ�
    public void LoadSaveDataScene(int num)
    {
        StartCoroutine(LoadSaveDataPlayScene(num));
    } // LoadSaveDataScene

    private IEnumerator LoadBonfireScene(BonfireData bonfire)
    {
        // �ڵ����� ���Կ� ���絥���� ����
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
        // �ε�â Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        var asyncLoad = SceneManager.LoadSceneAsync(GData.SCENENAME_PLAY);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // ���� �ҷ������� �÷��̾� ������ �ε�
        DataManager.Instance.LoadData();
        player.transform.position = bonfire.bonfirePos;
        DataManager.Instance.SaveData();
        yield return new WaitForSeconds(3f);
        Debug.Log($"���ε� ��");
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // �ε�â ��Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadScene

    //! ������ ���� �� ���̵� �ڷ�ƾ�Լ�
    private IEnumerator NewGameLoadScene(int num)
    {
        // �ε�â Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        var asyncLoad = SceneManager.LoadSceneAsync(GData.SCENENAME_PLAY);
        Debug.Log($"���ε� ����");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log($"���ε� ��");
        yield return new WaitForSeconds(3f);
        GameManager.Instance.player.PlayerNameSelect(DataManager.Instance.selectPlayerName);
        // ������ ���۽� ����Ʈ���� ���� ����Ʈ�� ������ ����
        ItemData hpPotion = new ItemData(DataManager.Instance.itemDatas[0]);
        hpPotion.Quantity = hpPotion.maxQuantity;
        ItemData mpPotion = new ItemData(DataManager.Instance.itemDatas[1]);
        mpPotion.Quantity = mpPotion.maxQuantity;
        Inventory.Instance.AddItem(hpPotion);
        Inventory.Instance.AddItem(mpPotion);
        DataManager.Instance.slotNum = num;
        DataManager.Instance.SaveData();
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // �ε�â ��Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadScene

    //! ���̺굥���� �ҷ��ͼ� �ش� ������ �÷��̾� �ҷ����� �ڷ�ƾ�Լ�
    private IEnumerator LoadSaveDataPlayScene(int num)
    {
        // �ε�â Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        var asyncLoad = SceneManager.LoadSceneAsync(GData.SCENENAME_PLAY);
        Debug.Log($"���ε� ����");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log($"���ε� ��");
        DataManager.Instance.slotNum = num;
        DataManager.Instance.LoadData();
        InitPlayer(DataManager.Instance.playerStatusSaveData);
        yield return new WaitForSeconds(3f);
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // �ε�â ��Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadSaveDataPlayScene

    //! �÷��̾� Dead���¿� ���� ������ �ε��ϴ� �Լ�
    public void InitPlayer(StatusSaveData _playerStatusData)
    {
        if (_playerStatusData._isPlayerDead == true)
        {
            // �÷��̾ �׾��� ���
            float neardistance = Mathf.Infinity;
            Vector3 revivePos = Vector3.zero;

            // ������ġ���� Ȱ��ȭ�� ���� ����� ȭ����� ��ġ���� ��Ȱ��Ŵ
            for (int i = 0; i < UiManager.Instance.warp.bonfireList.Count; i++)
            {
                float _dis = Vector3.SqrMagnitude(_playerStatusData._playerPos - UiManager.Instance.warp.bonfireList[i].bonfirePos);
                if (_dis < neardistance)
                {
                    neardistance = _dis;
                    revivePos = UiManager.Instance.warp.bonfireList[i].bonfirePos;
                }
            }
            player.transform.position = revivePos;
            // �ҿ��� ��� �Ұ� ���� ��ġ�� �������ִ� �ҿ��� ��� ��Ŵ
            if (Inventory.Instance.Soul > 0)
            {
                GameObject Soul = Instantiate(Resources.Load<GameObject>("KKS/Prefabs/Objecct/DropSoul"));
                Soul.GetComponent<DropSoul>().souls = Inventory.Instance.Soul;
                UiManager.Instance.soulBag.GetSoul(-Inventory.Instance.Soul);
                Soul.transform.position = _playerStatusData._playerPos;
            }
        }
        else
        {
            // �÷��̾ ������� ���
            player.transform.position = _playerStatusData._playerPos;
            player.HealthSys.HP = _playerStatusData._currentHealthPoint;
            player.HealthSys.MP = _playerStatusData._currentManaPoint;
        }
    } // InitPlayer
} // GameManager
