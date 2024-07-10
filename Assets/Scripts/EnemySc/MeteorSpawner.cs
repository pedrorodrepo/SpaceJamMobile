using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] meteors;
    [SerializeField] private float spawnTime;

    private float timer = 0;
    private int index;

    private Camera mainCamera;
    private float maxLeft;
    private float maxRight; 
    private float yPosition;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundaries());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTime)
        {
            index = Random.Range(0, meteors.Length);
            GameObject obj = Instantiate(meteors[index], new Vector3(Random.Range(maxLeft, maxRight), yPosition, -5), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            float size = Random.Range(0.5f, 1.5f);
            obj.transform.localScale = new Vector3(size, size, 1);

            timer = 0;
        }
    }

    private IEnumerator SetBoundaries()
    {
        yield return new WaitForSeconds(0.4f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;
        yPosition = mainCamera.ViewportToWorldPoint(new Vector2(0, 1.1f)).y;
    }
}
