
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
   public static void SavePlayer(Player player){
       BinaryFormatter formatter = new BinaryFormatter();
       string path = Application.persistentDataPath + "/player.br";
       FileStream steam = new FileStream(path,FileMode.Create);

       PlayerData data = new PlayerData(player);

       formatter.Serialize(steam,data);
       steam.Close();

   }

   public static PlayerData LoadPlayer(){
       string path =  Application.persistentDataPath + "/player.br";

       if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream steam = new FileStream(path,FileMode.Open);
            PlayerData data = formatter.Deserialize(steam) as PlayerData;
            steam.Close();
            return data;
       }else{
           Debug.LogError("Save file not found in" + path);
           return null;
       }
   }
}
