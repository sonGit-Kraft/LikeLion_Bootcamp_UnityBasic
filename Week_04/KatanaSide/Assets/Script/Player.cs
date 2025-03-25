using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� �Ӽ�")]
    public float speed = 5;
    public float jumpUp = 1;
    public float power;
    public Vector3 direction;
    public GameObject slash;

    // �׸���
    public GameObject Shadow1;
    List<GameObject> sh = new List<GameObject>();

    // ��Ʈ ����Ʈ
    public GameObject hit_lazer;

    bool bJump = false;
    Animator pAnimator;
    Rigidbody2D pRig2D;
    SpriteRenderer sp;

    void Start()
    {
        pAnimator = GetComponent<Animator>();
        pRig2D = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
        sp = GetComponent<SpriteRenderer>();
    }

    void KeyInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // GetAxisRaw: -1 0 1

        if (direction.x < 0)
        {
            // left
            sp.flipX = true;
            pAnimator.SetBool("Run", true);

            // ShadowFlip
            for (int i = 0; i < sh.Count; i++)
            {
                sh[i].GetComponent<SpriteRenderer>().flipX = sp.flipX;
            }
        }
        else if (direction.x > 0)
        {
            // right
            sp.flipX = false;
            pAnimator.SetBool("Run", true);

            // ShadowFlip
            for (int i = 0; i < sh.Count; i++)
            {
                sh[i].GetComponent<SpriteRenderer>().flipX = sp.flipX;
            }
        }
        else if (direction.x == 0)
        {
            pAnimator.SetBool("Run", false);

            for (int i = 0; i < sh.Count; i++)
            {
                Destroy(sh[i]); // ���ӿ�����Ʈ �����
                sh.RemoveAt(i); // ���ӿ�����Ʈ �����ϴ� ����Ʈ �����
            }
        }

        if (Input.GetMouseButtonDown(0)) // 0�� -> ���ʸ��콺
        {
            pAnimator.SetTrigger("Attack");
            Instantiate(hit_lazer, transform.position, Quaternion.identity);
        }
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(pRig2D.position, Vector3.down, new Color(0, 1, 0));

        // ����ĳ��Ʈ�� �� üũ
        RaycastHit2D rayHit = Physics2D.Raycast(pRig2D.position, Vector2.down, 1, LayerMask.GetMask("Ground"));

        if (pRig2D.linearVelocityY < 0) // Y�� �ӵ��� ������ �� (�������� ���� ��)
        {
            if (rayHit.collider != null) // ���� �浹�� ���
            {
                if (rayHit.distance < 0.7f) // ������ �Ÿ� 0.7 ������ ��
                {
                    pAnimator.SetBool("Jump", false); // ���� �ִϸ��̼� ����
                }
            }
        }
    }

    void Update()
    {
        KeyInput();
        Move();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (pAnimator.GetBool("Jump") == false)
            {
                Jump();
                pAnimator.SetBool("Jump", true);
            }
        }
    }

    public void Jump()
    {
        pRig2D.linearVelocity = Vector2.zero;
        pRig2D.AddForce(new Vector2(0, jumpUp), ForceMode2D.Impulse);
    }

    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void AttSlash()
    {
        if (sp.flipX == false) // �÷��̾� ������
        {
            pRig2D.AddForce(Vector2.right * power, ForceMode2D.Impulse);
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = sp.flipX;
        }
        else // �÷��̾� ����
        {
            pRig2D.AddForce(Vector2.left * power, ForceMode2D.Impulse);
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = sp.flipX;
        }
    }

    // �׸���
    public void RunShadow()
    {
        if (sh.Count < 6)
        {
            GameObject go = Instantiate(Shadow1, transform.position, Quaternion.identity);
            go.GetComponent<Shadow>().TwSpeed = 10 - sh.Count;
            sh.Add(go);
        }
    }
}