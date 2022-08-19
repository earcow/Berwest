using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Заземление персонажа")]
    [Tooltip("Флаг показывающий заземлён ли персонаж. Не часть проверки заземления компонента CharacterController")]
    public bool IsGrounded = true;

    [Tooltip("Гестерейзис определения (для случае когда под ногами неровная поверхность)")]
    public float GroundedOffset = -0.14f;
    [Tooltip("Радиус проверочной сферы заземления. Должен совпадать с радиусом CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("Какие слои распознаются как земля")]
    public LayerMask GroundLayers;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        GroundedCheck();
    }


    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (IsGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
