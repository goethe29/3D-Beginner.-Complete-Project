using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBurst : MonoBehaviour
{
    [SerializeField] private float _lifeTimeInSec = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SelfDestroy), _lifeTimeInSec);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
