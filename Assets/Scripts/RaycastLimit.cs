using System;
using UnityEngine;

public class RaycastLimit : MonoBehaviour
{
    [SerializeField] private Vector3 limitOne;
    [SerializeField] private Vector3 limitTwo;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool limitOneHit;
    [SerializeField] private bool limitTwoHit;
    [SerializeField] private int rayLength;
    [SerializeField] private GameObject limitIndicator;
    [SerializeField] private LayerMask offWeapon;
    
    private void Update()
    {
        Debug.DrawRay(limitOne, Vector3.right * rayLength, Color.red);
        Debug.DrawRay(limitTwo, Vector3.right * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        limitOneHit = Physics2D.Raycast(limitOne, Vector3.right, rayLength, offWeapon);
        limitTwoHit = Physics2D.Raycast(limitTwo, Vector3.right, rayLength, offWeapon);
        if (limitOneHit && !gameManager.GetGameState())
        {
            limitIndicator.SetActive(true);
        }
        if (limitOneHit && limitTwoHit && !gameManager.GetGameState())
        {
            gameManager.EndLevel();
        }
    }
}
