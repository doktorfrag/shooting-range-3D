using UnityEngine;

public class Barrel : MonoBehaviour
{
    //serialized variables
    [SerializeField] private GameObject _projectilePrefab;

    //public variables

    //private variables
    private GameObject _projectile;
    private Rigidbody _rigidbody;
    private Projectile _projectileScript;
    private float _force = 1000f;
    private bool _loaded = false;

    void Update()
    {
        if (!_loaded && Input.GetMouseButtonDown(0))
        {
            //instantiate projectile and place at end of barrel
            _projectile = Instantiate(_projectilePrefab) as GameObject;
            _projectile.transform.position = transform.TransformPoint(Vector3.up * 1.0f);
            _projectile.transform.rotation = transform.rotation;
            _projectile.transform.parent = this.transform;
            _loaded = true;

            //shoot projectile
            _rigidbody = _projectile.GetComponent<Rigidbody>();
            _projectileScript = _projectile.GetComponent<Projectile>();
            if (_rigidbody)
            {
                AudioSource audio = GetComponent<AudioSource>();
                _projectileScript.ProjectileFired = true;
                audio.Play();
                _projectileScript.Shoot();
                _rigidbody.AddForce(_projectile.transform.up * _force);
                _rigidbody.drag = .5f;
                transform.DetachChildren();
                _loaded = false;
            }
        }
    }
}
