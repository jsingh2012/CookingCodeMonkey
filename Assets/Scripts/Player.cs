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

    private void Update()
    {
        Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
        Debug.Log("Input "+ inputVector);
        Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
        move = move * Time.deltaTime * PlayerSpeed;
        isWalking = move != Vector3.zero;
        Debug.Log("IsWalking" + isWalking);
        transform.position += move;
        //transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * PlayerSpeed/10);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
