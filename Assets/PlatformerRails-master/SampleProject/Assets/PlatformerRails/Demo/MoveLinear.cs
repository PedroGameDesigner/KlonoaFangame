using UnityEngine;
using PlatformerRails;

[RequireComponent(typeof(MoverOnRails))]
public class MoveLinear : MonoBehaviour {
    [SerializeField]
    Vector3 Velocity;

    MoverOnRails Controller;
    void Start()
    {
        Controller = GetComponent<MoverOnRails>();
        Controller.Velocity = this.Velocity;
    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(
            -Controller.Position.x * 5f,
            Controller.Velocity.y,
            Controller.Velocity.z);
        Controller.Velocity = velocity;
    }
}
