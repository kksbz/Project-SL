using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerLeftArm;
    public GameObject playerRightArm;
    public GameObject player;

    //! �� �ε��ϴ� �ڷ�ƾ�Լ�
    public IEnumerator LoadScene(BonfireData bonfire)
    {
        // �ڵ����� ���Կ� ���絥���� ����
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
        // �ε�â Ȱ��ȭ
        UiManager.Instance.loadingPanel.SetActive(true);
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
        // �ε�â ��Ȱ��ȭ
        UiManager.Instance.loadingPanel.SetActive(false);
    } // LoadScene
} // GameManager
