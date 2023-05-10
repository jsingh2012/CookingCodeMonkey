using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float PlayerSpeed = 20f;
    private bool isWalking = false;
    [SerializeField] private GameInput m_GameInput;
    [SerializeField] private float playerRadious = 30f;
    [SerializeField] private float playerHeight = 30f;

    private void FixedUpdate()
    {
        Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
        Debug.Log("Input "+ inputVector);
        if (Math.Abs(inputVector.x) > 0.5 || Math.Abs(inputVector.y) > 0.5)
        {
            Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
            move = move * Time.deltaTime * PlayerSpeed;
            isWalking = move != Vector3.zero;

            float moveDistance = Time.deltaTime * PlayerSpeed;
            Debug.Log("IsWalking" + isWalking + " move " + move + " moveDistance " + moveDistance);
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, move, moveDistance);
            if (!canMove)
            {
                Vector3 moveX = new Vector3(move.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, moveX, moveDistance);
                if (canMove)
                {
                    transform.position += moveX;
                    Debug.DrawRay(transform.position, move * 100, Color.red);
                }

                Vector3 moveZ = new Vector3(0, 0, move.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadious, moveZ, moveDistance);
                if (canMove)
                {
                    transform.position += moveZ;
                    Debug.DrawRay(transform.position, move * 100, Color.red);
                }
            }
            else
            {
                transform.position += move;
                Debug.DrawRay(transform.position, move*100, Color.red);
            }
        }
    }



    public bool IsWalking()
    {
        return isWalking;
    }
}
