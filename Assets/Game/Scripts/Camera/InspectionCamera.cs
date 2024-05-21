using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCamera : MonoBehaviour
{
    public Transform target; // ������� ������ (������ �������� ������)
    public float distance = 10.0f; // ��������� ���������� ������ �� ����
    public float minDistance = 5.0f; // ����������� ���������� ������ �� ����
    public float maxDistance = 15.0f; // ������������ ���������� ������ �� ����
    public float zoomSpeed = 5.0f; // �������� �����������/��������� ������
    public float rotationSpeed = 3.0f; // �������� �������� ������

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            // �����������/��������� ������ ��� ������ �������� ���� ��� ���� �� ���-������
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // �������� ������ ��� ������ ������ ������ ���� ��� ������� �� ���-������
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * rotationSpeed;
                y -= Input.GetAxis("Mouse Y") * rotationSpeed;
            }

            // ����������� ���� �������� ������ �� ���������
            y = Mathf.Clamp(y, -80.0f, 80.0f);

            // ������ ������� ������
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            // ���������� ������� � �������� ������
            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
