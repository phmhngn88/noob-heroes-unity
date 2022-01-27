using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomController : MonoBehaviour
{
    [SerializeField] float _launchForce = 1000; // Lực bắn
    [SerializeField] float _maxDragDistance = 0.3f; // Bán kính kéo
    new ParticleSystem particleSystem;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true; // Giành quyền điều khiển thay vì trọng lực trong game

    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red; // Click vào chim thì màu đỏ
    }

    void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        System.Console.WriteLine(direction);
        direction.Normalize();

        _rigidbody2D.isKinematic = false; // Đặt lại động lực học
        float dis = Mathf.Sqrt(Mathf.Pow(_startPosition.x - currentPosition.x, 2) + Mathf.Pow(_startPosition.y - currentPosition.y, 2));
        _rigidbody2D.AddForce(direction * _launchForce * (dis / _maxDragDistance));
        _spriteRenderer.color = Color.white; // Thả chim ra trở lại bình thường
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Set main camera theo vị trí trỏ chuột
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + direction * _maxDragDistance;
        }

        _rigidbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Button Down!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // Khi va chạm, chim quay về vị trí ban đầu
    {
        particleSystem.Play();
        StartCoroutine(Count1Second());
    }

    IEnumerator Count1Second()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
