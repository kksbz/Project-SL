using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerLeftArm;
    public GameObject playerRightArm;
    public GameObject player;

    //! 씬 로드하는 코루틴함수
    public IEnumerator LoadScene(BonfireData bonfire)
    {
        // 자동저장 슬롯에 현재데이터 저장
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
        // 로딩창 활성화
        UiManager.Instance.loadingPanel.SetActive(true);
        var asyncLoad = SceneManager.LoadSceneAsync(bonfire.activeSceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // 씬이 불러와지면 플레이어 데이터 로드
        DataManager.Instance.LoadData();
        player.transform.position = bonfire.bonfirePos;
        yield return new WaitForSeconds(3f);
        Debug.Log($"씬로드 끝");
        // 로딩창 비활성화
        UiManager.Instance.loadingPanel.SetActive(false);
    } // LoadScene
} // GameManager
