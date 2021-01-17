using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 20f;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _jumpForce = 1;
    private int _maxJumpNum = 2;
    private bool _isGrounded;
    private int _jumpCount = 0;

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

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        bool isJumping = Input.GetButtonDown("Jump");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();
        m_Movement *= _moveSpeed;

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
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

        if (isJumping && _jumpCount < _maxJumpNum)
        {
            m_Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _jumpCount++;
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    void OnAnimatorMove ()
    {

    }

    void OnCollisionStay()
    {
        _isGrounded = true;
        _jumpCount = 0;
    }
}