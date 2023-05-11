using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;
    public void Interact()
    {
        Transform tomatoTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        tomatoTranform.localPosition = Vector3.zero;
        Debug.Log("Interact! " + tomatoTranform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);

    }
}
