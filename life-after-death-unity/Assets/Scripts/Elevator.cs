using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    Rigidbody2D elevator;

    [SerializeField]
    Transform elevatorEndPoint;

    bool hasActivated = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasActivated == true)
            return;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            hasActivated = true;
            StartCoroutine(MoveElevator(player));
        }
    }

    IEnumerator MoveElevator(Player player)
    {
        //Freeze player

        while (transform.position.y != elevatorEndPoint.position.y)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, elevatorEndPoint.position, moveSpeed * Time.deltaTime);
            elevator.MovePosition(newPosition);
            yield return null;
        }

        //Unfreeze player
        yield break;
    }
}
