using Unity.VisualScripting;
using UnityEngine;

public class Homing : MonoBehaviour
{
    public GameObject target; // �÷��̾�
    public float Speed = 3f;
    
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        // �÷��̾� �±׷� ã��
        target = GameObject.FindGameObjectWithTag("Player");

        // A - B: A�� �ٶ󺸴� ���� (�÷��̾� - �̻���)
        dir = target.transform.position - transform.position;
        // ���� ����(���� ����)�� ���ϱ�
        dirNo = dir.normalized; // ���͸� ����ȭ(normalize): ������ ���̰� 1�� ����, ���⸸ ����
    }

    void Update()
    {
        /* ��� �Ѿ� ���� (Upadate�� ������ ������ �ǽð����� �ٲ�� ������)
        // A - B: A�� �ٶ󺸴� ���� (�÷��̾� - �̻���)
        dir = target.transform.position - transform.position;
        // ���� ���͸� ���ϱ� (���� ����)
        dirNo = dir.normalized;
        */

        transform.Translate(dirNo * Speed * Time.deltaTime);

        // ����Ƽ ���� �Լ��� ���� ����
        // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            // Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
} 