using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] Color hightlightedColor;
    [SerializeField] int lettersPerSecond;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;
    public void SetDialog(string dialog){
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog){
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }

        yield return new WaitForSeconds(1f);
    }

    public int GetActionTextsCount(){
        return actionTexts.Count;
    }

    public void EnableDialogText(bool enabled){
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled){
        actionSelector.SetActive(enabled);
    }
    public void EnableMoveSelector(bool enabled){
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction){
        for(int i=0;i<actionTexts.Count;++i){
            if(i == selectedAction){
                actionTexts[i].color = hightlightedColor;
            }else{
                actionTexts[i].color = Color.white;
            }
        }
    }

     public void UpdateMovesSelection(int selectedMove,Move move){
        for(int i=0;i<moveTexts.Count;++i){
            if(i == selectedMove){
                moveTexts[i].color = hightlightedColor;
            }else{
                moveTexts[i].color = Color.white;
            }
        }

        ppText.text = $"pp {move.PP}/{move.Base.PP}";
        typeText.text = move.Base.Type.ToString();
    }
    

    public void SetMoveNames(List<Move> moves){
        for(int i = 0;i<moveTexts.Count;++i){
            if(i<moves.Count){
                moveTexts[i].text = moves[i].Base.Name;
            }else{
                moveTexts[i].text = "-";
            }
        }

    }

}
