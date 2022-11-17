using UnityEngine;

public class Gun : GunScript
{
    [SerializeField] Camera _characterCamera;
    private PlayerController _parentController;
    string _lastColor;
    string _bulletType;

    private void Awake()
    {
        _parentController = transform.root.GetComponent<PlayerController>();
    }
    public override void Fire(string bulletColor, string bulletType)
    {
        _lastColor = bulletColor;
        _bulletType = bulletType;
        Shoot();
    }

    private void Shoot()
    {
        Debug.Log(Info.name + "Fire. BulletColor is" + _lastColor);
        Ray ray = _characterCamera.ViewportPointToRay(new Vector3(.5f, .5f));
        ray.origin = _characterCamera.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.GetComponent<IDamagable>() != null)
        {
            hit.collider.GetComponent<IDamagable>().ShootByPlayer(_lastColor, _bulletType);
            if (hit.collider.gameObject.GetComponent<TargetScript>()._color == _lastColor && hit.collider.gameObject.GetComponent<TargetScript>()._type == _bulletType)
                SetScore(true);
            else
                SetScore(false);
        }
    }
    private void SetScore(bool UpDown)
    {
        if (UpDown)
            _parentController.Score += 10;
        else
            _parentController.Score -= 10;

        _parentController.SetScore();
    }
}
