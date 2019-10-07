﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(SphereCollider))]
public class BlackHole : MonoBehaviour
{
   
   
    [SerializeField] public float GRAVITY_PULL = .78f;  //If you need to use this, make sure you have a sphere (Check on trigger) and make the radius of the trigger you zone of gravity.
    public bool CanAffectPlayer;
    public bool CanAffectAnything;
    
      public static float m_GravityRadius = 1f;
      void Awake()
      {
         m_GravityRadius = GetComponent<SphereCollider>().radius;
      }
   
      void OnTriggerStay(Collider other)
      {
         if (CanAffectAnything == true)
         {
            if (other.attachedRigidbody)
            {
               float gravityIntensity =
                  Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
               other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity *
                                                other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
               Debug.DrawRay(other.transform.position, transform.position - other.transform.position);
            }
         }

         if (CanAffectPlayer == true)
         {
            Rigidbody Player = other.gameObject.GetComponent<Rigidbody>();
            if (Player != null)
            {
               float gravityIntensity =
                  Vector3.Distance(transform.position, Player.transform.position) / m_GravityRadius;
               other.attachedRigidbody.AddForce((transform.position - Player.transform.position) * gravityIntensity *
                                                other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
               Debug.DrawRay(Player.transform.position, transform.position - Player.transform.position);
            }
         }
      }
   }
   
/// <summary>
/// Attract objects towards an area when they come within the bounds of a collider.
/// This function is on the physics timer so it won't necessarily run every frame.
/// </summary>
/// <param name="other">Any object within reach of gravity's collider</param>