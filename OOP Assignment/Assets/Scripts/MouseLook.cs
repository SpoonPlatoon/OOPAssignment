using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.FirstPerson.Camera
{
    [AddComponentMenu("Player/Camera")]
    public class MouseLook : MonoBehaviour
    {
        [Header("Sensitivity")] //Header in the inspector of unity to classify these variables from the others
        public float sensitivityX = 6; //preset sensitivity for the X-axis
        public float sensitivityY = 6; //preset sensitivity for the Y-axis
        [Header("Max and Min Y")] //Header in the inspector of unity to classify these variables from the others
        public float minimumY = -60; //minimun y axis as to not allow for flipping
        public float maximumY = 60; //maximun y axis ' '
        float rotationY = 0;
        [Header("Rotation Type")] //Header in the inspector of unity to classify these variables from the others
        public RotationalAxis axis = RotationalAxis.MouseX;
        public enum RotationalAxis
        {
            MouseXAndY,
            MouseX,
            MouseY
        }
        void Start()
        {
            Cursor.visible = false;
        }
        void Update()
        {
            if (axis == RotationalAxis.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
