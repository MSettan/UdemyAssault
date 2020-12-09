using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement: MonoBehaviour
{
    [Header("Movement Configure")]
    [Tooltip("In mps")][SerializeField] float xSpeed = 100f;
    [Tooltip("In mps")] [SerializeField] float ySpeed = 100f;
    [Tooltip("In m")] [SerializeField] float xRange = 4.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("Rotation Configure")]
    [SerializeField] float PositionPitchFactor = -3f;
    [SerializeField] float ControlPitchFactor = -5f;
    [SerializeField] float PositionYawFactor = 2f;
    [SerializeField] float ControlYawFactor = 6f;
    [SerializeField] float PositionRollFactor = -20f;


    float xThrow, yThrow;
    bool ControlCheck = true;

    // Update is called once per frame
    void Update()
    {
        if (ControlCheck)
        {
            Movement();
            Rotation();
            Firing();
        }
    }

    private void Rotation()
    {
        float pitch = transform.localPosition.y * PositionPitchFactor + yThrow*ControlPitchFactor;
        float yaw = transform.localPosition.x * PositionYawFactor + xThrow * ControlYawFactor;
        float roll = xThrow * PositionRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Movement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void PlayerDeath() // called by string method 
    {
        ControlCheck = false;
    }

    private void Firing()
    {
        if (CrossPlatformInputManager.GetButton("fire"))
        {

            foreach (GameObject gun in guns)
            {
                ParticleSystem.EmissionModule emission = gun.GetComponent<ParticleSystem>().emission;
                emission.enabled = true;
            }
        }
        else
        {

                foreach (GameObject gun in guns)
                {
                    ParticleSystem.EmissionModule emission = gun.GetComponent<ParticleSystem>().emission;
                    emission.enabled = false;
                }
        }
    }

}
