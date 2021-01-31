using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private GameEvent _onEnemyDie;
    [SerializeField] float _freezeOnDamage = 1f;
    private NavMeshAgent _mover;

    private void Start() {
        _mover = GetComponent<NavMeshAgent>();
    }
    public void Hurt(int damage)
    {
        print("Ouch: " +damage);

        _health -= damage;

        StartCoroutine(FreezeOnDamage());

        if (_health <= 0)
        {
            Invoke("Die", 1);
        }
    }

    private void Die()
    {
        _onEnemyDie.Raise();
        Destroy(gameObject);
    }

    private IEnumerator FreezeOnDamage()
    {
        StopAllCoroutines();
        _mover.isStopped = true;
        yield return new WaitForSeconds(_freezeOnDamage);
        _mover.isStopped = false;
    }
}
