using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] string path;
    // Start is called before the first frame update
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }
    public bool IsRunning {get;set;}
     public bool IsSkill {get;set;}
     public bool IsHurt {get;set;}
     public bool IsFaint {get;set;}
    public bool IsAttack {get;set;}
    public Vector2 Direction {get;set;}
    //states
    public SpriteAnimator currentAnim {get;set;}

    List<Sprite> frames = new List<Sprite>();

    SpriteRenderer spriteRenderer;

    
    public List<SpriteAnimator> AttackDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> runDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> skillDirections = new List<SpriteAnimator>();

    public List<SpriteAnimator> helloDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> faintDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> hurtDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> defanceDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> cryDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> angryDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> sitDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> idleDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> nodDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> walkDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> victoryDirections = new List<SpriteAnimator>();

    int lastDireaction;

    

    private void Awake()
    {
        frames = Resources.LoadAll<Sprite>(path).ToList();
    }
    private void Start()
    {
        lastDireaction = 6;
        spriteRenderer = GetComponent<SpriteRenderer>();
        int count = frames.Count;
        int offset = 0;
        int defaultPer = 64;
        int per = 47;
        // nw 39 
        for (int i = 0; i < 8; i++)
        {
            if(count ==384){
                offset =1;
                per = 48;
            }
            var index = i * per ;
            AttackDirections.Add(new SpriteAnimator(frames.GetRange(index + 0, 8+offset), spriteRenderer,0.16f,false));
            
            runDirections.Add(new SpriteAnimator(frames.GetRange(index + 8+offset, 6), spriteRenderer));
            helloDirections.Add(new SpriteAnimator(frames.GetRange(index + 14+offset , 2), spriteRenderer));
            faintDirections.Add(new SpriteAnimator(frames.GetRange(index + 16+offset, 6), spriteRenderer));
            hurtDirections.Add(new SpriteAnimator(frames.GetRange(index + 22+offset, 2), spriteRenderer));
            defanceDirections.Add(new SpriteAnimator(frames.GetRange(index + 24+offset, 1), spriteRenderer));
            cryDirections.Add(new SpriteAnimator(frames.GetRange(index + 25+offset, 2), spriteRenderer));
            skillDirections.Add(new SpriteAnimator(frames.GetRange(index + 27+offset, 3), spriteRenderer,0.16f,false));
            angryDirections.Add(new SpriteAnimator(frames.GetRange(index + 30+offset, 2), spriteRenderer));
            sitDirections.Add(new SpriteAnimator(frames.GetRange(index + 32+offset, 1), spriteRenderer));
            idleDirections.Add(new SpriteAnimator(frames.GetRange(index + 33+offset, 4), spriteRenderer, 1.394f / 4));
            nodDirections.Add(new SpriteAnimator(frames.GetRange(index + 37+offset, 2), spriteRenderer));
            walkDirections.Add(new SpriteAnimator(frames.GetRange(index + 39+offset, 6), spriteRenderer));
            victoryDirections.Add(new SpriteAnimator(frames.GetRange(index + 45+offset, 2), spriteRenderer));
        }

        // spriteRenderer.sprite = frames.GetRange(368,6)[0];
        currentAnim = idleDirections[lastDireaction];
    }

    private void Update()
    {
        var prevAnim = currentAnim;
        SetDirection();
        if(IsSkill){
            currentAnim = skillDirections[lastDireaction];
        }else if(IsAttack){
            currentAnim = AttackDirections[lastDireaction];
        }else if(IsRunning){
            currentAnim = runDirections[lastDireaction];
        }else if(IsMoving){
           currentAnim = walkDirections[lastDireaction];
        }else{
            currentAnim = idleDirections[lastDireaction];
        }
        if (prevAnim != currentAnim )
            currentAnim.Start();
        
        currentAnim.HandleUpdate();
      
    }

    public int LastDireaction{
        get {return lastDireaction;}
        set {lastDireaction = value;}
    }

    public bool IsFinshed(){
        return currentAnim.CurrentFrame == currentAnim.Frames.Count-1;
    }

    public void SetDirection()
    {
        if (Direction.magnitude < 0.01) //MARKER character is static. 
        {
             IsMoving = false;
            // directionArray = idleDirections;
        }
        else
        {
             IsMoving = true;
            // directionArray = walkDirections;
            lastDireaction = DirectionToIndex();
        }
        // currentAnim = directionArray[lastDireaction];
    }

    private int DirectionToIndex()
    {
        Vector2 norDir = Direction.normalized;
        float step = 360 / 8;
        float offset = step / 2;
        float angle = -Vector2.SignedAngle(new Vector2(-1,1), norDir);
       
        angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        //NOW      NW N  NE E  SE S  SW W
        //BEFORE   N  NW W  SW S  SE E  NE
        lastDireaction = Mathf.FloorToInt(stepCount);
        return lastDireaction;
    }
}
