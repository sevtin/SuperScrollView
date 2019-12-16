using System.Collections.Generic;

public class Match_info
{
    /// <summary>
    /// 凯尔特人
    /// </summary>
    public string homeName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string awayFlag { get; set; }
    /// <summary>
    /// 76人
    /// </summary>
    public string awayName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int homeScore { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sourceName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string homeFlag { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string source { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int awayScore { get; set; }
    /// <summary>
    /// 完场
    /// </summary>
    public string status { get; set; }
}

public class LiveReviewItem
{
    public bool mChecked;
    public int mId;
    /// <summary>
    /// 
    /// </summary>
    public string startTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Match_info match_info { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int roomId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pcImage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pano { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int liveType { get; set; }
    /// <summary>
    /// 凯尔特人vs76人
    /// </summary>
    public string roomName { get; set; }
    /// <summary>
    /// 网易原创
    /// </summary>
    public string source { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int liveStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mutilVideo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int confirm { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string video { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string endTime { get; set; }
}

public class TopItem
{
    /// <summary>
    /// 
    /// </summary>
    public string startTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int roomId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pcImage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pano { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int liveType { get; set; }
    /// <summary>
    /// 网易原创
    /// </summary>
    public string source { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int liveStatus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string mutilVideo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int confirm { get; set; }
    /// <summary>
    /// 2020东京奥运会网易战略发布会
    /// </summary>
    public string roomName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string video { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string endTime { get; set; }
}

public class FutureItem
{
    /// <summary>
    /// 
    /// </summary>
    public string startTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pcImage { get; set; }
    /// <summary>
    /// 网易原创
    /// </summary>
    public string source { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int liveType { get; set; }
    /// <summary>
    /// 热火vs湖人
    /// </summary>
    public string roomName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int roomId { get; set; }
}

public class SublivesItem
{
    /// <summary>
    /// 
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int userCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int collectionId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tid { get; set; }
    /// <summary>
    /// 网易讲讲历史
    /// </summary>
    public string tname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ename { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string userId { get; set; }
    /// <summary>
    /// 直播
    /// </summary>
    public string cname { get; set; }
}

public class RootModel
{
    /// <summary>
    /// 
    /// </summary>
    public int nextPage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<LiveReviewItem> live_review { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<TopItem> top { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int pageNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<FutureItem> future { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<SublivesItem> sublives { get; set; }
}