using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerLeftArm;
    public GameObject playerRightArm;
    public PlayerCharacter player;

    //! 화톳불 이용시 씬 로드하는 함수
    public void LoadBonfire(BonfireData bonfire)
    {
        StartCoroutine(LoadBonfireScene(bonfire));
    } // LoadBonfire
    //! 화톳불 이용시 씬 로드하는 코루틴함수

    //! 뉴게임 선택 시 씬 로드하는 함수
    public void NewGamePlay(int num)
    {
        StartCoroutine(NewGameLoadScene(num));
    } // NewGamePlay

    //! 세이브데이터 로드 시 해당 씬 로드하는 함수
    public void LoadSaveDataScene(int num)
    {
        StartCoroutine(LoadSaveDataPlayScene(num));
    } // LoadSaveDataScene

    private IEnumerator LoadBonfireScene(BonfireData bonfire)
    {
        // 자동저장 슬롯에 현재데이터 저장
        DataManager.Instance.slotNum = 0;
        DataManager.Instance.SaveData();
        // 로딩창 활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
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
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // 로딩창 비활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadScene

    //! 뉴게임 선택 시 씬이동 코루틴함수
    private IEnumerator NewGameLoadScene(int num)
    {
        // 로딩창 활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        var asyncLoad = SceneManager.LoadSceneAsync(GData.SCENENAME_PLAY);
        Debug.Log($"씬로드 시작");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log($"씬로드 끝");
        yield return new WaitForSeconds(3f);
        GameManager.Instance.player.PlayerNameSelect(DataManager.Instance.selectPlayerName);
        // 뉴게임 시작시 에스트병과 잿빛 에스트병 가지고 시작
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
        // 로딩창 비활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadScene

    //! 세이브데이터 불러와서 해당 데이터 플레이씬 불러오는 코루틴함수
    private IEnumerator LoadSaveDataPlayScene(int num)
    {
        // 로딩창 활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(true);
        float fadeTime = UiManager.Instance.loadingPanel.FadeInLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        var asyncLoad = SceneManager.LoadSceneAsync(GData.SCENENAME_PLAY);
        Debug.Log($"씬로드 시작");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log($"씬로드 끝");
        DataManager.Instance.slotNum = num;
        DataManager.Instance.LoadData();
        player.transform.position = player.GetPlayerData().PlayerPos;
        yield return new WaitForSeconds(3f);
        UiManager.Instance.loadingPanel.FadeOutLoadingPanel();
        yield return new WaitForSeconds(fadeTime);
        // 로딩창 비활성화
        UiManager.Instance.loadingPanel.gameObject.SetActive(false);
    } // LoadSaveDataPlayScene
} // GameManager
