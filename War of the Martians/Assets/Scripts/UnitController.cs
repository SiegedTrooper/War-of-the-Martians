using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UnitController : MonoBehaviour
{
    [SerializeField] public float speed = 1f;
    [SerializeField] public float nextWaypointDistance = 3f;
    [SerializeField] public float updatePathInterval = 1f;
    public bool isSelected = false;
    public Vector3 targetPosition;
    public bool isCommandedToMove;

    public float health = 100f;
    // Attack information is stored in AttackController

    private Rigidbody2D rb;
    private Camera cam;
    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool lockout = false;

    private void Start() {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        //InvokeRepeating("UpdatePath", 0f, .5f);
        //UpdatePath();
    }

    // Unit is damaged
    public void Damage(float damage) {
        health -= damage;
        //Debug.Log("Took " + damage + " damage!");
        if (health <= 0) {
            // Death
            Debug.Log(gameObject.name + " has died");
            Destroy(gameObject);
        }
    }

    // Unit starts path
    public void UpdatePath() {
        //Debug.Log("Called");
        //Debug.Log(seeker.IsDone());
        if (seeker.IsDone())
            seeker.StartPath(rb.position, targetPosition, OnPathComplete);
    }

    // Problem: State was spamming, causing no movement. Solution: cooldown on path calculation
    public void SpecialTargetFunction() {
        if (lockout == false) {
            lockout = true;
            UpdatePath();
            UnlockWait();
        }
    }
    private void UnlockWait() {
        IEnumerator waittt()
        {
            yield return new WaitForSeconds(1f);
            lockout = false;
        }
        StartCoroutine(waittt());
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1) && isSelected) {
                //Debug.Log("is moving");
                targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = transform.position.z;
                isCommandedToMove = true;
                UpdatePath();
        }
    }

    private void FixedUpdate() {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            isCommandedToMove = false;
            return;
        } else {
            reachedEndOfPath = false;
        }

        //Debug.Log(currentWaypoint);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
            currentWaypoint++;
    }
}
