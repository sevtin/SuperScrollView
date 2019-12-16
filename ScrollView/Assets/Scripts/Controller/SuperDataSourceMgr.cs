using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace SuperScrollView
{
    enum RequestStatus
    {
        None,
        Requesting,
        Done,
    }

    public class SuperDataSourceMgr : MonoBehaviour
    {
        //列表数据
        List<ItemData> mItemDataList = new List<ItemData>();
        //加载新数据完成回调
        System.Action mOnRefreshFinished = null;
        //加载更多数据完成
        System.Action mOnLoadMoreFinished = null;
        //加载更多数据数量
        int mLoadMoreCount = 20;
        //数据加载时间
        float mDataLoadLeftTime = 0;
        //是否等待刷新数据
        bool mIsWaittingRefreshData = false;
        //是否等待加载更多数据
        bool mIsWaitLoadingMoreData = false;
        //数据管理对象
        static SuperDataSourceMgr instance = null;
        //是否请求中
        RequestStatus requestStatus = RequestStatus.None;

        public static SuperDataSourceMgr Get
        {
            get
            {
                if (instance == null)
                {
                    instance = Object.FindObjectOfType<SuperDataSourceMgr>();
                }
                return instance;
            }

        }

        //获取index的数据模型
        public ItemData GetItemDataByIndex(int index)
        {
            if (index < 0 || index >= mItemDataList.Count)
            {
                return null;
            }
            return mItemDataList[index];
        }
        //获取指定id的数据模型
        public ItemData GetItemDataById(int itemId)
        {
            int count = mItemDataList.Count;
            for (int i = 0; i < count; ++i)
            {
                if (mItemDataList[i].mId == itemId)
                {
                    return mItemDataList[i];
                }
            }
            return null;
        }

        //获取数据条数
        public int TotalItemCount
        {
            get
            {
                return mItemDataList.Count;
            }
        }
        /// <summary>
        /// 加载最新数据
        /// </summary>
        /// <param name="onReflushFinished"></param>
        public void RequestRefreshDataList(System.Action onReflushFinished)
        {
            mDataLoadLeftTime = 1;
            mOnRefreshFinished = onReflushFinished;//回调
            mIsWaittingRefreshData = true;
            requestdata(1);
        }
        /// <summary>
        /// 加载更多数据
        /// </summary>
        /// <param name="loadCount"></param>
        /// <param name="onLoadMoreFinished"></param>
        public void RequestLoadMoreDataList(int loadCount, System.Action onLoadMoreFinished)
        {
            mLoadMoreCount = loadCount;
            mDataLoadLeftTime = 1;
            mOnLoadMoreFinished = onLoadMoreFinished;//回调
            mIsWaitLoadingMoreData = true;
            requestdata(2);
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void Update()
        {
            if (requestStatus == RequestStatus.Done)
            {
                if (mIsWaittingRefreshData)
                {
                    mDataLoadLeftTime -= Time.deltaTime;
                    if (mDataLoadLeftTime <= 0)
                    {
                        mIsWaittingRefreshData = false;
                        if (mOnRefreshFinished != null)
                        {
                            mOnRefreshFinished();
                        }
                    }
                }
                else if (mIsWaitLoadingMoreData)
                {//如果是加载更多数据
                    mDataLoadLeftTime -= Time.deltaTime;
                    if (mDataLoadLeftTime <= 0)
                    {
                        mIsWaitLoadingMoreData = false;
                        if (mOnLoadMoreFinished != null)
                        {
                            //加载数据完成回调
                            mOnLoadMoreFinished();
                        }
                    }
                }
            }
        }

        void requestdata(int page)
        {
            if (requestStatus == RequestStatus.Requesting)
            {
                return;
            }
            requestStatus = RequestStatus.Requesting;
            string url = "http://data.live.126.net/livechannel/previewlist.json";
            HttpHelper.Request(this, url, HttpHelper.MethodType.GET, null, HttpHelper.DownloadHanlderType.kHttpTEXT, requestCallback);
        }

        void requestCallback(bool isSucceed, object value)
        {
            string text = value.ToString();
            RootModel _Model = JsonConvert.DeserializeObject<RootModel>(text); //某实体MODEL

            int count = mItemDataList.Count;
            for (int i = 0; i < _Model.live_review.Count; i++)
            {
                Live_reviewItem item = _Model.live_review[i];
                ItemData model = new ItemData();
                model.mName = item.roomName;
                model.mDesc = item.source;
                model.mIcon = item.image;
                model.mId = i + count;
                mItemDataList.Add(model);
            }
            //最后更新状态
            requestStatus = RequestStatus.Done;
            /*
            if (mIsWaitLoadingMoreData)
            {//加载数据回调
                if(mOnLoadMoreFinished != null)
                {
                    mOnLoadMoreFinished();
                }
            }
            else if (mIsWaittingRefreshData)
            {//加载新数据
                if (mOnRefreshFinished != null)
                {
                    mOnRefreshFinished();
                }
            }
            mIsWaitLoadingMoreData = false;
            mIsWaittingRefreshData = false;
            Debug.Log(value);
            */
        }

        /// <summary>
        /// 选中所有item
        /// </summary>
        public void CheckAllItem()
        {
            int count = mItemDataList.Count;
            for (int i = 0; i < count; ++i)
            {
                mItemDataList[i].mChecked = true;
            }
        }
        /// <summary>
        /// 不选所有item
        /// </summary>
        public void UnCheckAllItem()
        {
            int count = mItemDataList.Count;
            for (int i = 0; i < count; ++i)
            {
                mItemDataList[i].mChecked = false;
            }
        }
        /// <summary>
        /// 删除所有选中的item
        /// </summary>
        /// <returns></returns>
        public bool DeleteAllCheckedItem()
        {
            int oldCount = mItemDataList.Count;
            mItemDataList.RemoveAll(it => it.mChecked);
            return (oldCount != mItemDataList.Count);
        }

    }

}