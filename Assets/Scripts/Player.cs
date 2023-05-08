using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float PlayerSpeed = 60f;
    private bool isWalking = false;
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if(Input.GetKey(KeyCode.W))
        {  
            inputVector.y = +1; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;            
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;   
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized;
        Debug.Log("Input "+ inputVector);
        Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
        move = move * Time.deltaTime * PlayerSpeed;
        isWalking = move != Vector3.zero;
        Debug.Log("IsWalking" + isWalking);
        transform.position += move;
        transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * PlayerSpeed/10);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
