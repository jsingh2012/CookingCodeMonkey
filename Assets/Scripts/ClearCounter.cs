using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTranform.GetComponent<KitchenObject>().SetClearCounter(this);
            Debug.Log("Interact! " + kitchenObjectTranform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
        }
        else
        {
            Debug.Log("Placed " + kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenTopPoint()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        kitchenObject = ko;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObect()
    {
        return (kitchenObject != null);
    }
}
