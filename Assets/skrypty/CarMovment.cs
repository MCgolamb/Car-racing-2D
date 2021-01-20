using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovment : MonoBehaviour
{
   public float accelaration;
   public float sterring;
   public int speedMeter;
   private Rigidbody2D carRB;
   public TrailRenderer[] tireMarks;
   public GameObject Text;
   public Vector2 centerofmass;

   void Start()

   {
       carRB = GetComponent<Rigidbody2D>();
       carRB.centerOfMass = centerofmass;

   }

   public void FixedUpdate()
   {
       carMovment();
       speedMeter = (int)Vector2.Dot(carRB.velocity, transform.up);
       Text.GetComponent<Text>().text = "Prędkość:" + (int)speedMeter;
   }

   public void carMovment()
   {
       float x = -Input.GetAxis("Horizontal");
       float y = Input.GetAxis("Vertical");

       Vector2 speed = transform.up * (y * accelaration);
       carRB.AddForce(speed);

       float direction = Vector2.Dot(carRB.velocity, carRB.GetRelativeVector(Vector2.up));
       if (direction >= 0.0f)
       {
           stopEmiter();
           carRB.rotation += x * sterring * (carRB.velocity.magnitude / 5.0f);
       }
       else
       {
           startEmiter();
           carRB.rotation += x * sterring * (carRB.velocity.magnitude / 5.0f);
       }

       Vector2 forward = new Vector2(0.0f, 0.5f);
       float steeringRightAngle;
       if (carRB.angularVelocity > 0)
       {
           steeringRightAngle = -90;
       }
       else
       {
           steeringRightAngle = 90;
       }

       Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
       float driftForce = Vector2.Dot(carRB.velocity, carRB.GetRelativeVector(rightAngleFromForward.normalized));


       Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);
       carRB.AddForce(carRB.GetRelativeVector(relativeForce));
   }


   void startEmiter()
   {
      foreach (TrailRenderer T in tireMarks)
      {
          T.emitting = true;
      }
   }

   void stopEmiter()
   {
       foreach (TrailRenderer T in tireMarks)
       {
           T.emitting = false;
       }
   }

 
}
