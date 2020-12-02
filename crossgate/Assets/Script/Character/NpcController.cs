using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private float moveSpeed = 4f;
     private Vector2 move;
     private Rigidbody2D rb;
     public float moveH, moveV;
     private CharacterAnimator animator;
     bool isMoving;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        animator = GetComponent<CharacterAnimator>();
         rb = GetComponent<Rigidbody2D>();
        // Move(new Vector2(2,2));
        var orginVec = new Vector2(transform.position.x,transform.position.y);
        yield return StartCoroutine(Run(new Vector2(1,2)));

        yield return StartCoroutine(Skill());

        yield return StartCoroutine(Attack());

        yield return StartCoroutine(Run(orginVec));
       
        animator.LastDireaction = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Skill(){
         animator.IsSkill = true;
         Debug.Log("start skill");
         yield return new WaitForSeconds(0.16f*5*2);
         Debug.Log("end skill");
         animator.IsSkill = false;
    }
    IEnumerator Attack(){
         animator.IsAttack = true;
         Debug.Log("start IsAttack");
         yield return new WaitForSeconds(0.16f*8+1f);
         Debug.Log("end IsAttack");
         animator.IsAttack = false;
    }
    IEnumerator Walk(Vector2 targetPos)
    { //固定移动
        animator.IsMoving = true;
        animator.Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        while ((targetPos - new Vector2(transform.position.x,transform.position.y)).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        animator.Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        animator.IsMoving = false;
    }
    IEnumerator Run(Vector2 targetPos)
    { //固定移动
        animator.IsRunning = true;
        animator.Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        while ((targetPos - new Vector2(transform.position.x,transform.position.y)).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * 1.5f * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        animator.Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        animator.IsRunning = false;
    }
}
