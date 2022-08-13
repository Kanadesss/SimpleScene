using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour {
  public Player player;

  /*void Start() {
    player = player == null ? player.GetComponent<Player>() : player;
    if (player == null) {
      Debug.LogError("player is not set in PlayerKeyboardController");
    }
  }*/

  private float _horizontalMove;
  private bool _jump;


  private void Update() {
    //Debug.Log(horizontalMove);
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
