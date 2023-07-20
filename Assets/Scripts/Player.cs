using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerInputActions _input;

    [SerializeField]
    private GameObject _bulletHole;

    private bool _isReloading=false;


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
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.UpdateAmmoCount(25);
        _isReloading = false;

    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (UIManager.Instance.GetAmmoCount() > 0 && !_isReloading)
        {
            UIManager.Instance.UpdateAmmoCount(-1);
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Fire performed");
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, 1 << 6 | 1 << 7))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.gameObject.TryGetComponent( out AIController _aicontroller))
                {
                    if(!_aicontroller.GetIsDead())
                    {
                        _aicontroller.ChangeState(new DeathState(_aicontroller));
                    }
                }
            }

        }
    }
}
