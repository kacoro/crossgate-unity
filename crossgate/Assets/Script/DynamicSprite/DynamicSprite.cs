using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSprite: MonoBehaviour
{
    [SerializeField] string path;
    [SerializeField] int index;

    new SpriteRenderer renderer ;
    Sprite sp;
    float framRate = 0.16f;
    float timer;

    int currentFrame;
    List<Sprite> frames = new List<Sprite>();
    // Start is called before the first frame update
   private void Awake() {
      sp  = Resources.Load<Sprite>(path + index);
       while (sp)
       {
           frames.Add(sp);
           index +=1;
           sp  = Resources.Load<Sprite>(path + index);
       }
     
   }
   private void Start() {
        
        currentFrame = 0;
        timer = 0f;
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = frames[currentFrame];
   }

    private void Update() {
            HandleUpdate();
    }

    public void HandleUpdate(){
        timer += Time.deltaTime;
        if(timer > framRate){
            currentFrame = (currentFrame+1) % frames.Count;
             renderer.sprite = frames[currentFrame];
             timer -= framRate;
        }
    }
  
}
