using UnityEngine;
using UnityEngine.AI;

public class JWPlayer : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public NavMeshAgent playerAgent; // �÷��̾��� NavMeshAgent
    public LayerMask clickableLayer; // Ŭ�� ������ ���̾� (�ͷ���)

    private void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                // Ŭ���� ������ ���� ��ǥ�� ����ϴ�.
                Vector3 targetPosition = hit.point;

                // NavMeshAgent�� ����Ͽ� �÷��̾ Ŭ���� �������� �̵���ŵ�ϴ�.
                playerAgent.SetDestination(targetPosition);
            }
        }
    }
}
