using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    Rigidbody2D playerBody;
    public float moveSpeed = 2f;
    public float jumpPower = 10f;
    private int extraJumps;
    public int extraJumpsAmount;
    Vector2 playerScale;
    Animator playerAnimController;
    public ParticleSystem dust;
    public bool isGrounded;
    public bool canJump;
    public LayerMask groundLayers;
    public Transform groundCheck;
    public float checkRadius;
    private int doubleJump;
    internal int collectedKeys;
    public Text keyCountText;
    public bool jumpPressed;
    public static Player instance;
    bool facingRight;
    public float currentHealth;
    public float maxHealth = 100;
    //public HealthBar healthbar;
    public bool isTakenDamage = false;
    public GameObject playerDeathPrefab;
    public GameObject gameOverPrefab;
    GameObject instantiatedGameOverPrefab;
    public GameObject canvas;


    private float minX, maxX, minY, maxY;
    void Start()
    {
        keyCountText = GameObject.FindWithTag("KeyText").GetComponent<Text>() as Text;
        canvas = GameObject.Find("Canvas");
        //Camera.main.aspect = 320f / 180f;
        instance = this;
        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
        playerAnimController = gameObject.GetComponent<Animator>();


        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;



    }

    // Update is called once per frame
    void Update()
    {

        if (isTakenDamage)
        {
            StartCoroutine(TakeDamageAnim());
        }
        playerAnimController.SetBool("isGrounded", isGrounded);
        playerAnimController.SetFloat("velocityY", playerBody.velocity.y);

        Vector3 pos = transform.position;

        // Horizontal contraint
        if (pos.x < minX) pos.x = minX;
        if (pos.x > maxX) pos.x = maxX;

        // vertical contraint
        if (pos.y < minY) pos.y = minY;
        if (pos.y > maxY) pos.y = maxY;

        // Update position
        transform.position = pos;


    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayers);
        
        Move();

        if (isGrounded)
            canJump = true;
        else
            canJump = false;

      
    }
    void Move()
    {
       
      


        if (Input.GetAxisRaw("Horizontal") > 0f || CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0f)
        {
            playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
            playerAnimController.SetBool("isWalking", true);

        }
        else if (Input.GetAxisRaw("Horizontal") < 0f || CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0f)
        {
            playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
            playerAnimController.SetBool("isWalking", true);

        }
        else
        {
            playerAnimController.SetBool("isWalking", false);
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsAmount;
            doubleJump = 0;
            playerAnimController.SetBool("isDoubleJump", false);
        }
        if (doubleJump == 2)
        {
            playerAnimController.SetBool("isDoubleJump", true);


        }
        if(playerBody.velocity.y<0)
            playerAnimController.SetBool("isDoubleJump", false);

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 || CrossPlatformInputManager.GetButtonDown("Jump") && extraJumps > 0)
        {
            
            playerBody.velocity = Vector2.up * jumpPower;
            //GetComponent<Rigidbody2D>().AddForce((Vector2.up * jumpPower), ForceMode2D.Force);
            playerAnimController.SetBool("isJumping", true);
            playerAnimController.SetBool("isWalking", false);
            playerAnimController.SetBool("isDoubleJump", false);
            extraJumps--;
            doubleJump++;
            createDust();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && canJump || CrossPlatformInputManager.GetButtonDown("Jump") && extraJumps == 0 && canJump)
        {
            
            playerBody.velocity = Vector2.up * jumpPower;
            //GetComponent<Rigidbody2D>().AddForce((Vector2.up * jumpPower), ForceMode2D.Force);
            playerAnimController.SetBool("isJumping", true);
            playerAnimController.SetBool("isWalking", false);
            playerAnimController.SetBool("isDoubleJump", false);


        }







        else if (isGrounded)
        {
            playerAnimController.SetBool("isJumping", false);
        }

        float move = Input.GetAxisRaw("Horizontal");
        float move_mobile = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        if (move > 0 && facingRight)
        {
            Flip();
        }
        else if (move < 0 && !facingRight)
        {
            Flip();
        }

        if (move_mobile > 0 && facingRight)
        {
            Flip();
        }
        else if (move_mobile < 0 && !facingRight)
        {
            Flip();
        }
        if (move < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (move > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            AudioManager.instance.PlaySound("CollectKeySound");
            Destroy(other.gameObject);
            collectedKeys += 1;
            RefreshKeyText(collectedKeys.ToString());
        }
        if (other.gameObject.tag == "EnemyFrog" || other.gameObject.tag=="Water")
        {
            PlayerDeath();

            /* if (!isTakenDamage)
             {
                 isTakenDamage = true;
             }
             TakeDamage(20);*/
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag=="CannonBall")
        {
            PlayerDeath();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
       
    }
    public void PlayerDeath()
    {
        instantiatedGameOverPrefab=Instantiate(gameOverPrefab, GameObject.FindGameObjectWithTag("LevelSpawnPoint").transform.position, GameObject.FindGameObjectWithTag("LevelSpawnPoint").transform.rotation);
        instantiatedGameOverPrefab.transform.localScale += new Vector3(0.3f, 0.3f, 1);
        instantiatedGameOverPrefab.transform.SetParent(canvas.transform);
        Instantiate(playerDeathPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
    public void RefreshKeyText(string keys)
    {

        keyCountText.text = keys;
    }
    void createDust()
    {
        dust.Play();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //healthbar.SetHealth(currentHealth);
    }
    public IEnumerator TakeDamageAnim()
    {
        for (int i = 0; i < 3; i++)
        {
            this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.2f);
        }

        isTakenDamage = false;

    }
}