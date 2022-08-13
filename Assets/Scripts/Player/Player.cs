using UnityEngine;

public class Player : MonoBehaviour {
  enum MoveState {
    Idle,
    Walk,
    Jump,
  }

  [Header("Speeds")]
  [SerializeField] private float walkSpeed;
  [SerializeField] private float jumpForce;

  [Header("Ground")]
  [SerializeField] private Transform _groundCheck;
  [SerializeField] private LayerMask _WhatIsGround;

  private MoveState _moveState = MoveState.Idle;
  private Transform _transform;
  private Animator _animator;
  private Rigidbody2D _rigidbody;
  private float _groundCheckRadius = 0.2f;
  private bool _facingRight = true;
  private bool _isGrounded = true;
  private int _itemCount;

  public void Move(float walkDirection, bool jump) {
    Walk(walkDirection);

    if(jump) {

      Jump();
    }
    SetPlayerDirection(walkDirection);
  }

  public void AddItem(ItemPick item) {
    _itemCount += item.GetItemCount();
    Debug.Log("Items count = " + _itemCount);
  }

  private void Start() {
    _transform = GetComponent<Transform>();
    _animator = GetComponent<Animator>();
    _rigidbody = GetComponent<Rigidbody2D>();
    _itemCount = 0;
  }

  private void FixedUpdate() {
    _isGrounded = false;
    Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundCheckRadius, _WhatIsGround);
    
    foreach(Collider2D collider in colliders) {
      
      if(collider.gameObject != gameObject) {

        _isGrounded = true;
        StopJump();
      }
    }
  }

  private void Walk(float walkDirection) {
    if (_moveState != MoveState.Jump) {

      if(walkDirection == 0) {

        Idle();
      } else {        
        _moveState = MoveState.Walk;
        _animator.SetFloat("WalkSpeed", walkSpeed);
      }
    }
    Vector3 movement = new Vector3(walkDirection, 0, 0);
    _transform.Translate(movement * walkSpeed);
  }

  private void Jump() {
    if(_moveState != MoveState.Jump && _isGrounded) {

      _rigidbody.AddForce(Vector2.up * jumpForce);
      _moveState = MoveState.Jump;
      _animator.SetBool("IsJumping", true);
    }
  }

  private void StopJump() {
    if (_isGrounded && _moveState == MoveState.Jump) {

      _moveState = MoveState.Idle;
      _animator.SetBool("IsJumping", false);
      Idle();
    }
  }

  private void Idle() {
    _moveState = MoveState.Idle;
    _animator.SetFloat("WalkSpeed", 0);
    _animator.SetBool("IsJumping", false);
  }

  private void SetPlayerDirection(float direction) {
    if (direction > 0 && !_facingRight) {

      _facingRight = !_facingRight;
      Flip();
    } else if (direction < 0 && _facingRight) {
      _facingRight = !_facingRight;
      Flip();
    }
  }

  private void Flip() {
    Vector3 scale = _transform.localScale;
    scale.x *= -1;
    _transform.localScale = scale;
  }
}
