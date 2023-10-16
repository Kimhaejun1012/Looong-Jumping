using UnityEngine;

public class Test : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 3f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            Debug.Log("Player Y ��ǥ: " + transform.position.y);
            timer = 0f;
        }
    }
}