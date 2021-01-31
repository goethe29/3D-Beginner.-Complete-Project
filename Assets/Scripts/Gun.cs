using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector3 _position;
    [SerializeField] GameObject _bullet;
    [SerializeField] private float _shootForce = 5000f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    void Shot() 
    {
        _position = gameObject.transform.position;
            
            //var target = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.3f));
            //var targetDirection = target - _position; --- to shot at mouse position

            var targetDirection = gameObject.transform.forward; //to shot forward

            var bullet = Instantiate(_bullet, _position, Quaternion.identity );
            var bulletBody = bullet.GetComponent<Rigidbody>();

            bulletBody.AddForce(targetDirection * _shootForce); 
    }
}
