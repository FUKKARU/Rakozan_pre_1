using Ex;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomMaze
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] GameObject cam;

        Rigidbody rb;
        float x, z;
        Quaternion cameraRot, characterRot;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            cameraRot = cam.transform.localRotation;
            characterRot = transform.localRotation;
        }

        void Update()
        {
            if (GameManager.Instance.IsClear) { GetComponent<PlayerMove>().enabled = false; }

            float xRot = UnityEngine.Input.GetAxis("Mouse X");
            float yRot = UnityEngine.Input.GetAxis("Mouse Y");

            cameraRot *= Quaternion.AngleAxis(-yRot * PlayerParamsSO.Entity.Sensitivity[1], Vector3.right);
            characterRot *= Quaternion.AngleAxis(xRot * PlayerParamsSO.Entity.Sensitivity[0], Vector3.up);

            //Update�̒��ō쐬�����֐����Ă�
            cameraRot = ClampRotation(cameraRot);

            cam.transform.localRotation = cameraRot;
            transform.localRotation = characterRot;

            // �S�[���̖ڂ̑O�܂Ń��[�v
            if (KeyCode.Alpha0.Down())
            {
                transform.position = new Vector3(0f, 1.5f, 120f);
            }
        }

        private void FixedUpdate()
        {
            x = UnityEngine.Input.GetAxisRaw("Horizontal"); // * speed;
            z = UnityEngine.Input.GetAxisRaw("Vertical"); // * speed;

            float _speed = PlayerParamsSO.Entity.Speed;
            if (KeyCode.LeftShift.Stay()) _speed *= PlayerParamsSO.Entity.SpeedCoef;
            rb.velocity = (transform.forward * z + transform.right * x).normalized * Time.deltaTime * _speed;
        }

        //�p�x�����֐��̍쐬
        public Quaternion ClampRotation(Quaternion q)
        {
            //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1f;

            float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

            angleX = Mathf.Clamp(angleX, PlayerParamsSO.Entity.CameraRotXLimit[0], PlayerParamsSO.Entity.CameraRotXLimit[1]);

            q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

            return q;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.HasTag("Goal"))
            {
                GameManager.Instance.IsClear = true;
            }
        }
    }
}
