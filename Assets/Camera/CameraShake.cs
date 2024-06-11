using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; } // �̱��� �ν��Ͻ�

    public CinemachineVirtualCamera virtualCamera; // ī�޶� ���� ��ü
    private CinemachineBasicMultiChannelPerlin noise; // ī�޶� ��鸲 ����

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // �̹� �ٸ� �ν��Ͻ��� �����ϸ� ���� ��ü �ı�
    }

    private void Start()
    {
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 0f;
    }


    public void Shake(float duration, float power)
    {
        StartCoroutine(Recoil(duration, power));
    }

    private IEnumerator Recoil(float duration, float power)
    {
        noise.m_AmplitudeGain = power;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0f;
    }
}
