using StarterAssets;
using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    #region PlayerSetting
    [Header("== PlayerController ==")]
    [SerializeField] private float _normalSensitivity, _aimSensitivity;
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    public bool _reload = false;

    private float _animationRigWeight = 0;

    #endregion

    #region References

    [Header("== References ==")]
    [SerializeField] private WeaponSystem _weaponSystem;
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _vfxHitEnemyRed;
    [SerializeField] private Transform _vfxHitWallGreen;

    private StarterAssetsInputs _starterAssetsInputs;
    private ThirdPersonController _thirdPersonController;
    private Animator _animator;
    
    
    #endregion

    [Header("ForTests")] [SerializeField] private Transform _debugTransform;
    private static readonly int Aiming = Animator.StringToHash("Aiming");
    private static readonly int ReloadRiffle = Animator.StringToHash("ReloadRiffle");


    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        var hitpoint = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitpoint = raycastHit.point;
        }
        
        if (_starterAssetsInputs._aim && !_reload)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetRotateOnMove(false);
            _thirdPersonController.SetSensitivity(_aimSensitivity);
            //_animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            _animator.SetFloat(Aiming, 2);
            _animationRigWeight = 1;

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            if (_starterAssetsInputs._reloadRiffle && _reload)
            {
                _animator.SetFloat(ReloadRiffle, 2);
            }
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetRotateOnMove(true);
            _thirdPersonController.SetSensitivity(_normalSensitivity);
            //_animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            _animator.SetFloat(Aiming, 0);
            _animationRigWeight = 0;
            _starterAssetsInputs._shoot = false;
        }

        if (_starterAssetsInputs._shoot && !_reload)
        {
            _weaponSystem.Shoot();
            if (GetComponents<BulletTarget>() != null)
            {
                Instantiate(_vfxHitEnemyRed, mouseWorldPosition, Quaternion.identity);
                Debug.Log("Red");
            }
            else if (GetComponents<BulletTarget>() == null)
            {
                //Hit something else
                Instantiate(_vfxHitWallGreen, mouseWorldPosition, Quaternion.identity);
                Debug.Log("Green");
            }
            _starterAssetsInputs._shoot = false;
        }
        //Rig Controller(smooth)
        float currentWeight = _rig.weight;
        _rig.weight = Mathf.Lerp(currentWeight, _animationRigWeight, Time.deltaTime * 10f);
        
    }

    public void SetReloadAnimation(int value)
    {
        _animator.SetFloat(ReloadRiffle, value);
        _reload = false;
    }
}
