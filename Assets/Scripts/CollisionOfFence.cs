using UnityEngine;

public class CollisionOfFence : MonoBehaviour
{
    public GameObject sphere;

    public GameObject otherSphere;

    private bool m_HasCollidedWithSphere;

    private bool m_HasCollidedWithOtherSphere;

    private ContactPoint m_ContactPoint;

    void Start()
    {
        m_HasCollidedWithSphere = false;
        m_HasCollidedWithOtherSphere = false;
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject == sphere)
        {
            //print("In");
            m_HasCollidedWithSphere = true;

            m_ContactPoint = collision.GetContact(0);
        }
        else if (collision.gameObject == otherSphere)
        {
            m_HasCollidedWithOtherSphere = true;

            m_ContactPoint = collision.GetContact(0);
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.gameObject == sphere)
        {
            //print("Out");
            m_HasCollidedWithSphere = false;
        }
        else if (collision.gameObject == otherSphere)
        {
            m_HasCollidedWithOtherSphere = false;
        }
    }

    private void Collide(GameObject collision)
    {
        Movement movement = GetComponent<Movement>();

        Movement collisionMovement = collision.GetComponent<Movement>();

        float m1 = collisionMovement.mass;
        Vector3 v1 = collisionMovement.curVelocity;

        //print("In: " + v1);

        float m2 = movement.mass;
        Vector3 v2 = movement.curVelocity;

        Vector3 newDirection = Vector3.Reflect(v1, m_ContactPoint.normal);
        newDirection.y = 0;
        newDirection.Normalize();

        //collisionMovement.curVelocity = v1.magnitude * newDirection;
        //movement.curVelocity = Vector3.zero;

        //print("Out: " + collisionMovement.curVelocity);

        //使用动量守恒与能量守恒公式。
        collisionMovement.curVelocity = ((Mathf.Abs(m1 - m2) * v1.magnitude + 2 * m2 * v2.magnitude) / (m1 + m2)) * newDirection;
        movement.curVelocity = -((Mathf.Abs(m2 - m1) * v2.magnitude + 2 * m1 * v1.magnitude) / (m1 + m2)) * newDirection;

        //如果发生碰撞则位置回退以防止穿透。
        transform.position = movement.prePosition;
    }

    void FixedUpdate()
    {
        if (m_HasCollidedWithSphere)
        {
            Collide(sphere);

            m_HasCollidedWithSphere = false;
        }
        else if (m_HasCollidedWithOtherSphere)
        {
            Collide(otherSphere);

            m_HasCollidedWithOtherSphere = false;
        }
    }
}
