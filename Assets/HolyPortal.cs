using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HolyPortal : MonoBehaviour
{
    [SerializeField] private int _damage;
    AudioSource m_AudioSource;
    NavMeshAgent mover;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<MyEnemy>();
            mover = other.GetComponent<NavMeshAgent>();

            m_AudioSource.Play();

            if (mover != null)
            {
                mover.isStopped = true;
            }
            
            Invoke("DestroyPortal", 1);
            enemy.Hurt(_damage);
        }
    }

    private void DestroyPortal()
    {
        Destroy(gameObject);
        if (mover != null)
        {
            mover.isStopped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_AudioSource.Play();
            Invoke("DestroyPortal", 1);
        }
    }
}
