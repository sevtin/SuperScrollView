using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SuperScrollView
{
    public class CustomCell : MonoBehaviour
    {
        //标题名称
        public Text mNameText;
        //图标
        public Image mIcon;
        //星星
        public Image[] mStarArray;
        //描述
        public Text mDescText;
        //描述2
        public Text mDescText2;
        //红色
        public Color32 mRedStarColor = new Color32(249, 227, 101, 255);
        //白色
        public Color32 mGrayStarColor = new Color32(215, 215, 215, 255);
        public GameObject mContentRootObj;
        int mItemDataIndex = -1;

        public void Init()
        {

        }
        //更新item数据和UI
        public void SetItemData(LiveReviewItem itemData, int itemIndex)
        {
            mItemDataIndex = itemIndex;
            mNameText.text = itemData.roomName;
            mDescText.text = itemData.source;
            mDescText2.text = itemData.image;
            //mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
        }
    }
}