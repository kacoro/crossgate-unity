using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
public class BattleUnit : MonoBehaviour
{

    public List<SpriteAnimator> AttackDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> runDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> faintDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> hurtDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> defanceDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> skillDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> idleDirections = new List<SpriteAnimator>();
    public List<SpriteAnimator> walkDirections = new List<SpriteAnimator>();
    List<Sprite> frames = new List<Sprite>();
    SpriteRenderer spriteRenderer;
    int lastDireaction;
    public SpriteAnimator currentAnim {get;set;}

    [SerializeField] bool isPlayerUnit;
    [SerializeField] BattleHud hud;

    public bool IsPlayerUnit{
        get { return isPlayerUnit;}
    }

    public BattleHud Hud{
        get {return hud;}
    }

    public Pet Pet {get;set;}

    Image image;
    Vector3 originalPos;
    Color originalColor;
    
      public Vector2 Direction {get;set;}
     public bool IsMoving { get; set; }
    public bool IsRunning {get;set;}
     public bool IsSkill {get;set;}
     public bool IsHurt {get;set;}
     public bool IsFaint {get;set;}
    public bool IsAttack {get;set;}
    private float moveSpeed = 4f;
    private void Awake() {
         spriteRenderer = GetComponent<SpriteRenderer>();
        originalPos = transform.localPosition;
        originalColor = spriteRenderer.color;
        
    }
    private void Start() {
        
       
    }

   public void Setup(Pet pet){
        Pet = pet;
       if(isPlayerUnit){
        //    image.sprite = Pet.Base.Sprite;
           lastDireaction = 0;
       }else{
        //    image.sprite = Pet.Base.Sprite;
           lastDireaction = 4;
       }
       
       hud.setData(pet);

       spriteRenderer.color = originalColor;
       
       frames = Resources.LoadAll<Sprite>(Pet.Base.FramsPath).ToList();
       int count = frames.Count;
       int per = count/8;
       for (int i = 0; i < 8; i++)
        {
            var index = i * per;
            AttackDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Attack.x,Pet.Base.FramOffsets.Attack.y), spriteRenderer,0.16f,false));
            runDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Run.x,  Pet.Base.FramOffsets.Run.y), spriteRenderer));
            faintDirections.Add(new SpriteAnimator(frames.GetRange(index +Pet.Base.FramOffsets.Faint.x, Pet.Base.FramOffsets.Faint.y), spriteRenderer));
            hurtDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Hurt.x, Pet.Base.FramOffsets.Hurt.y), spriteRenderer));
            defanceDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Defance.x, Pet.Base.FramOffsets.Defance.y), spriteRenderer));
            skillDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Skill.x, Pet.Base.FramOffsets.Skill.y), spriteRenderer,0.16f,false));
            idleDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Idle.x, Pet.Base.FramOffsets.Idle.y), spriteRenderer, 1.394f / 4));
            walkDirections.Add(new SpriteAnimator(frames.GetRange(index + Pet.Base.FramOffsets.Walk.x, Pet.Base.FramOffsets.Walk.y), spriteRenderer));
        }
         currentAnim = idleDirections[lastDireaction];
         PlayEnterAnimation();
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
   public void PlayEnterAnimation(){
       if(isPlayerUnit){
           transform.localPosition = new Vector3(900f,-900f*47/64);
       }else{
           transform.localPosition = new Vector3(-900f,900f*47/64);
       }
       transform.DOLocalMove(originalPos,1f);
   }

   public void PlayAttackAnimation(){
       var sequence = DOTween.Sequence();
       if(isPlayerUnit){
          sequence.Append(transform.DOLocalMove(new Vector3(originalPos.x - 50f,originalPos.y+50*47/64,0f),0.25f));
       }
       else{
           sequence.Append(transform.DOLocalMove(new Vector3(originalPos.x + 50f,originalPos.y-50*47/64,0f),0.25f));
       }
       sequence.Append(transform.DOLocalMove(originalPos,0.25f));
   }
   public void PlayHitAnimation(){
       var sequence = DOTween.Sequence();
       sequence.Append(spriteRenderer.DOColor(Color.gray,0.1f));
       sequence.Append(spriteRenderer.DOColor(originalColor,0.1f));
   }

   public void PlayFaintAnimation(){
    //    var sequence = DOTween.Sequence();
    //    sequence.Append(transform.DOMoveY(originalPos.y -150f,0.5f));
    //    sequence.Join(spriteRenderer.DOFade(0f,0.5f));
   }

   IEnumerator Skill(){
         IsSkill = true;
         Debug.Log("start skill");
         yield return new WaitForSeconds(0.16f*5*2);
         Debug.Log("end skill");
         IsSkill = false;
    }
    IEnumerator Attack(){
         IsAttack = true;
         Debug.Log("start IsAttack");
         yield return new WaitForSeconds(0.16f*8+1f);
         Debug.Log("end IsAttack");
         IsAttack = false;
    }
    IEnumerator Walk(Vector2 targetPos)
    { //固定移动
        IsMoving = true;
        Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        while ((targetPos - new Vector2(transform.position.x,transform.position.y)).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        IsMoving = false;
    }
    IEnumerator Run(Vector2 targetPos)
    { //固定移动
        IsRunning = true;
        Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        while ((targetPos - new Vector2(transform.position.x,transform.position.y)).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * 1.5f * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        Direction = targetPos - new Vector2(transform.position.x,transform.position.y);
        IsRunning = false;
    }
}
