using UnityEngine;

public class Stair : MonoBehaviour
{
    // �浹ó��
    // trigger �浹�� �Ͼ�� �� ���
    // Collison �浹�� �Ͼ�� �� ��� X

    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
