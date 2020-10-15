using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject otherSphere;

    void FixedUpdate()
    {
        Movement movement = GetComponent<Movement>();

        Movement otherMovement = otherSphere.GetComponent<Movement>();

        //判断是否发生碰撞。
        if (Vector3.Distance(transform.position, otherSphere.transform.position) <= movement.radius + otherMovement.radius)
        {
            float m1 = movement.mass;
            Vector3 v1 = movement.curVelocity;

            float m2 = otherMovement.mass;
            Vector3 v2 = otherMovement.curVelocity;

            movement.curVelocity = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
            otherMovement.curVelocity = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

            //如果发生碰撞则位置回退以防止穿透。
            transform.position = movement.prePosition;
        }
    }
}
