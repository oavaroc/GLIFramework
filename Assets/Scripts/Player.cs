using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerInputActions _input;

    [SerializeField]
    private GameObject _bulletHole;

    private bool _isReloading=false;

    [SerializeField]
    private LayerMask _layerMask;

    private float _fireCoolDown = 0.5f;

    private float _fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Fire.performed += Fire_performed;
        _input.Player.Reload.performed += Reload_performed;
    }

    private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(!_isReloading)
            StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        AudioManager.Instance.PlayReloadSound();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.UpdateAmmoCount(25);
        _isReloading = false;

    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (UIManager.Instance.GetAmmoCount() > 0 && !_isReloading && Time.time >= _fireTimer)
        {
            FireShot();
        }
        else if(Time.time >= _fireTimer)
        {
            AudioManager.Instance.PlayEmptyFire();
        }
    }
    private void FireShot()
    {
        _fireTimer = Time.time + _fireCoolDown;
        AudioManager.Instance.PlayGunFire();
        UIManager.Instance.UpdateAmmoCount(-1);
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Fire performed");
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, _layerMask))
        {
            RaycastHitSomething(hit);
        }
    }
    private void RaycastHitSomething(RaycastHit hit)
    {
        Debug.Log("Hit: " + hit.collider.name);
        if (hit.collider.gameObject.TryGetComponent(out AIController aicontroller))
        {
            HitAI(aicontroller);
        }
        else if (hit.collider.gameObject.TryGetComponent(out ForceBarrier forceBarrier))
        {
            if (forceBarrier != null)
            {
                HitForceBarrier(forceBarrier, forceBarrier.GetHealth());
            }
        }
    }

    private void HitAI(AIController aicontroller)
    {
        if (!aicontroller.GetIsDead())
        {
            aicontroller.ChangeState(new DeathState(aicontroller));
        }

    }

    private void HitForceBarrier(ForceBarrier forceBarrier, int health)
    {
        AudioManager.Instance.PlayForceBarrierHit();
        if (health > 0)
        {
            if (forceBarrier.SetHealth(health - 1) == 0)
            {
                BarrierManager.Instance.DisableForceBarrier(forceBarrier);
            }
        }
    }
}
