using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Target : MonoBehaviour {
    //serialized variables
    [SerializeField]
    private GameObject _targetExplosionPrefab;

    //private variables
    private GameObject _explosion;

    public void ExplodeTarget()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //destroy target and explosion
        yield return new WaitForSeconds(2);
        _explosion = Instantiate(_targetExplosionPrefab) as GameObject;
        _explosion.transform.position = transform.position;
        Destroy(gameObject);
        Destroy(_explosion, 4.0f);
        AudioSource audio = _explosion.GetComponent<AudioSource>();
        audio.Play();
        GameController.Instance.TargetDestroyed = true;
    }
}

