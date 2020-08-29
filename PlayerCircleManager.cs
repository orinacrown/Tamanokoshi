using UnityEngine;
using System.Collections;

public class PlayerCircleManager : MonoBehaviour
{
    private TimeFlagger survivalTimeFlag;
    private readonly int survivalTimeMS = 2000;
    private bool aliveFlag;

    private CircleCollider2D playerCollider;
    private SpriteRenderer playerSprite;

    // Use this for initialization
    void Start()
    {
        survivalTimeFlag = new TimeFlagger(survivalTimeMS);
        aliveFlag = true;
        playerCollider = GetComponent<CircleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((survivalTimeFlag.IsTimeOver() == true) || (Input.GetMouseButton(0) == false) || (aliveFlag == false))
        {
            Death();
        }
        Vector3 scale = transform.localScale;
        scale.x += 0.2f;
        scale.y += 0.2f;
        transform.localScale = scale;

    }

    private void Death()
    {
        aliveFlag = false;
        playerCollider.enabled = false;
        Color playerColor = playerSprite.color;
        playerColor.a -= 0.1f;
        if (playerColor.a <= 0)
        {
            Destroy(gameObject);
        }
        playerSprite.color = playerColor;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Wall"))
        {
            return;
        }
        if (collision.tag.Equals("Sphere"))
        {
            if (transform.position == collision.transform.position)
            {
                return;
            }
            float distance= Vector2.Distance(collision.transform.position, transform.position);
            Vector2 force = (collision.transform.position - transform.position) / distance;
            collision.GetComponent<Rigidbody2D>().AddForce(force * 3);
        }
    }
}
