using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionBehaviors : MonoBehaviour {

    public Collider2D Medalion;

    private void Start()
    {
        Medalion = GameObject.Find("Medalion1").GetComponent<Collider2D>();
    }

    void LateUpdate()
    {
        float pinchAmount = 0;
        Quaternion desiredRotation = transform.rotation;

        DetectTouchMovement.Calculate();

        if (Mathf.Abs(DetectTouchMovement.pinchDistanceDelta) > 0)
        { // zoom
            pinchAmount = DetectTouchMovement.pinchDistanceDelta;
            transform.localScale += new Vector3(.001f, .001f, 0) * pinchAmount;
            if (transform.localScale.x > 1.5f)
                transform.localScale = new Vector3(1.5f, 1.5f, 0);
            if (transform.localScale.x < 0.2f)
                transform.localScale = new Vector3(0.2f, 0.2f, 0);

        } else if (Input.touchCount == 1)
        {
            Touch Touch1 = Input.GetTouch(0);

            if (Touch1.phase == TouchPhase.Moved)
            {
                Vector3 Movement = new Vector3(Touch1.deltaPosition.x, Touch1.deltaPosition.y, transform.position.z) * 20;
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, transform.position + Movement, Time.deltaTime);
            }
        }

        if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
        { // rotate
            Vector3 rotationDeg = Vector3.zero;
            rotationDeg.z = -DetectTouchMovement.turnAngleDelta;
            desiredRotation *= Quaternion.Euler(-rotationDeg);
        }


        // not so sure those will work:
        transform.rotation = (desiredRotation);


        
    }

        // Update is called once per frame
        void Update () {
            
       // if(Input.GetMouseButton(0))
        //transform.position = Input.mousePosition;
		
	}
}
