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
        var asyncLoad = SceneManager.LoadSceneAsync(bonfire.activeSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // ���� �ҷ������� �÷��̾� ������ �ε�
        DataManager.Instance.LoadData();
        player.transform.position = bonfire.bonfirePos;
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
        player.transform.position = player.GetPlayerData().PlayerPos;
        yield return new WaitForSeconds(3f);
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // �ε�â ��Ȱ��ȭ
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadSaveDataPlayScene
} // GameManager
