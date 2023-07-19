using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerInputActions _input;

    [SerializeField]
    private GameObject _bulletHole;


    // Start is called before the first frame update
    void Start()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Fire.performed += Fire_performed;
        //TODO: move the spawning to another script
        SpawnManager.Instance.StartSpawning();
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Fire performed");
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, out hit, Mathf.Infinity, 1 << 6 | 1 << 7))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.gameObject.TryGetComponent( out AIController _aicontroller))
            {
                _aicontroller.ChangeState(new DeathState(_aicontroller));
            }
        }
    }
}
