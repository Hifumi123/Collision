using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mass = 1.0f;

    public float friction = 0.2f;

    public float radius = 0.5f;

    [NonSerialized]
    public Vector3 curVelocity;

    [NonSerialized]
    public Vector3 prePosition;

    protected float m_MotionThreshold = 0.1f;

    void Start()
    {
        curVelocity = Vector3.zero;

        prePosition = transform.position;
    }

    void FixedUpdate()
    {
        //计算摩擦力。
        Vector3 frictionForce = Vector3.zero;
        if (curVelocity.magnitude > m_MotionThreshold)
            frictionForce = -friction * curVelocity.normalized;
        else
        {
            curVelocity = Vector3.zero;

            return;
        }

        //计算加速度。
        Vector3 acceleration = frictionForce / mass;

        prePosition = transform.position;

        //计算速度和位置。
        Vector3 newVelocity = curVelocity + Time.deltaTime * acceleration;
        transform.Translate((newVelocity + curVelocity) * Time.deltaTime / 2);

        curVelocity = newVelocity;
    }
}
