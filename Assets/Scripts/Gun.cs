using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector3 _position;
    [SerializeField] GameObject _boolet;
    [SerializeField] private float _shootForce = 10f;
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
            _position = gameObject.transform.position;
            
            //var target = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5.3f));
            //var targetDirection = target - _position; --- to shot at mouse position

            var targetDirection = gameObject.transform.forward; //to shot forward

            var boolet = Instantiate(_boolet, _position, Quaternion.identity );
            var booletBody = boolet.GetComponent<Rigidbody>();

            booletBody.AddForce(targetDirection * _shootForce); 
        }
    }
}
