using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spikeController : MonoBehaviour
{
    private Rigidbody2D myRigibody;
    private Animator myAnimator;
    
   private void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) 
        {
            Die();
        }
    }
    private void Die()
    {
        myRigibody.bodyType = RigidbodyType2D.Static;
        myAnimator.SetTrigger("death");
    }
    private void Restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
