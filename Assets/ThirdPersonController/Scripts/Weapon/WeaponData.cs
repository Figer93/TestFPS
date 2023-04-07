using UnityEngine.VFX;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponData/New weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private SelectWeaponType _selectWeaponType;
    [SerializeField] private GameObject _prefabGun;
    [SerializeField] private Sprite _icon;
    [SerializeField] private VisualEffect _shootEffect;
    [SerializeField] private int _magazineSize;
    [SerializeField] private float _damage;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _weaponID;
    public SelectWeaponType SelectWeapon => _selectWeaponType;
    public GameObject PrefabGun => _prefabGun;
    public Sprite Icon => _icon;
    public int MagazineSize => _magazineSize;
    public float Damage => _damage;
    public float ReloadTime => _reloadTime;
    public int WeaponID => _weaponID;

    public enum SelectWeaponType
    {
        Riffle,
        Pistol,
        Sniper
    }
}
