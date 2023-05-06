using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEnemyHpBar : GData.IInitialize
{
    void InitHpBar(float maxHp, float currentHp);
    void ActiveHpBar();
    void UpdateHpBar(float newHp);
}

public class EnemyHpBar : MonoBehaviour, IEnemyHpBar
{
    private Transform _camera = default;

    #region Inspector
    [SerializeField]
    private Transform _hpBarCanvas;
    [SerializeField]
    private Image _hpFront;
    #endregion

    private float _maxHp = default;

    [SerializeField]
    private bool _isBoss;

    void Start()
    {
        _camera = Camera.main.transform;
    }

    public void Init()
    {
        BossBase bossBase;
        _isBoss = TryGetComponent(out bossBase);
    }


    void Update()
    {
        Quaternion cameraRotation_ = new Quaternion(_camera.rotation.x, _camera.rotation.y, _camera.rotation.z, _camera.rotation.w);
        if (!_isBoss && _hpBarCanvas.gameObject.activeSelf)
        {
            _hpBarCanvas.LookAt(_hpBarCanvas.position + cameraRotation_ * Vector3.forward, cameraRotation_ * Vector3.up);
            //_hpBarCanvas.rotation = Quaternion.LookRotation(_hpBarCanvas.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
        }
    }

    public void InitHpBar(float maxHp, float currentHp)
    {
        _maxHp = maxHp;
        _hpFront.fillAmount = currentHp / maxHp;
        _hpBarCanvas.gameObject.SetActive(false);
    }
    public void ActiveHpBar()
    {
        _hpBarCanvas.gameObject.SetActive(true);
    }
    public void UpdateHpBar(float newHp)
    {
        _hpFront.fillAmount = newHp / _maxHp;
    }
}