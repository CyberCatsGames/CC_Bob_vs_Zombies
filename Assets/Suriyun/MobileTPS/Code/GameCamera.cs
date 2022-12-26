using UnityEngine;
using TouchScript.Gestures;

namespace Suriyun.MobileTPS
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private float _leftBound = 120;
        [SerializeField] private float _rightBound = 260;
        [HideInInspector] public GameObject player;
        public Transform target;
        public Vector3 offset_pos;
        public float smoothness = 1.66f;
        private Transform trans;

        public Transform aimer;
        public ScreenTransformGesture screen_gesture;

        [Space(5f)] [SerializeField] private float _pcRotationSpeed = 8f;
        public float _mobile_screen_rotation_speed = 0.33f;
        [Space(5f)] public float screen_rotation_smoothness = 16.66f;

        public bool zoomed = false;
        public float zoomed_speed_multiplier = 0.33f;
        public float zoom_speed = 6f;
        public float fov_zoom = 30;
        public float fov_normal = 60;
        private Camera cam;

        private bool _isBlocked;

        public bool IsBlocked => _isBlocked;

        private void Start()
        {
            trans = transform;
            aimer.rotation = trans.rotation;
            cam = GetComponent<Camera>();
            player = FindObjectOfType<Agent>().gameObject;
            BlockRotate();
        }

        public void BlockRotate()
        {
            Cursor.visible = true;
            _isBlocked = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void UnblockRotate()
        {
            Cursor.visible = false;
            _isBlocked = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (_isBlocked == true)
                return;

            if (Application.isMobilePlatform == false)
            {
                aimer.position = Vector3.back;

                Vector3 rotate_horizontal = Vector3.up * (Input.GetAxis("Mouse X") * _pcRotationSpeed);
                Vector3 rotate_vertical = aimer.right * (-1f * Input.GetAxis("Mouse Y") * _pcRotationSpeed);

                if (zoomed)
                {
                    rotate_horizontal *= zoomed_speed_multiplier;
                    rotate_vertical *= zoomed_speed_multiplier;
                }

                Quaternion tmp = aimer.rotation;
                aimer.Rotate(rotate_vertical, Space.World);
                if (aimer.up.y < 0)
                {
                    aimer.rotation = tmp;
                }

                aimer.Rotate(rotate_horizontal, Space.World);
            }

            // Zoom control //
            if (zoomed)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov_zoom, zoom_speed * Time.deltaTime);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov_normal, zoom_speed * Time.deltaTime);
            }

            // Aim position //
            aimer.position = trans.position;

            Vector3 pos = player.transform.position + aimer.forward * offset_pos.z + aimer.up * offset_pos.y +
                          aimer.right * offset_pos.x;
            pos.y = Mathf.Clamp(pos.y, player.transform.position.y, pos.y + 1);
            trans.position = Vector3.Slerp(
                trans.position,
                pos,
                smoothness * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Raycast(trans.position, trans.forward, out hit))
            {
                target.transform.position = hit.point;
            }
            else
            {
                target.transform.position = trans.position + trans.forward * 20;
            }

            // Rotation control //

            Vector3 eulerAngles = aimer.rotation.eulerAngles;
            eulerAngles.y = Mathf.Clamp(eulerAngles.y, _leftBound, _rightBound);
            eulerAngles.z = 0f;
            aimer.rotation = Quaternion.Euler(eulerAngles);

            trans.rotation =
                Quaternion.Slerp(trans.rotation, aimer.rotation, screen_rotation_smoothness * Time.deltaTime);
        }

        private void OnEnable()
        {
            screen_gesture.Transformed += FullScreenHandler;
        }

        private void OnDisable()
        {
            screen_gesture.Transformed -= FullScreenHandler;
        }

        private void FullScreenHandler(object sender, System.EventArgs e)
        {
            if (Application.isMobilePlatform == false)
            {
                return;
            }

            // Aim camera //
            Vector3 rotate_horizontal = Vector3.up * screen_gesture.DeltaPosition.x * _mobile_screen_rotation_speed;
            Vector3 rotate_vertical =
                aimer.right * (-1f) * screen_gesture.DeltaPosition.y * _mobile_screen_rotation_speed;

            if (zoomed)
            {
                rotate_horizontal *= zoomed_speed_multiplier;
                rotate_vertical *= zoomed_speed_multiplier;
            }

            Quaternion tmp = aimer.rotation;
            aimer.Rotate(rotate_vertical, Space.World);
            if (aimer.up.y < 0)
            {
                aimer.rotation = tmp;
            }

            aimer.Rotate(rotate_horizontal, Space.World);
        }
    }
}