using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    public float speed = 2.0f;
    public float jumpSpeed = 50f;
    float horizontal;
    float vertical;
    Vector2 lookDirection = new Vector2(1, 0);
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public float jumpForce;

    public GameObject tntWeapon;

    PlayerInputActions playerInputActions;
    Vector2 currentInput;

    public GameObject pauseGame;
    public GameObject uIHealth;

    private float health = 1;
    public Text[] numOfBomb;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //playerInputActions.Player.Launch.performed += LaunchProjectile;
        playerInputActions.Player.Movement.performed += OnMovement;
        playerInputActions.Player.Movement.canceled += OnMovement;
        //playerInputActions.Player.ChangeWeapon.performed += ChangeWeapon;

        //playerInputActions.Player.Talk.performed += TalkingWithJambi;
        playerInputActions.Player.Pause.performed += OnPauseGame;


        if (MyManager.Instance.isNewGame == false)
        {
            MyManager.Instance.LoadGame(this);
            MyManager.Instance.LoadAmmo();
        }

        pauseGame.SetActive(false);
        uIHealth.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround); ;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpSpeed * 2f  *  Time.deltaTime);
            rigidbody2d.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * jumpForce, ForceMode2D.Impulse);
        }

        Vector2 moveDirection = transform.position;
        moveDirection.x = moveDirection.x + 2f * horizontal * Time.deltaTime * speed;

        //Flip character when moving right/left
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        rigidbody2d.MovePosition(moveDirection);


        Vector2 move = new Vector2(horizontal, vertical);


        //set animator parameters
        animator.SetBool("running", horizontal != 0);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }



    }


    //modify later
    /*public void ChangeHealth(float amount)
    {
        health += amount;
        Debug.Log(health);
    }   */

    public void setHealth(float amount)
    {
        GetComponent<Health>().SetHealth(amount);
    }

    public float GetHealth()
    {
        return GetComponent<Health>().currentHealth;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentInput = context.ReadValue<Vector2>();
            //Debug.Log("Moving");
        }
        if (context.canceled)
        {
            currentInput = Vector2.zero;
        }
    }

    void ChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BowController.currentArrow += 1;
            if (BowController.currentArrow > 2)
            {
                BowController.currentArrow = 0;
            }
            Debug.Log("Change weapon");
        }
    }

    void OnPauseGame(InputAction.CallbackContext context)
    {
        MyManager.Instance.PauseGame();

        //Show menu
        pauseGame.SetActive(true);
    }

    //void OnMouseDown()
    //{
    //    GameObject tntObject = Instantiate(tntWeapon, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
    //    BomController tnt = tntObject.GetComponent<BomController>();
    //}


    //load game
    void AfterDie()
    {
        StartCoroutine(WaitAfterDie());



    }
    IEnumerator WaitAfterDie()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        BowController.numberOfAmmo[0] = 50;
        BowController.numberOfAmmo[1] = 20;
        BowController.numberOfAmmo[2] = 10;
    }

    //Collect item
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Item trigger");
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name.Equals("ItemBom01"))
            {
                Debug.Log("Item 1");
                BowController.numberOfAmmo[0] += 10;
                numOfBomb[0].text = BowController.numberOfAmmo[0].ToString();
            }
            else
            {
                if (collision.gameObject.name.Equals("ItemBom02(Clone)"))
                {
                    Debug.Log("Item 2");
                    BowController.numberOfAmmo[1] += 6;
                    numOfBomb[1].text = BowController.numberOfAmmo[1].ToString();
                }
                else
                {
                    Debug.Log("Item 3");
                    BowController.numberOfAmmo[2] += 3;
                    numOfBomb[2].text = BowController.numberOfAmmo[2].ToString();
                }
            }
            Destroy(collision.gameObject);

        }
    }
}
