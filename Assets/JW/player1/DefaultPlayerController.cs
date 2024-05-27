using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class DefaultPlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public NavMeshAgent playerAgent;
    public Animator anim;
    public LayerMask clickableLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
        playerAgent = GetComponentInChildren<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }


    public Ray ray;
    public RaycastHit hit;
    public Vector3 targetPosition;
    void Update()
    {

        AnimationController();

        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                targetPosition = hit.point;

                playerAgent.SetDestination(targetPosition);

                Vector3 direction = (targetPosition - playerAgent.transform.position).normalized;

                Quaternion lookDirection = Quaternion.LookRotation(direction);

                playerAgent.transform.rotation = lookDirection;
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetTrigger("Evade");
        }
    }

    void AnimationController()
    {
        anim.SetBool("Move", IsMoving());
    }

    [SerializeField] private bool isMoving => IsMoving();

    private bool IsMoving()
    {
        // ������Ʈ�� �̵� ������ Ȯ��
        if (playerAgent.pathPending) return true;  // ��θ� ��� ���� ���
        if (playerAgent.remainingDistance > playerAgent.stoppingDistance) return true;  // ���������� ���� �Ÿ��� stoppingDistance���� ū ���
        if (playerAgent.velocity.sqrMagnitude > 0.1f * 0.1f) return true;  // ������Ʈ�� �����̰� �ִ� ���

        return false;  // �� ���� ��쿡�� �̵� ���� �ƴ�
    }

}
