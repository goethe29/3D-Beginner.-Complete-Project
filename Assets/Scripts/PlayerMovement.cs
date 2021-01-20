using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 20f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForce = 150f;
    private int _maxJumpNum = 2;
    private bool _isGrounded = true;
    private int _jumpCount = 0;

    private float _horizontal;
    private float _vertical;
    private bool _jump;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
    }

    private void Update() {
        _horizontal = Input.GetAxis ("Horizontal");
        _vertical = Input.GetAxis ("Vertical");
        _jump = Input.GetButtonDown("Jump");

        if (_jump && _jumpCount < _maxJumpNum)
        {
            m_Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _jumpCount++;
        }
    }
    
    void FixedUpdate ()
    {       
        m_Movement.Set(_horizontal, 0f, _vertical);
        m_Movement.Normalize ();
        m_Movement *= _moveSpeed;

        bool hasHorizontalInput = !Mathf.Approximately (_horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (_vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        if (isWalking && _isGrounded)
        {
            m_Animator.SetBool("IsWalking", isWalking);
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
            m_Animator.SetBool("IsWalking", false);
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, _turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void OnAnimatorMove ()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumpCount = 0;
        }  
    }
}