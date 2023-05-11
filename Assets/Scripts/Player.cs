using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField]
    private float PlayerSpeed = 20f;
    private bool isWalking = false;
    [SerializeField] private GameInput m_GameInput;
    [SerializeField] private float playerRadious = 30f;
    [SerializeField] private float playerHeight = 30f;
    [SerializeField] private LayerMask layerMask;

    private Vector3 lastInteractionDir;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one Player instance");
        }
    }

    private void Start()
    {
        m_GameInput.OnInteractAction += M_GameInput_OnInteractAction;
    }

    private void M_GameInput_OnInteractAction(object sender, EventArgs e)
    {

        if (selectedCounter)
        {
            //Debug.Log("Has ClearCount");
            selectedCounter.Interact();
        }

    }

    private void Update()
    {
        HandleMovement();
        HandleInteracions();
    }

    private void HandleInteracions()
    {
        Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 5f;
        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        Debug.DrawRay(transform.position, moveDir, Color.red);
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, layerMask))
        {
            Debug.Log(raycastHit.transform.parent.parent.transform);
            if (raycastHit.transform.parent.parent.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SelectNewCounter(clearCounter);
                }
            }
            else
            {
                SelectNewCounter(null);
            }
        }
        else
        {
            SelectNewCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = m_GameInput.GetMovementVectorNormalized();
        //Debug.Log("Input " + inputVector);
        if (Math.Abs(inputVector.x) > 0.5 || Math.Abs(inputVector.y) > 0.5)
        {
            Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
            move = move * Time.deltaTime * PlayerSpeed;
            isWalking = move != Vector3.zero;

            float moveDistance = Time.deltaTime * PlayerSpeed;
            //Debug.Log("IsWalking" + isWalking + " move " + move + " moveDistance " + moveDistance);
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
                Debug.DrawRay(transform.position, move * 100, Color.red);
            }
        }
        else
        {
            isWalking = false;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SelectNewCounter(ClearCounter clearCounter)
    {
        this.selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }
}
