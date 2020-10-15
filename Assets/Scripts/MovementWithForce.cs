using UnityEngine;

public class MovementWithForce : Movement
{
    public float force = 2.0f;

    void FixedUpdate()
    {
        //计算用户施加的力。
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 playerForceDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerForceDirection.Normalize();
        Vector3 playerForce = force * playerForceDirection;

        //计算摩擦力。
        Vector3 frictionForce = Vector3.zero;
        if (curVelocity.magnitude >= m_MotionThreshold)
            frictionForce = -friction * curVelocity.normalized;
        else if (playerForce.magnitude == 0)
        {
            curVelocity = Vector3.zero;

            return;
        }

        //计算合力。
        Vector3 jointForce = playerForce + frictionForce;

        //计算加速度。
        Vector3 acceleration = jointForce / mass;

        prePosition = transform.position;

        //计算速度和位置。
        Vector3 newVelocity = curVelocity + Time.deltaTime * acceleration;
        transform.Translate((newVelocity + curVelocity) * Time.deltaTime / 2);

        curVelocity = newVelocity;
    }
}
