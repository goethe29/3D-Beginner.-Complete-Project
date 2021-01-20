using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private GameEvent _onEnemyDie;

    public void Hurt(int damage)
    {
        print("Ouch: " +damage);

        _health -= damage;

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
}
