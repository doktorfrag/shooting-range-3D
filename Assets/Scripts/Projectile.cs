using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    //serialized variables
    [SerializeField]
    private GameObject _explosionPrefab;

    //public variables
    public float explosionRadius = 1.5f;
    public float explosivePower = 20.0f;

    //private variables
    private GameObject _explosion;
    //private bool _targetHit;
    //private bool _targetNearHit;
    private int _projectileNumber;
    private List<string> _projectileData = new List<string>();

    //private variable accessors
    private bool _projectileFired = false;
    public bool ProjectileFired
    {
        get
        {
            return _projectileFired;
        }

        set
        {
            _projectileFired = value;
        }
    }

    //methods
    private void Awake()
    {
        GameController.Instance.ShotsFired = 1;
        _projectileNumber = GameController.Instance.ShotsFired;
    }

    private void Update()
    {
        if (_projectileFired == true)
        {
            string projectileNumber = _projectileNumber.ToString();
            double dateReturn = Math.Round((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            string timestamp = dateReturn.ToString();
            //Vector3 projectile = transform.TransformPoint(transform.position.x, transform.position.y, transform.position.z);
            string projectileX = transform.position.x.ToString();
            string projectileY = transform.position.y.ToString();
            string projectileZ = transform.position.z.ToString();
            string projectilePosition = projectileNumber + "," + timestamp + "," + projectileX + "," + projectileY + "," + projectileZ;

            //add new item to list on each update
            _projectileData.Add(projectilePosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Target" || collision.gameObject.name == "Target(Clone)")
        {
            GameController.Instance.TargetsHit = 1;
            //_targetHit = true;
        }

        //if (collision.gameObject.name == "Floor")
        //{
        //    Debug.Log("Landed on floor");
        //}
    }

    public void Shoot()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //wait for projectile to blow up
        yield return new WaitForSeconds(3);

        //destroy projectile and explosion
        Vector3 explosionPos = transform.position;
        _explosion = Instantiate(_explosionPrefab) as GameObject;
        _explosion.transform.position = explosionPos;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Destroy(gameObject);
        Destroy(_explosion, 4.0f);

        //see what explosion influences
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Target target = hit.GetComponent<Target>();
            if (rb != null)
            {
                float upwardsModifier = UnityEngine.Random.Range(2.0f, 4.0f);
                rb.AddExplosionForce(explosivePower, explosionPos, explosionRadius, upwardsModifier, ForceMode.Impulse);
                if (target)
                {
                    target.ExplodeTarget();
                    //_targetNearHit = true;
                }
            }
        }
        //data on whether projectile hits target or explodes within 1.5 of target
        //_projectileData.Add(_targetHit.ToString());
        //_projectileData.Add(_targetNearHit.ToString());
        //_projectileData.Add("----------");
        GameController.Instance.WriteData = _projectileData;
    }
}
