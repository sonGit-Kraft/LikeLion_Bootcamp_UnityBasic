using Unity.Cinemachine;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 5f; // �̻��� �ӵ�
    public float lifeTime = 3f; // �̻��� ���� �ð�
    public int damage = 10; // �̻��� ������
    public Vector2 direction; // �̻��� �̵� ����

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void Update()
    {
        float timeScale = TimeController.Instance.GetTimeScale();
        transform.Translate(direction * speed * Time.deltaTime * timeScale);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy")) // ���� �浹���� ��
        {
            // ���� ���� �ִϸ��̼� ����
            ShootingEnemy enemy = other.GetComponent<ShootingEnemy>();
            if (enemy != null)
            {
                enemy.PlayDeathAnimation();
            }

            // �̻��� ����
            Destroy(gameObject);
        }
    }
}
