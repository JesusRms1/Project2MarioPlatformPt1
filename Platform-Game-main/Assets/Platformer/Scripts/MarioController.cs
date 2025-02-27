using UnityEngine;
using System;

public class MarioController : MonoBehaviour
{
  public float acceleration = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 5f;
    public bool isGrounded;

    public bool didContact;

    public GameObject FlagPolePrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    void Start()
    {
        brickPrefab = GameObject.Find("testblock(Clone)");
        questionBoxPrefab = GameObject.Find("Question 1(Clone)");   
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rbody = GetComponent<Rigidbody>();
        rbody.linearVelocity += Vector3.right * horizontalMovement * Time.deltaTime * acceleration;

        Collider col =  GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + .03f;
        float fullHeight = col.bounds.extents.y + 1f;
        //Debug.Log(col.bounds.extents.y);
        
        Vector3 startPoint = transform.position;
        //Debug.Log($"Start Point: {startPoint}");
        Vector3 endPoint = startPoint + Vector3.down * halfHeight;
        Vector3 topPoint = startPoint + Vector3.up * fullHeight;
        //Debug.Log($"Top Point: {topPoint}");

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
        didContact = Physics.Raycast(startPoint, Vector3.up, fullHeight);

        //Color lineColor = (isGrounded) ? Color.green : Color.red;
        Color secondLineColor = (didContact) ? Color.green : Color.red;
        //Debug.DrawLine(startPoint, endPoint, lineColor,0f,false);
        //Debug.DrawLine(startPoint, topPoint, secondLineColor,10f,true);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space) && rbody.linearVelocity.y > 0)
        {
            rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }

        if(Math.Abs(rbody.linearVelocity.x) > maxSpeed)
        {
            Vector3 newVel = rbody.linearVelocity;
            newVel.x = Math.Clamp(newVel.x,-maxSpeed,maxSpeed);
            rbody.linearVelocity = newVel;
        }

        if(isGrounded && Math.Abs(horizontalMovement) < .5f) 
        {
            Vector3 newVel = rbody.linearVelocity;
            newVel.x *= 1f - Time.deltaTime;
            rbody.linearVelocity = newVel;
        }

        float yaw = (rbody.linearVelocity.x > 0) ? 90f : -90f;
        transform.rotation = Quaternion.Euler(0f,yaw,0f);

        float speed = rbody.linearVelocity.x;
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", Math.Abs(speed));
        anim.SetBool("In Air", !isGrounded);

        if(transform.position.y < 0) {
            transform.position = new Vector3(20f, 1.5f, 0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        questionBoxPrefab = GameObject.Find("Question 1(Clone)");
        FlagPolePrefab = GameObject.Find("FlagPole");

        GameObject gameManager = GameObject.Find("Game Manager");
        GameManager gm = gameManager.GetComponent<GameManager>();

        if (col.gameObject == FlagPolePrefab)
        {
            Debug.Log("You win!");
            gm.timerText.text = "You win!";

        }

        if (col.gameObject.tag == "Brick" && didContact && col.contacts[0].normal.y < 0)
        {
            Destroy(col.gameObject);
            gm.totalScore += 100;
        }

        if (col.gameObject.tag == "question" && didContact && col.contacts[0].normal.y < 0)
        {
            gm.score += 1;
            gm.totalScore += 100;
        }
        gm.timerText.text = $"MARIO                      WORLD               Time: \n{gm.totalScore}           x{gm.score}         1-1                     {100 - (int)Time.realtimeSinceStartup}";
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Water")
        {
            transform.position = new Vector3(20f, 1.5f, 0f);
        }
    }
}
