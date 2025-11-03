using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonScript : MonoBehaviour
{
    public Transform orientation; // Obiect care defineste directia camerei pe planul orizontal
    public Transform playerEmpty; // Pozitia centrala a playerului
    public Transform playerVisual; // Modelul 3D vizual al playerului
    public Rigidbody playerRB; // Rigidbody-ul folosit pentru miscare
    public float rotationSpeed; // Viteza de rotatie
    public float moveSpeed = 5.0f; // Viteza playerului
    Vector3 inputDirection; // Directia finala de miscare calculata din input

    void Update()
    {
        // Calculeaza directia orizontala dintre camera si obiectul "playerEmpty"
        // Folosita pentru a seta orientarea miscarii in functie de camera
        Vector3 playerDirection = playerEmpty.position - new Vector3(transform.position.x, playerEmpty.position.y, transform.position.z);

        // Actualizeaza directia "in fata" a obiectului orientation
        // astfel incat sa urmeze directia camerei 
        orientation.forward = playerDirection.normalized;

        //inputuri movement
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Combina inputul cu orientarea camerei pentru a obtine o miscare relativa la camera
        inputDirection = orientation.forward * verticalInput + horizontalInput * orientation.right;

        // Daca exista input, roteste vizualul playerului spre directia de mers
        if (inputDirection != Vector3.zero)
            playerVisual.forward = Vector3.Slerp(playerVisual.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
    }

    void FixedUpdate()
    {
        // Aplica miscarea fizica in directia calculata, folosind Rigidbody
        playerRB.MovePosition(playerRB.position + inputDirection * moveSpeed * Time.fixedDeltaTime);
    }
}