using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    public GameObject[] arrow;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;
    public float nextShot;
    bool canShoot = true;

    int numberOfBomb = 3;
    public static int currentArrow = 0;

    public static int[] numberOfAmmo = { 50, 20, 10 };
    public Text[] numOfBomb;
    public Text coin;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, transform.position, Quaternion.identity);
        }
        numOfBomb[0].text = numberOfAmmo[0].ToString();
        numOfBomb[1].text = numberOfAmmo[1].ToString();
        numOfBomb[2].text = numberOfAmmo[2].ToString();
        coin.text = MyManager.Instance.coin.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = new Vector2(direction.y, -direction.x);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot && numberOfAmmo[currentArrow] > 0)
            {
                StartCoroutine(Shoot()); // Wait for seconds to Shoot.
            }
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentArrow += 1;
            if (currentArrow > 2)
            {
                currentArrow = 0;
            }
        }
        coin.text = MyManager.Instance.coin.ToString();
    }

    IEnumerator Shoot()
    {
        GameObject newArrow = Instantiate(arrow[currentArrow], transform.position, transform.rotation);
        ParticleController tntnewArrow = newArrow.GetComponent<ParticleController>();
        tntnewArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.right.y, transform.right.x) * launchForce;
        canShoot = false;
        yield return new WaitForSeconds(nextShot);
        canShoot = true;
        numberOfAmmo[currentArrow] -= 1;
        numOfBomb[currentArrow].text = numberOfAmmo[currentArrow].ToString();
        
        Debug.Log(numberOfAmmo[currentArrow].ToString());
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)transform.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
