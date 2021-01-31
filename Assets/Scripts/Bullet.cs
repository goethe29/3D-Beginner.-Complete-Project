using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTimeInSec = 1f;
    [SerializeField] GameObject _burst;
    [SerializeField] int _damage = 1;

    private MeshRenderer _renderer;
    private Rigidbody _rigidbody;

    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        Invoke(nameof(SelfDestroy), _lifeTimeInSec);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) 
    {
        CancelInvoke(nameof(SelfDestroy));
        Instantiate(_burst, gameObject.transform.position, Quaternion.identity);
        
        var enemy = other.gameObject.GetComponent<MyEnemy>();
        if (enemy != null)
        {
            enemy.Hurt(_damage);
        }
        
        Destroy(gameObject);
    }
}
