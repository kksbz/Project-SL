using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    [SerializeField] private List<Sprite> frontSprites = new List<Sprite>(); // ����Ʈ �̹��� ����Ʈ
    [SerializeField] private List<Sprite> behindSprites = new List<Sprite>(); // �����ε� �̹��� ����Ʈ
    [SerializeField] private GameObject messageObj; // �޽��� ������Ʈ
    [SerializeField] private Animator messageAni; // �޽��� �ִϸ�����
    [SerializeField] private Image frontImage; // ����Ʈ �̹���
    [SerializeField] private Image behindImage; // �����ε� �̹���

    //! �÷��̾� ��� �޽���
    public void PlayerDeadMessage()
    {
        frontImage.sprite = frontSprites[0];
        behindImage.sprite = behindSprites[0];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // PlayerDeadMessage

    //! ȭ��� Ȱ��ȭ �޽���
    public void BonfireMessage()
    {
        frontImage.sprite = frontSprites[1];
        behindImage.sprite = behindSprites[1];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // BonfireMessage

    //! �Ҿ���� �ҿ� ã���� �� �޽���
    public void GetSoulBackMessage()
    {
        frontImage.sprite = frontSprites[2];
        behindImage.sprite = behindSprites[2];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // GetSoulBackMessage

    //! �Ϲ� ���� óġ �޽���
    public void KillNormalBossMessage()
    {
        frontImage.sprite = frontSprites[3];
        behindImage.sprite = behindSprites[3];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // KillNormalBossMessage

    //! ���� ���� óġ �޽���
    public void KillLastBossMessage()
    {
        frontImage.sprite = frontSprites[4];
        behindImage.sprite = behindSprites[4];
        frontImage.SetNativeSize();
        behindImage.SetNativeSize();
        StartCoroutine(ActiveMessage());
    } // KillLastBossMessage

    //! �޽��� �ִϸ��̼� ������ ��Ȱ��ȭ�� �ڷ�ƾ�Լ�
    private IEnumerator ActiveMessage()
    {
        messageObj.SetActive(true);
        yield return new WaitForSeconds(messageAni.GetCurrentAnimatorStateInfo(0).length);
        messageObj.SetActive(false);
        if (frontImage.sprite == frontSprites[0])
        {
            // �÷��̾ �׾��� ��� �ڵ����彽�Կ� ������ �� ��Ȱ
            DataManager.Instance.slotNum = 0;
            DataManager.Instance.SaveData();
            Inventory.Instance.Soul = 0;
            yield return new WaitForSeconds(1f);
            GameManager.Instance.LoadSaveDataScene(0);
        }
    } // ActiveMessage
} // MessagePanel
