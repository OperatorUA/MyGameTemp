using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionCamera : MonoBehaviour
{
    public Transform target; // Целевой объект (обычно персонаж игрока)
    public float distance = 10.0f; // Начальное расстояние камеры от цели
    public float minDistance = 5.0f; // Минимальное расстояние камеры от цели
    public float maxDistance = 15.0f; // Максимальное расстояние камеры от цели
    public float zoomSpeed = 5.0f; // Скорость приближения/отдаления камеры
    public float rotationSpeed = 3.0f; // Скорость вращения камеры

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
            // Приближение/отдаление камеры при помощи колесика мыши или зума на тач-экране
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Вращение камеры при помощи правой кнопки мыши или пальцем на тач-экране
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * rotationSpeed;
                y -= Input.GetAxis("Mouse Y") * rotationSpeed;
            }

            // Ограничение угла вращения камеры по вертикали
            y = Mathf.Clamp(y, -80.0f, 80.0f);

            // Расчет позиции камеры
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            // Применение позиции и вращения камеры
            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
