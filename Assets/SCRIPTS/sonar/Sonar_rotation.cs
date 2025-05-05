using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar_rotation : MonoBehaviour
{
    //public float speed = 50f;
    public Color rayColor = Color.red; // I��n�n �arpt���nda kullan�lacak renk
    private float startTime;
    private float rotationInterval = 0.0875F; // *** birim ba�� d�nme h�z� (35/400) *** 
    private float rotation = 0f;
    private float rotationAmount = 0.9f; //360/400. bluerobotics sonar� 400 derecede tarama yap�yor.

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        move();
        isin();
    }

    private void move()
    {
        if (Time.time - startTime >= rotationInterval)
        {
            startTime = Time.time;
            rotation += rotationAmount;
            transform.Rotate(Vector3.up, rotationAmount);
        }

    }

    private void isin()
    {
        // Ana ���n�n ba�lang�� noktas� ve y�n�
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Ana ���n �izdirme
        DrawRay(rayOrigin, rayDirection, rayColor);

        float angleStep = 1f;    // Her ad�mda 1 derece art��

        // 24 adet ek ���n� �iz
        for (int i = 0; i < 13; i++)
        {
            // D�n�� matrisi ile ���n y�n�n� g�ncelle
            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, transform.right);
            Vector3 rotatedDirection = rotation * rayDirection;

            // �izgiyi �iz
            DrawRay(rayOrigin, rotatedDirection, rayColor);
        }
        for (int i = 0; i < 13; i++)
        {
            // D�n�� matrisi ile ���n y�n�n� g�ncelle
            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, (-1)*transform.right);
            Vector3 rotatedDirection = rotation * rayDirection;

            // �izgiyi �iz
            DrawRay(rayOrigin, rotatedDirection, rayColor);
        }
    }

    private void DrawRay(Vector3 origin, Vector3 direction, Color color)
    {
        // Raycast
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 50);

        bool hitSomething = false; // I��n�n bir �eye �arp�p �arpmad���n� kontrol etmek i�in bir de�i�ken

        // Her �arp��ma noktas� i�in �izgi �iz
        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(origin, hit.point, color); // �arpt���nda rayColor rengini kullan
            hitSomething = true; // I��n�n bir �eye �arpt���n� belirt
            Debug.Log("Hitted: " + hit.collider.transform.position);
            Debug.Log("Hit distance: " + hit.distance);
        }

        // E�er bir �eye �arpt�ysa ���n� belirtilen renge �evir
        if (hitSomething)
        {
            Debug.DrawRay(origin, direction * 50, color);
        }
        else
        {
            // Rayin 50 birim mesafede �izgi �iz
            Debug.DrawRay(origin, direction * 50, Color.green);
        }
    }
}
