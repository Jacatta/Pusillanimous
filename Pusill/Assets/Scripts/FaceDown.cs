using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDown : MonoBehaviour {

    Rigidbody2D rb;
    Vector2 OGPOS;
    Vector3 newPos;
    Transform OG;
    float speed;
    GameObject targetArm;
    GameObject pivot;
    float rotationZ;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        speed = 20f;
        targetArm = this.gameObject;
        pivot = GameObject.Find("pivot");
        rotationZ = 0;

        OGPOS = targetArm.transform.position;

       // var Og = transform.rotation;
       //GameObject massCenter = GameObject.Find("reward Collider");
       //centerofM = (massCenter.transform.position);
       //rb.centerOfMass = centerofM;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotationZ = transform.rotation.z;
        //rotationZ = 0;// += Input.GetAxis("Vertical") * Time.deltaTime;
        rotationZ = Mathf.Clamp(rotationZ, -100f, 100f);
        //rotationY += Input.GetAxis("Horizontal" * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0,0, rotationZ);
        /*
        if (rb.angularVelocity > .5f )
        {
           // if (rb.angularVelocity == 0) targetArm.transform.rotation.z(OGPOS.z);

            rb.angularVelocity -= (rb.angularVelocity + rb.angularVelocity*.4f);
            Debug.Log("Arm has some rotation going on. COMMENCE ADJUSTMENT!");


        }else if(rb.angularVelocity < -.5f)
        {
            rb.angularVelocity += (rb.angularVelocity + rb.angularVelocity * .4f);
        }
        */

    }
    void Update () {
        //GetComponent<Transform>().transform.rotation.z -= transform.rotation.z ;
        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z / 1000);

        /*
        targetArm.transform.RotateAround(
            pivot.transform.position,
            pivot.transform.forward,
            speed * Time.deltaTime
        );
        */
        //rb.AddRelativeForce(Vector2.down*100  );
       // rb.AddForceAtPosition(Vector2.up * 10, new Vector2(transform.localPosition.x, transform.localPosition.y-5));//forward*-10, new Vector2(transform.localPosition.x, transform.localPosition.y));
        //transform.rotation = Quaternion.Lerp(transform.rotation, OG.rotation , Time.time * speed);
        // transform.localRotation = Quaternion.Euler(0,0, transform.rotation.z / 2);
        //rb.transform.rotation.z -= this.transform.rotation.z / 10;

    }
}
