using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private List<Sprite> frontSprites = new List<Sprite>(); // 프론트 이미지 리스트
    [SerializeField] private List<Sprite> behindSprites = new List<Sprite>(); // 비하인드 이미지 리스트
    [SerializeField] private GameObject messageObj; // 메시지 오브젝트
    [SerializeField] private Animator messageAni; // 메시지 애니메이터
    [SerializeField] private Image frontImage; // 프론트 이미지
    [SerializeField] private Image behindImage; // 비하인드 이미지

    //! 플레이어 사망 메시지
    public void PlayerDeadMessage()
    {
        frontImage.sprite = frontSprites[0];
        behindImage.sprite = behindSprites[0];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // PlayerDeadMessage

    //! 화톳불 활성화 메시지
    public void BonfireMessage()
    {
        frontImage.sprite = frontSprites[1];
        behindImage.sprite = behindSprites[1];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // BonfireMessage

    //! 잃어버린 소울 찾았을 때 메시지
    public void GetSoulBackMessage()
    {
        frontImage.sprite = frontSprites[2];
        behindImage.sprite = behindSprites[2];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // GetSoulBackMessage

    //! 일반 보스 처치 메시지
    public void KillNormalBossMessage()
    {
        frontImage.sprite = frontSprites[3];
        behindImage.sprite = behindSprites[3];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // KillNormalBossMessage

    //! 최종 보스 처치 메시지
    public void KillLastBossMessage()
    {
        frontImage.sprite = frontSprites[4];
        behindImage.sprite = behindSprites[4];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // KillLastBossMessage

    //! 메시지 애니메이션 끝나면 비활성화는 코루틴함수
    private IEnumerator ActiveMessage()
    {
        messageObj.SetActive(true);
        yield return new WaitForSeconds(messageAni.GetCurrentAnimatorStateInfo(0).length);
        messageObj.SetActive(false);
        if (frontImage.sprite == frontSprites[0])
        {
            // 플레이어가 죽었을 경우 자동저장슬롯에 저장한 후 부활
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.SaveData();
            Inventory.Instance.Soul = 0;
            yield return new WaitForSeconds(1f);
            GameManager.Instance.LoadSaveDataScene(0);
        }
    } // ActiveMessage
} // MessagePanel
