using StarterAssets;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    #region Variables
    [SerializeField] private float _damage, _reloadTime;
    [SerializeField] private int _magazineSize, _bulletsLeft;
    [SerializeField] private WeaponData.SelectWeaponType _weaponType;
    [SerializeField] private Sprite _weaponIcon;
    [SerializeField] private GameObject _currentWeapon;
    
    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public float ReloadTime
    {
        get => _reloadTime;
        set => _reloadTime = value;
    }

    public int MagazineSize
    {
        get => _magazineSize;
        set => _magazineSize = value;
    }
    
    public int BulletsLeft
    {
        get => _bulletsLeft;
        set => _bulletsLeft = value;
    }
    public WeaponData.SelectWeaponType WeaponType
    {
        get => _weaponType;
        set => _weaponType = value;
    }
    
    public Sprite WeaponIcon
    {
        get => _weaponIcon;
        set => _weaponIcon = value;
    }
    
    public GameObject CurrentWeapon
    {
        get => _currentWeapon;
        set => _currentWeapon = value;
    }

    #endregion

    #region References
    [Header("== References ==")]
    [SerializeField] private StarterAssetsInputs _starterAssetsInputs;
    [SerializeField] private ThirdPersonShooterController _thirdPersonShooterController;

    #endregion

    private void Awake()
    {
        _bulletsLeft = _magazineSize;
    }
    private void Update()
    {
        if (_bulletsLeft <= 0)
        {
            if (_starterAssetsInputs._reloadRiffle && !_thirdPersonShooterController._reload)
            {
                Reload();
            }
        }
    }
    
    public void Shoot()
    {
        if (_bulletsLeft <= _magazineSize)
        {
            _bulletsLeft--;
        }

        if (_bulletsLeft <= 0)
        {
            Debug.Log($"{_bulletsLeft} ammo" );
        }
    }
    private void Reload()
    {
        _thirdPersonShooterController._reload = true;
        _thirdPersonShooterController.SetReloadAnimation(2);
        Invoke("SetReloadAnimationDelayed", _reloadTime);
    }
    private void SetReloadAnimationDelayed()
    {
        ReloadFinished();
        _thirdPersonShooterController.SetReloadAnimation(0);
    }
    private void ReloadFinished()
    {
        _bulletsLeft = _magazineSize;
    }
}
