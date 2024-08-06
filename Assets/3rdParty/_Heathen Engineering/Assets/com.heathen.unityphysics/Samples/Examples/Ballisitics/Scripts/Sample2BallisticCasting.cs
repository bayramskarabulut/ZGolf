#if HE_SYSCORE
using System.Collections;
using HeathenEngineering.UnityPhysics;
using HeathenEngineering.UnityPhysics.API;
using UnityEngine;
using UnityEngine.UIElements;

namespace HeathenEngineering.Demos
{
    [System.Obsolete("This script is for demonstration purposes ONLY\nIt is specific to this example scene and simply handles user input.")]
    public class Sample2BallisticCasting : MonoBehaviour
    {
        private TrickShot trickShot;

        private Vector3 AimPosition;
        public RectTransform BallPosition;
        public LineRenderer lineRenderer;

        public float Drift = 20.0f;
        public float DriftSpeed = 20.0f;

        Vector3 DriftTarget = Vector3.zero;
        Vector3 NowDriftPosition = Vector3.zero;
        public float YaxisPosition = 0.5f;
        public GameObject ball;
        public float ballWaiteTime = 0.2f;

        private void Start()
        {
            trickShot = GetComponentInChildren<TrickShot>();
        }

        private void Update()
        {
            // if (Input.GetMouseButton(1))
            // {
            //     /***********************************************************************************
            //      * Right Mouse button held
            //      * Here we are simply moving the camera around so you can change your point of view
            //      ***********************************************************************************/
            //     var rotationX = Input.GetAxis("Mouse X");
            //     var rotationY = Input.GetAxis("Mouse Y");
            //     Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            //     Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            //     transform.localRotation = transform.localRotation * xQuaternion * yQuaternion;
            //     transform.LookAt(transform.position + transform.forward);
            // }
            //else if (!Input.GetKey(KeyCode.Space))
            
            if (Input.GetMouseButton(0))
            {
                /***************************************************************
                 * When the Spacebar is not being held down
                 * Here we check for a ray cast hit under the mouse pointer
                 * if a hit is found we calculate the solution and aim at it
                 * we then apply that rotation to the emitter transform
                 ***************************************************************/

                //Find the world point under the mouse by casting a ray from the camera through the mouse pointer
                
                if (DriftTarget == Vector3.zero || DriftTarget == NowDriftPosition)
                {
                    DriftTarget = new Vector3(Random.Range(-Drift, Drift), Random.Range(-Drift, Drift), 0);
                }
                NowDriftPosition = Vector3.MoveTowards(NowDriftPosition, DriftTarget, Time.deltaTime * DriftSpeed);
                
                
                if ((BallPosition.position - Input.mousePosition).y > 0)
                    AimPosition = new Vector3(2*BallPosition.position.x, (2.0f + YaxisPosition)*BallPosition.position.y, 0) - Input.mousePosition + NowDriftPosition;
                
                var ray = Camera.main.ScreenPointToRay(AimPosition);
                if(Physics.Raycast(ray, out var hit))
                {
                    //The ray cast hit something so we want to aim the emitter at it using the TrickShot's position, speed and constant acceleration
                    if (Ballistics.Solution(trickShot.transform.position, trickShot.speed, hit.point, trickShot.constantAcceleration, out Quaternion low, out Quaternion _) >= 1)
                    {
                        //We found at least 1 solution, so use the low angle as the rotation for the emitter this will cause it to "look" along the path such that the projectile will hit where the mouse is pointing
                        trickShot.transform.rotation = low;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                /********************************************************************************************************
                 * Left Mouse button held
                 * Simply shoot a projectile, this uses TrickShot to shoot so the projectile will be 
                 * deterministically controlled for the duration of the path according to the TrickShot settings.
                 ********************************************************************************************************/
                trickShot.Shoot();
                
                StartCoroutine(BallWaitOn());
            }
            else if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.enabled = true;
            }
            
        }

        public IEnumerator BallWaitOn()
        {
            ball.active = false;
            yield return new WaitForSeconds(ballWaiteTime);
            ball.active = true;
        }
    }
}

#endif