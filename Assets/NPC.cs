
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField]
    private float speed = .02f;
    [SerializeField]
    private float shuffleTime = 10f;

    Rigidbody rb;
    private Animator animator;
    private Vector2 currentdirection;
    private bool idle;
    private float shuffleCountdown;


    private void Awake() {
        idle = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shuffleCountdown = Time.time + Random.Range(shuffleTime * .75f,shuffleTime * 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > shuffleCountdown) {
            Shuffle();
        }
        if(!idle) {
            Walk();
        } else {
            Debug.Log("idle");
        }
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall")){
            //currentdirection = RandomDirection();
            StartCoroutine("Pause");
        }
    }

    private void RandomRotate() {
        transform.Rotate(new Vector3(0,Random.Range(0,360),0));
    }

    private void Walk() {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.Log("walk");
        //rb.AddForce(transform.TransformDirection(Vector3.forward)*speed, ForceMode.VelocityChange);
        rb.AddForce(transform.forward * speed,ForceMode.VelocityChange);
    }

    private void Shuffle() {
        RandomRotate();
        float timer = Random.Range(shuffleTime * .75f,shuffleTime * 1.25f);
        shuffleCountdown = Time.time + timer;
    }

    private IEnumerator Pause() {
        rb.velocity = Vector3.zero;
        idle = true;
        float duration = Random.Range(.5f,3f);
        yield return new WaitForSeconds(duration);
        Shuffle();
        idle = false;
    }
}
