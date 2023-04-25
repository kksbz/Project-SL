using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{
    //! Ư�� ������Ʈ�� �ڽ� ������Ʈ�� ��ġ�ؼ� ������Ʈ�� ã���ִ� �Լ�
    public static T FindChildComponent<T>(
        this GameObject targetObj_, string objName_) where T : Component
    {
        T searchResultComponent = default(T);
        GameObject searchResultObj = default(GameObject);

        searchResultObj = targetObj_.FindChildObj(objName_);
        if (searchResultObj.IsValid())
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }
        return searchResultComponent;
    }       // FindChildComponent()

    //! Ư�� ������Ʈ�� �ڽ� ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject FindChildObj(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);

                // ������
                if (searchResult == null || searchResult == default) { /* Pass */ }
                else { return searchResult; }
            }
        }       // loop

        return searchResult;
    }       // FindChildObj()


    //! ���� ��Ʈ ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals(objName_))
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }       // loop

        return targetObj_;
    }       // GetRootObj()

    //! Ư�� ������Ʈ�� �ڽ� ������Ʈ�� ��� �����ϴ� �Լ�
    public static List<GameObject> GetChildrenObjs(this GameObject targetObj_)
    {
        List<GameObject> objs_ = new List<GameObject>();
        GameObject searchTarget = default;
        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            objs_.Add(searchTarget);
        }
        if (objs_.IsValid())
        { return objs_; }
        else
        { return default(List<GameObject>); }
    }   // GetChildrenObjs()
    //! RectTransform �� ã�Ƽ� �����ϴ� �Լ�
    public static RectTransform GetRect(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>();
    }       // GetRect()

    //! RectTransform ���� sizeDelta�� ã�Ƽ� �����ϴ� �Լ�
    public static Vector2 GetRectSizeDelta(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>().sizeDelta;
    }       // GetRectSizeDelta()

    //! ���� Ȱ��ȭ �Ǿ� �ִ� ���� ã���ִ� �Լ�
    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }       // GetActiveScene()



    //! ������Ʈ �������� �Լ�
    public static T GetComponentMust<T>(this GameObject obj) where T : Component
    {
        T component_ = obj.GetComponent<T>();

        GFunc.Assert(component_.IsValid<T>() != false,
            string.Format("{0}���� {1}��(��) ã�� �� �����ϴ�.",
            obj.name, component_.GetType().Name));

        return component_;
    }       // GetComponentMust()

    //! ���ο� ������Ʈ�� ���� ������Ʈ�� �����ϴ� �Լ�
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }       // CreateObj()

    //! ������Ʈ�� �ı��ϴ� �Լ�
    public static void DestroyObj(this GameObject obj_, float delay = 0.0f)
    {
        Object.Destroy(obj_, delay);
    }

    //! ���� �������� �������� �� Ÿ�� ������Ʈ�� ��ġ�� ���ϴ� �Լ�
    public static int CompareTileObjToLodalPos2D(GameObject firstObj, GameObject secondObj)
    {
        Vector2 fPos = firstObj.transform.localPosition;
        Vector2 sPos = secondObj.transform.localPosition;

        int compareResult = 0;
        if (fPos.y.IsEquals(sPos.y))
        {
            // x �������� ������ ���� Ÿ���̹Ƿ� 0�� ����
            if (fPos.x.Equals(sPos.x)) { compareResult = 0; }
            else
            {
                if (fPos.x < sPos.x) { compareResult = -1; }
                else { compareResult = 1; }
            }   // else : x �������� �ٸ� ���
            return compareResult;
        }   // if : y �������� ���� ���

        // y �������� �ٸ� ��� ��Һ�
        if (fPos.y < sPos.y) { compareResult = -1; }
        else
        {
            compareResult = 1;
        }

        return compareResult;

    }
    #region Object Transform Control
    //! ������Ʈ�� ���� �������� �����ϴ� �Լ�
    public static void SetLocalScale(this GameObject obj_, Vector3 localScale_)
    {
        obj_.transform.localScale = localScale_;
    }   // SetLocalScale()
    //! ������Ʈ�� ���� �������� �����ϴ� �Լ�
    public static void SetLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition = new Vector3(x, y, z);
    }       // SetLocalPos()
            //! ������Ʈ�� ���� �������� �����ϴ� �Լ�
    public static void SetLocalPos(this GameObject obj_,
        Vector3 localPos)
    {
        obj_.transform.localPosition = localPos;
    }       // SetLocalPos()

    //! ������Ʈ�� ���� �������� �����ϴ� �Լ�
    public static void AddLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition =
            obj_.transform.localPosition + new Vector3(x, y, z);
    }       // AddLocalPos()

    //! ������Ʈ�� ��Ŀ �������� �����ϴ� �Լ�
    public static void AddAnchoredPos(this GameObject obj_,
        float x, float y)
    {
        obj_.GetRect().anchoredPosition += new Vector2(x, y);
    }       // AddAnchoredPos()

    //! ������Ʈ�� ��Ŀ �������� �����ϴ� �Լ�
    public static void AddAnchoredPos(this GameObject obj_,
        Vector2 position2D)
    {
        obj_.GetRect().anchoredPosition += position2D;
    }       // AddAnchoredPos()

    //! Ʈ�������� ����ؼ� ������Ʈ�� �����̴� �Լ�
    public static void Translate(this Transform transform_, Vector2 moveVector)
    {
        transform_.Translate(moveVector.x, moveVector.y, 0f);
    }       // Translate()
    #endregion


    /// <summary>
    /// 지정된 범위 안에 타겟과 기준점 사이에 장애물(벽 등)이 있는지 확인하여서 기준점 사이에 장애물이 존재하지 않는 타겟만을 반환하는 함수
    /// </summary>
    /// <param name="myTransform">기준점</param>
    /// <param name="radius">범위</param>
    /// <param name="angle">각도</param>
    /// <param name="targetMask">타겟 LayerMask</param>
    /// <param name="obstacleMask">장애물 LayerMask</param>
    /// <returns></returns>
    public static List<Transform> FindTargetInRange(Transform myTransform, float radius, float angle, LayerMask targetMask, LayerMask obstacleMask)
    {
        List<Transform> targets = new List<Transform>();

        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(myTransform.position, radius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - myTransform.position).normalized;

            // forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(myTransform.forward, dirToTarget) < angle / 2)
            {
                float dstToTarget = Vector3.Distance(myTransform.position, target.transform.position);
                // 타겟으로 가는 레이캐스트에 obstacleMask가 걸리지 않으면 visibleTargets에 Add
                if (!Physics.Raycast(myTransform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    targets.Add(target);
                }
            }
        }

        return targets;
    }
}
