using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerController : MonoBehaviour
{
   private float speedRotation = 10f;
   private float speed = 5f;
   //private Vector2 _movementInput;
   private Vector2 _rotationInput;
   private bool reset = false;
   


   private void Update()
   {
      transform.Translate(new Vector3(InputManager.ActiveDevice.LeftStickX * speed * Time.deltaTime, 0, 0), Space.World);
      transform.Translate(new Vector3(0, 0, InputManager.ActiveDevice.LeftStickY * speed * Time.deltaTime), Space.World);

      if (InputManager.ActiveDevice.RightStick.Sensitivity > 0.5f)
      {
         _rotationInput = InputManager.ActiveDevice.RightStick.Value;
         transform.Rotate(new Vector3(0, _rotationInput.magnitude, 0));
      }

         if (InputManager.ActiveDevice.Command.IsPressed)
      {
         SceneManager.LoadScene("SampleScene");
      }
   }

   /*private void FixedUpdate()
   {
      transform.Rotate(0, _rotationInput.x, 0);
   }*/

   //public void OnMove(InputAction.CallbackContext ctx) => _movementInput = ctx.ReadValue<Vector2>();

   //public void OnRotate(InputAction.CallbackContext ctx) => _rotationInput = ctx.ReadValue<Vector2>();
   
   /*public void OnReset(InputAction.CallbackContext ctx)
   {
      reset = ctx.action.triggered;
   }*/

   private void MoveHorizontal()
   {
      transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
   }
   
   
}
