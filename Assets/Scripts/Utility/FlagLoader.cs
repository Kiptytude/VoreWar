using System.IO;

namespace FlagLoader
{
    internal class FlagLoader
    {
        internal void LoadFlags()
        {
            if (File.Exists($"{State.StorageDirectory}flags.txt"))
            {
                var flagList = File.ReadAllText($"{State.StorageDirectory}flags.txt");
                flagList = flagList.ToLower();
                if (flagList.Contains("leadersstartwitha")) Config.LetterBeforeLeaderNames = "A";
                if (flagList.Contains("leadersstartwithe")) Config.LetterBeforeLeaderNames = "E";
                if (flagList.Contains("leadersstartwithi")) Config.LetterBeforeLeaderNames = "I";
                if (flagList.Contains("leadersstartwitho")) Config.LetterBeforeLeaderNames = "O";
                if (flagList.Contains("leadersstartwithu")) Config.LetterBeforeLeaderNames = "U";
                if (flagList.Contains("permanentaitoaimessages")) Config.AIToAIMessagesForever = true;
                if (flagList.Contains("ktconvertalltypes")) Config.KuroTenkoConvertsAllTypes = true;
                if (flagList.Contains("dontfixleaders")) Config.DontFixLeaders = true;
            }
        }
    }

}

