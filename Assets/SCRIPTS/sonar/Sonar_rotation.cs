using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar_rotation : MonoBehaviour
{
    //public float speed = 50f;
    public Color rayColor = Color.red; // Iþýnýn çarptýðýnda kullanýlacak renk
    private float startTime;
    private float rotationInterval = 0.0875F; // *** birim baþý dönme hýzý (35/400) *** 
    private float rotation = 0f;
    private float rotationAmount = 0.9f; //360/400. bluerobotics sonarý 400 derecede tarama yapýyor.

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
        // Ana ýþýnýn baþlangýç noktasý ve yönü
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Ana ýþýn çizdirme
        DrawRay(rayOrigin, rayDirection, rayColor);

        float angleStep = 1f;    // Her adýmda 1 derece artýþ

        // 24 adet ek ýþýný çiz
        for (int i = 0; i < 13; i++)
        {
            // Dönüþ matrisi ile ýþýn yönünü güncelle
            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, transform.right);
            Vector3 rotatedDirection = rotation * rayDirection;

            // Çizgiyi çiz
            DrawRay(rayOrigin, rotatedDirection, rayColor);
        }
        for (int i = 0; i < 13; i++)
        {
            // Dönüþ matrisi ile ýþýn yönünü güncelle
            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, (-1)*transform.right);
            Vector3 rotatedDirection = rotation * rayDirection;

            // Çizgiyi çiz
            DrawRay(rayOrigin, rotatedDirection, rayColor);
        }
    }

    private void DrawRay(Vector3 origin, Vector3 direction, Color color)
    {
        // Raycast
        RaycastHit[] hits = Physics.RaycastAll(origin, direction, 50);

        bool hitSomething = false; // Iþýnýn bir þeye çarpýp çarpmadýðýný kontrol etmek için bir deðiþken

        // Her çarpýþma noktasý için çizgi çiz
        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(origin, hit.point, color); // Çarptýðýnda rayColor rengini kullan
            hitSomething = true; // Iþýnýn bir þeye çarptýðýný belirt
            Debug.Log("Hitted: " + hit.collider.transform.position);
            Debug.Log("Hit distance: " + hit.distance);
        }

        // Eðer bir þeye çarptýysa ýþýný belirtilen renge çevir
        if (hitSomething)
        {
            Debug.DrawRay(origin, direction * 50, color);
        }
        else
        {
            // Rayin 50 birim mesafede çizgi çiz
            Debug.DrawRay(origin, direction * 50, Color.green);
        }
    }
}
