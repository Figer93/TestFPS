using UnityEngine;

public class WeaponDataLoader : MonoBehaviour
{
    [SerializeField] private WeaponData[] _weapons, _sprites;
    [SerializeField] private Transform _spawnWeaponHolder;
    
    private WeaponSystem _weaponSystem;

    //LoadWeaponObjects
    private GameObject _currentWeaponPrefab;
    private GameObject _currentWeaponLink;
    
    private void Awake()
    {
        _weaponSystem = GetComponent<WeaponSystem>();
        LoadResources();
        LoadCurrentWeapon();
        AssignWeaponData();
    }

    private void LoadResources()
    {
        _weapons = Resources.LoadAll<WeaponData>("Guns");
        _sprites = Resources.LoadAll<WeaponData>("Icons");
    }

   private void LoadCurrentWeapon()
    {
        foreach (var data in _weapons)
        {
            _currentWeaponPrefab = data.PrefabGun;
            _currentWeaponLink = Instantiate(_currentWeaponPrefab, _spawnWeaponHolder);
            _weaponSystem.CurrentWeapon = _currentWeaponLink;
        }
    }
   
   private void AssignWeaponData()
   {
       foreach (var data in _weapons)
       {
           _weaponSystem.ReloadTime = data.ReloadTime;
           _weaponSystem.MagazineSize = data.MagazineSize;
           _weaponSystem.Damage = data.Damage;
           _weaponSystem.WeaponType = data.SelectWeapon;

           //WeaponIconAssign(ne rabotaet)
           foreach (var sprite in _sprites)
           {
               if(sprite.Icon == data.Icon)
                   _weaponSystem.WeaponIcon = sprite.Icon;
           }
       }
   }
}
