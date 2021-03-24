using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
   private float speedRotation = 10f;
   private float speed = 5f;
   private Vector2 _movementInput;
   private Vector2 _rotationInput;
   private bool reset = false;
   
   private void Update()
   {
      transform.Translate(new Vector3(_movementInput.x, 0, _movementInput.y) * speed * Time.deltaTime, Space.World);
      transform.Rotate(0, _rotationInput.x, 0);

      if (reset)
      {
         SceneManager.LoadScene("SampleScene");
      }
   }

   /*private void FixedUpdate()
   {
      transform.Rotate(0, _rotationInput.x, 0);
   }*/

   public void OnMove(InputAction.CallbackContext ctx) => _movementInput = ctx.ReadValue<Vector2>();

   public void OnRotate(InputAction.CallbackContext ctx) => _rotationInput = ctx.ReadValue<Vector2>();
   
   public void OnReset(InputAction.CallbackContext ctx)
   {
      reset = ctx.action.triggered;
   }
}
