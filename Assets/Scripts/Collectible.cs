using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public AudioClip collectSound;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
    }
}
