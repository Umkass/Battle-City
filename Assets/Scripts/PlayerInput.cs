using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Field Declarations

    private bool _canShoot = true; //cooldown timer
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool FireWeapons { get; private set; }
    public bool PickUpModule { get; private set; }
    public bool DestroyModule { get; private set; }

    #endregion

    public event Action<bool> OnFire = delegate { };
    public event Action OnPickUpModule = delegate { };
    public event Action OnDestroyModule = delegate { };

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        FireWeapons = Input.GetButtonDown("Fire1");
        PickUpModule = Input.GetKeyDown(KeyCode.Z);
        DestroyModule = Input.GetKeyDown(KeyCode.M);
        if (FireWeapons && _canShoot)
        {
            OnFire?.Invoke(true);
            StartCoroutine(ShootCoroutine());
        }
        if (PickUpModule)
            OnPickUpModule?.Invoke();
        if (DestroyModule)
            OnDestroyModule?.Invoke();
    }
    IEnumerator ShootCoroutine() //timer
    {
        _canShoot = false;
        yield return new WaitForSeconds(2f);
        _canShoot = true;
        yield break;
    }
}
