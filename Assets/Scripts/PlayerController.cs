using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerController : MonoBehaviour
{
   private float _speed = 5f;
   private Vector2 _rotationInput;
   public int index = 0;
   
   
   private void Awake()
   {
      InputManager.OnDeviceAttached += inputDevice => Debug.Log("Attached: " + inputDevice.Name);
      InputManager.OnDeviceDetached += inputDevice => Debug.Log("Detached: " + inputDevice.Name);
      InputManager.OnActiveDeviceChanged += inputDevice => Debug.Log("Switched: " + inputDevice.Name);
   }

   private void Update()
   {
      if (InputManager.Devices.Count <= index)
      {
          return;
      }

      var device = InputManager.Devices[index];
      transform.Translate(new Vector3(device.LeftStickX * _speed * Time.deltaTime, 0, 0), Space.World);
      transform.Translate(new Vector3(0, 0, device.LeftStickY * _speed * Time.deltaTime), Space.World);

      {
         SelfRotation();
      }
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
