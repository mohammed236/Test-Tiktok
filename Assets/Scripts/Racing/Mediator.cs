using TikTokLiveSharp.Client;
using TikTokLiveSharp.Events;
using TikTokLiveSharp.Events.Objects;
using TikTokLiveUnity;
using TikTokLiveUnity.Utils;
using UnityEngine;
using UnityEngine.UI;
namespace Arab
{

    public class Mediator : MonoBehaviour
    {
        public static Mediator _instance { get; private set; }
        private TikTokLiveManager tikTokLiveManager => TikTokLiveManager.Instance;


        private float giftAmount = 0;
        private int giftCount = 0;
        private float likeCount = 0;
        private TikTokGift _gift;
        private Image _image_Gift;
        private Image _image_Gifter;
        private Image _image_Like;

        public void OnGift(TikTokGift gift)
        {
            // Display :
            // gifter name
            // gifter icon profile
            // gift name
            // gift icon
            // gift amount
            // Gift Count

            _gift = gift;

            //gifter name
            string name = _gift.Sender.UniqueId;

            //gifter icon profile
            //RequestImage(_image_Gifter, _gift.Sender.AvatarThumbnail);
            // ==> here you can access userSprite

            //gift icon
            //RequestImage(_image_Gift, _gift.Gift.Image);
            // ==> here you can access gift icon

            //Gift amount
            giftAmount = _gift.Amount;

            //gift count
            _gift.OnAmountChanged += Gift_OnAmountChanged;
            _gift.OnStreakFinished += Gift_OnStreakFinished;

            
            GameManager._instance.AddPlayerGifts(name, _gift.Sender.AvatarThumbnail);
            

        }
        private void Gift_OnAmountChanged(TikTokGift gift, long change, long newAmount)
        {
            giftCount =1;
            giftCount += 1;
            giftAmount = newAmount;
        }
        private void Gift_OnStreakFinished(TikTokGift gift, long finalAmount)
        {
            Gift_OnAmountChanged(gift, 0, finalAmount);
        }

        public void OnLike(Like like)
        {
            // Display :
            // liker name
            // liker icon profile
            // like count

            // liker name
            string likerName = like.Sender.UniqueId;

            // liker icon profile
            //RequestImage(_image_Like,like.Sender.AvatarThumbnail);
            // ==> here you can access userSprite

            //Like count
            likeCount = like.Count;

            // == > add user if like count >=3 == true
            if(likeCount >= GameManager._instance.minLikeToJoin)
            {
                GameManager._instance.AddPlayerLikes(likerName, like.Sender.AvatarThumbnail);
            }

        }

        public void OnComment(Chat comment)
        {

            // Display :
            // commenter name
            // comment

            //commenter name
            string commenterName = comment.Sender.UniqueId;

            //comment
            string _comment = comment.Message;


        }

        public void RequestImage(SpriteRenderer sprerer, Picture picture)
        {
            Dispatcher.RunOnMainThread(() =>
            {
                tikTokLiveManager.RequestSprite(picture, spr =>
                {

                    sprerer.sprite = spr;
                });
            });
        }

        private void Awake()
        {
            _instance = this;
        }
        private void OnDisable()
        {
            _gift.OnAmountChanged -= Gift_OnAmountChanged;
            _gift.OnStreakFinished -= Gift_OnStreakFinished;
        }
    }
}
