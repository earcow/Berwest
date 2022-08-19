using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("���������� ���������")]
    [Tooltip("���� ������������ ������� �� ��������. �� ����� �������� ���������� ���������� CharacterController")]
    public bool IsGrounded = true;

    [Tooltip("����������� ����������� (��� ������ ����� ��� ������ �������� �����������)")]
    public float GroundedOffset = -0.14f;
    [Tooltip("������ ����������� ����� ����������. ������ ��������� � �������� CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("����� ���� ������������ ��� �����")]
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
