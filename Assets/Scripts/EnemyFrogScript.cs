using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogScript : MonoBehaviour
{
    Rigidbody2D enemyFrogBody;
    public LayerMask enemyFrogMask;
    public float moveSpeed;
    Transform enemyFrogTransform;
    float enemyFrogWidth, enemyFrogHeight;
    public GameObject enemyDeathPrefab;
    GameObject insEnemyDeathPrefab;
    // Start is called before the first frame update
    void Start()
    {
        enemyFrogTransform = this.transform;
        enemyFrogBody = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer enemyFrogSprite = gameObject.GetComponent<SpriteRenderer>();
        enemyFrogWidth = enemyFrogSprite.bounds.extents.x;
        enemyFrogHeight = enemyFrogSprite.bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos = enemyFrogTransform.position - enemyFrogTransform.right * enemyFrogWidth + enemyFrogTransform.up * enemyFrogHeight * -.7f;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyFrogMask);
        Debug.DrawLine(lineCastPos, lineCastPos - Vector2.right * 0.03f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - Vector2.right * 0.03f, enemyFrogMask);

        if (!isGrounded || isBlocked)
        {
            Vector3 currentRotation = enemyFrogTransform.eulerAngles;
            currentRotation.y += 180;
            enemyFrogTransform.eulerAngles = currentRotation;

        }

        Vector2 enemyFrogVelocity = enemyFrogBody.velocity;
        enemyFrogVelocity.x = -enemyFrogTransform.right.x * moveSpeed;
        enemyFrogBody.velocity = enemyFrogVelocity;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Flash")
        {

            insEnemyDeathPrefab=Instantiate(enemyDeathPrefab, gameObject.transform.position, gameObject.transform.rotation);
            insEnemyDeathPrefab.transform.position= new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f);
            Destroy(gameObject);
        }
    }
}
