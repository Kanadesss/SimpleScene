using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour {
  public Player player;
  private float _horizontalMove;
  private bool _jump;

  private void Update() {
    _horizontalMove = Input.GetAxisRaw("Horizontal");
    
    if (Input.GetKey(KeyCode.W)) {

      _jump = true;
    }
  }

  private void FixedUpdate() {
    player.Move(_horizontalMove * Time.fixedDeltaTime, _jump);
    _jump = false;
  }
}
