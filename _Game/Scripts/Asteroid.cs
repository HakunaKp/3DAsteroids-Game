using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject rock;
    public GameObject health;
    public Gameplay GP;

    private float maxRotation;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    private Rigidbody rb;
    private Camera mainCam;
    private float maxSpeed;

    void Awake()
    {
        GP = GameObject.Find("Gameplay").GetComponent<Gameplay>();
    }

    void Start()
    {

        mainCam = Camera.main;

        maxRotation = 15f;
        rotationX = Random.Range(-maxRotation, maxRotation);
        rotationY = Random.Range(-maxRotation, maxRotation);
        rotationZ = Random.Range(-maxRotation, maxRotation);

        rb = rock.GetComponent<Rigidbody>();

        float speedX = Random.Range(200f, 800f);
        int selectorX = Random.Range(0, 2);
        float dirX = 0;
        if (selectorX == 1) { dirX = -1; }
        else { dirX = 1; }
        float finalSpeedX = speedX * dirX;
        rb.AddForce(transform.right * finalSpeedX);

        float speedY = Random.Range(200f, 800f);
        int selectorY = Random.Range(0, 2);
        float dirY = 0;
        if (selectorY == 1) { dirY = -1; }
        else { dirY = 1; }
        float finalSpeedY = speedY * dirY;
        rb.AddForce(transform.up * finalSpeedY);

    }

    void Update()
    {
        rock.transform.Rotate(new Vector3(rotationX, rotationY, 0) * Time.deltaTime);
        CheckPosition();
        float dynamicMaxSpeed = 3f;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -dynamicMaxSpeed, dynamicMaxSpeed), Mathf.Clamp(rb.velocity.y, -dynamicMaxSpeed, dynamicMaxSpeed));
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.name == "Bullet(Clone)")
        {
            GP.GetComponent<Gameplay>().UpdateAsteroids();
            Destroy();
        }

        if (collisionInfo.collider.name == "Rocket")
        {
            GP.GetComponent<Gameplay>().UpdateHealth();
        }
    }

    private void CheckPosition()
    {

        float rockOffset = 1;

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (rock.transform.position.x > sceneRightEdge + rockOffset)
        {
            rock.transform.position = new Vector2(sceneLeftEdge - rockOffset, rock.transform.position.y);
        }

        if (rock.transform.position.x < sceneLeftEdge - rockOffset)
        {
            rock.transform.position = new Vector2(sceneRightEdge + rockOffset, rock.transform.position.y);
        }

        if (rock.transform.position.y > sceneTopEdge + rockOffset)
        {
            rock.transform.position = new Vector2(rock.transform.position.x, sceneBottomEdge - rockOffset);
        }

        if (rock.transform.position.y < sceneBottomEdge - rockOffset)
        {
            rock.transform.position = new Vector2(rock.transform.position.x, sceneTopEdge + rockOffset);
        }

    }

    public void Destroy()
    {
        Destroy(gameObject, 0.01f);
    }

}
