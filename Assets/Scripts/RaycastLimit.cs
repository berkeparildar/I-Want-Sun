using System;
using UnityEngine;

public class RaycastLimit : MonoBehaviour
{
    [SerializeField] private Vector3 limitOne;
    [SerializeField] private Vector3 limitTwo;
    [SerializeField] private Vector3 limitThree;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool limitOneHit;
    [SerializeField] private bool limitTwoHit;
    [SerializeField] private bool limitThreeHit;
    [SerializeField] private int rayLength;
    
    private void Update()
    {
        Debug.DrawRay(limitOne, Vector3.right * rayLength, Color.red);
        Debug.DrawRay(limitTwo, Vector3.right * rayLength, Color.red);
        Debug.DrawRay(limitThree, Vector3.right * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        limitOneHit = Physics2D.Raycast(limitOne, Vector3.right, rayLength);
        limitTwoHit = Physics2D.Raycast(limitTwo, Vector3.right, rayLength);
        limitThreeHit = Physics2D.Raycast(limitThree, Vector3.right, rayLength);
        if (limitOneHit && limitTwoHit && limitThreeHit && !gameManager.GetGameState())
        {
            gameManager.LimitHit();
        }
    }
}
