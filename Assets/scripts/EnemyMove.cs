using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
    }
    private void enemyMove()
    {
        if (isFacingRight())
        {
            myrigidbody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            myrigidbody.velocity = new Vector2(-moveSpeed, 0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myrigidbody.velocity.x)), 1f);
    }

    bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
