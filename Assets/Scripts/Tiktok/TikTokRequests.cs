using System.Collections.Generic;
using TikTokLiveSharp.Client;
using TikTokLiveSharp.Events.Objects;
using TikTokLiveUnity;
using UnityEngine;

public class TikTokRequests : MonoBehaviour
{
    public string userName = "alloshw1";
    private string currentmsg = "";
    private string currentGft = "";
    private List<char> characters = new List<char> {
    // Lowercase
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
    'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    // Uppercase
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    '0','1','2','3','4','5','6','7','8','9'
    };

    // Start is called before the first frame update
    async void Start()
    {
        userName = Namer.Instance.userName;
        TikTokLiveManager.Instance.OnChatMessage += Instance_OnChatMessage;

        TikTokLiveManager.Instance.OnGift += Instance_OnGift;

        await TikTokLiveManager.Instance.ConnectToStream(userName);
    }

    public void Instance_OnGift(TikTokLiveClient sender, TikTokGift e)
    {
        Debug.Log(e.Gift.Name.ToString());
        //print(liveClient.HostName);
        // DisplayPicture.Insatance.SetPictureData(giftEvent.Sender.AvatarJpg);
        Gun.Instance.gft = e.Gift.Name.ToString();
        Gun.Instance.user = e.Sender.NickName.ToString();
    }

    public void Instance_OnChatMessage(TikTokLiveClient sender, TikTokLiveSharp.Events.Chat e)
    {
        //print(giftEvent.Message);
        string msg = "";
        if (currentmsg != e.Message.ToString())
        {
            foreach (var character in e.Message)
            {
                for (var i = 0; i < characters.Count; i++)
                {
                    if (character == characters[i])
                    {
                        msg += character;
                    }
                }
            }
            currentmsg = msg;
            print(currentmsg);
            //currentmsg = giftEvent.Message.ToString();
            Gun.Instance.str = currentmsg;
        }
        else
        {
            Gun.Instance.str = "";
        }
    }
}
