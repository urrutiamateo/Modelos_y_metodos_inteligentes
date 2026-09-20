using UnityEngine;

public enum ContactResult
{
    Win,
    Lose
}

public class ContactEnd : MonoBehaviour
{
    public ContactResult result;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        if (result == ContactResult.Win)
            GameManager.Instance.Win();
        else
            GameManager.Instance.Lose();
    }
}