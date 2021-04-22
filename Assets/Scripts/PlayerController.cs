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
   private Vector2 _rotationInput;
   [SerializeField] public int index = 0;
   


   private void Awake()
   {
      InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
      InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
      InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name);
   }

   private void Update()
   {
      var device = InputManager.Devices[index];
      transform.Translate(new Vector3(device.LeftStickX * speed * Time.deltaTime, 0, 0), Space.World);
      transform.Translate(new Vector3(0, 0, device.LeftStickY * speed * Time.deltaTime), Space.World);

      {
         SelfRotation();
      }
/*if (device.Command.IsPressed)
      {
         SceneManager.LoadScene("SampleScene");
      }*/
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

   private void SelfRotation()
   {
      var device = InputManager.Devices[index];
      var dir = device.RightStick.Value;

      if (dir.magnitude > 0.1f)
      {
         transform.rotation = Quaternion.Euler(new Vector3(0, -Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90.0f, 0));
      }
   }

   
}
