using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeController : MonoBehaviour
{
    private static TimeController instance;

    public static TimeController Instance { get { return instance; } }

    public float slowMontionTimeScale = 0.3f;
    public float slowMotionDuration = 0.5f; // ���ο� ��� ���ӽð�
    private float slowMotionTimer = 0f; // Ÿ�̸�

    public bool isSlowMotion { get; private set; }

    [Header("Post Processing")]
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private ColorGrading colorGrading;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        // Post Processing ������Ʈ ��������
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }
    void Update()
    {
        if (isSlowMotion)
        {
            slowMotionTimer += Time.deltaTime;
            if (slowMotionTimer >= slowMotionDuration)
            {
                SetSlowMotion(false);
                slowMotionTimer = 0f;
            }
        }
    }

    // ���ο� ȿ���� ����ϱ�
    public float GetTimeScale()
    {
        return isSlowMotion ? slowMontionTimeScale : 1f;
    }

    public void SetSlowMotion(bool slow)
    {
        isSlowMotion = slow;
        if (slow)
        {
            // ���ο� ��� ���� �� ȿ�� ����
            slowMotionTimer = 0f;
            vignette.intensity.value = 0.8f;         // ���Ʈ ���� ���� ����
            colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
            colorGrading.saturation.value = -40f;    // ä�� ���� ����
            colorGrading.temperature.value = -25f;    // �ſ� ������ ����
            colorGrading.contrast.value = 20f;        // ��� �� ���ϰ�
            colorGrading.postExposure.value = -1.0f;  // ��ü������ �� ��Ӱ�
            colorGrading.tint.value = 10f;           // �ణ�� �ʷϺ� �߰�
        }
        else
        {
            // ���ο� ��� ���� �� ȿ�� �ʱ�ȭ
            vignette.intensity.value = 0f;
            colorGrading.saturation.value = 0f;
            colorGrading.temperature.value = 0f;
            colorGrading.contrast.value = 0f;
            colorGrading.postExposure.value = 0f;
            colorGrading.tint.value = 0f;
        }
    }
}
