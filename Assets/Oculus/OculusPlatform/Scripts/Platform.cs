// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]

namespace Oculus.Platform
{
  using UnityEngine;
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Runtime.InteropServices;

  public sealed class Core {
    private static bool IsPlatformInitialized = false;
    public static bool IsInitialized()
    {
      return IsPlatformInitialized;
    }

    // If LogMessages is true, then the contents of each request response
    // will be printed using Debug.Log. This allocates a lot of heap memory,
    // and so should not be called outside of testing and debugging.
    public static bool LogMessages = false;

    internal static void ForceInitialized()
    {
      IsPlatformInitialized = true;
    }

    public static void Initialize(string appId = null)
    {
      bool forceWindowsPlatform = UnityEngine.Application.platform == RuntimePlatform.WindowsEditor && !PlatformSettings.UseStandalonePlatform;

      if (!UnityEngine.Application.isEditor || forceWindowsPlatform)
      {
        string configAppID = GetAppIDFromConfig(forceWindowsPlatform);
        if (String.IsNullOrEmpty(appId))
        {
          if (String.IsNullOrEmpty(configAppID))
          {
            throw new UnityException("Update your app id by selecting 'Oculus Platform' -> 'Edit Settings'");
          }
          appId = configAppID;
        }
        else
        {
          if (!String.IsNullOrEmpty(configAppID))
          {
            Debug.LogWarningFormat("The 'Oculus App Id ({0})' field in 'Oculus Platform/Edit Settings' is clobbering appId ({1}) that you passed in to Platform.Core.Init.  You should only specify this in one place.  We recommend the menu location.", configAppID, appId);
          }
        }
      }

      if (forceWindowsPlatform) {
        var platform = new WindowsPlatform();
        IsPlatformInitialized = platform.Initialize(appId);
      } else if (UnityEngine.Application.isEditor) {
        var platform = new StandalonePlatform();
        IsPlatformInitialized = platform.InitializeInEditor();
      } else if (UnityEngine.Application.platform == RuntimePlatform.Android) {
        var platform = new AndroidPlatform();
        IsPlatformInitialized = platform.Initialize(appId);
      } else if (UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer) {
        var platform = new WindowsPlatform();
        IsPlatformInitialized = platform.Initialize(appId);
      } else {
        throw new NotImplementedException("Oculus platform is not implemented on this platform yet.");
      }

      if (!IsPlatformInitialized)
      {
        throw new UnityException("Oculus Platform failed to initialize.");
      }

      if (LogMessages) {
        Debug.LogWarning("Oculus.Platform.Core.LogMessages is set to true. This will cause extra heap allocations, and should not be used outside of testing and debugging.");
      }

      // Create the GameObject that will run the callbacks
      (new GameObject("Oculus.Platform.CallbackRunner")).AddComponent<CallbackRunner>();
    }

    private static string GetAppIDFromConfig(bool forceWindows)
    {
      if (UnityEngine.Application.platform == RuntimePlatform.Android)
      {
        return PlatformSettings.MobileAppID;
      }
      else if (UnityEngine.Application.platform == RuntimePlatform.WindowsPlayer || forceWindows)
      {
        return PlatformSettings.AppID;
      }
      return null;
    }
  }

  public static class Entitlements
  {
    public static Request IsUserEntitledToApplication()
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Entitlement_GetIsViewerEntitled());
      }

      return null;
    }
  }

  public static partial class IAP
  {
    public static Request<Models.PurchaseList> GetViewerPurchases()
    {

      if (Core.IsInitialized())
      {
        return new Request<Models.PurchaseList>(CAPI.ovr_IAP_GetViewerPurchases());
      }

      return null;
    }

    public static Request<Models.ProductList> GetProductsBySKU(string[] skus)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.ProductList>(CAPI.ovr_IAP_GetProductsBySKU(skus, skus.Length));
      }

      return null;
    }

    public static Request<Models.Purchase> LaunchCheckoutFlow(string sku)
    {
      if (Core.IsInitialized())
      {
        if (UnityEngine.Application.isEditor) {
          throw new NotImplementedException("LaunchCheckoutFlow() is not implemented in the editor yet.");
        }

        return new Request<Models.Purchase>(CAPI.ovr_IAP_LaunchCheckoutFlow(sku));
      }

      return null;
    }

    public static Request ConsumePurchase(string sku)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_IAP_ConsumePurchase(sku));
      }

      return null;
    }
  }

  public static partial class Achievements
  {
    public static Request<Models.AchievementDefinitionList> GetAllDefinitions()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.AchievementDefinitionList>(CAPI.ovr_Achievements_GetAllDefinitions());
      }

      return null;
    }

    public static Request<Models.AchievementDefinitionList> GetDefinitionsByName(string[] names)
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.AchievementDefinitionList>(CAPI.ovr_Achievements_GetDefinitionsByName(names, names.Length));
      }

      return null;
    }

    public static Request<Models.AchievementProgressList> GetAllProgress()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.AchievementProgressList>(CAPI.ovr_Achievements_GetAllProgress());
      }

      return null;
    }

    public static Request<Models.AchievementProgressList> GetProgressByName(string[] names)
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.AchievementProgressList>(CAPI.ovr_Achievements_GetProgressByName(names, names.Length));
      }

      return null;
    }

    // Unlock the achievement with the given name. Can be of any achievement type.
    public static Request Unlock(string name)
    {
      if(Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Achievements_Unlock(name));
      }

      return null;
    }

    // Add 'count' to the achievement with the given name. Must be a COUNT achievement.
    public static Request AddCount(string name, ulong count)
    {
      if(Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Achievements_AddCount(name, count));
      }

      return null;
    }

    // Unlock fields of a BITFIELD acheivement. "fields" is a string containing either
    // '0' or '1' characters. Every '1' will unlock the field in the corresponding position.
    public static Request AddFields(string name, string fields)
    {
      if(Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Achievements_AddFields(name, fields));
      }

      return null;
    }
  }

  public static partial class Users
  {
    public static Request<Models.User> Get(UInt64 userID)
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.User>(CAPI.ovr_User_Get(userID));
      }

      return null;
    }

    public static Request<Models.User> GetLoggedInUser()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.User>(CAPI.ovr_User_GetLoggedInUser());
      }

      return null;
    }

    public static Request<Models.UserList> GetLoggedInUserFriends()
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.UserList>(CAPI.ovr_User_GetLoggedInUserFriends());
      }

      return null;
    }

    public static Request<string> GetAccessToken()
    {
      if (Core.IsInitialized())
      {
        return new Request<string>(CAPI.ovr_User_GetAccessToken());
      }

      return null;
    }

    public static Request<Models.UserProof> GetUserProof()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.UserProof>(CAPI.ovr_User_GetUserProof());
      }

      return null;
    }
  }

  public static partial class Application
  {
    public static Request<Models.ApplicationVersion> GetVersion()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.ApplicationVersion>(CAPI.ovr_Application_GetVersion());
      }

      return null;
    }
  }

  public static partial class Rooms
  {
    public static Request<Models.Room> CreateAndJoinPrivate(RoomJoinPolicy joinPolicy, uint maxUsers, bool subscribeToNotifications = false)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_CreateAndJoinPrivate(joinPolicy, maxUsers, subscribeToNotifications));
      }

      return null;
    }

    public static Request<Models.Room> Get(UInt64 roomID)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_Get(roomID));
      }

      return null;
    }

    // Kick the user from the specified room. The user won't be able to join again for kickDurationSeconds.
    public static Request<Models.Room> KickUser(UInt64 roomID, UInt64 userID, int kickDurationSeconds)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_KickUser(roomID, userID, kickDurationSeconds));
      }

      return null;
    }


    public static Request<Models.Room> Leave(UInt64 roomID)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_Leave(roomID));
      }

      return null;
    }

    public static Request<Models.Room> Join(UInt64 roomID, bool subscribeToNotifications = false)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_Join(roomID, subscribeToNotifications));
      }

      return null;
    }

    public static Request<Models.Room> GetCurrent()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_GetCurrent());
      }

      return null;
    }

    public static Request<Models.Room> GetCurrentForUser(UInt64 userID)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_GetCurrentForUser(userID));
      }

      return null;
    }

    public static Request<Models.UserList> GetInvitableUsers()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.UserList>(CAPI.ovr_Room_GetInvitableUsers());
      }

      return null;
    }

    public static Request<Models.Room> SetDescription(UInt64 roomID, string description)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_SetDescription(roomID, description));
      }

      return null;
    }

    public static Request<Models.Room> UpdatePrivateRoomJoinPolicy(UInt64 roomID, RoomJoinPolicy newJoinPolicy)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_UpdatePrivateRoomJoinPolicy(roomID, newJoinPolicy));
      }

      return null;
    }

    public static Request<Models.Room> UpdateMembershipLockStatus(UInt64 roomID, RoomMembershipLockStatus lockStatus)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_UpdateMembershipLockStatus(roomID, lockStatus));
      }

      return null;
    }


    public static Request<Models.Room> UpdateDataStore(UInt64 roomID, Dictionary<string, string> data)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovrKeyValuePair[] kvps = new CAPI.ovrKeyValuePair[data.Count];
        int i=0;
        foreach(var item in data)
        {
          kvps[i++] = new CAPI.ovrKeyValuePair(item.Key, item.Value);
        }

        return new Request<Models.Room>(CAPI.ovr_Room_UpdateDataStore(roomID, kvps, (uint)kvps.Length));
      }
      return null;
    }

    public static Request<Models.Room> InviteUser(UInt64 roomID, string inviteToken)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_InviteUser(roomID, inviteToken));
      }

      return null;
    }

    public static Request LaunchInvitableUserFlow(UInt64 roomID)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Room_LaunchInvitableUserFlow(roomID));
      }

      return null;
    }

    public static Request<Models.Room> UpdateOwner(UInt64 roomID, UInt64 userID)
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Room_UpdateOwner(roomID, userID));
      }

      return null;
    }

    public static Request<Models.RoomList> GetModeratedRooms()
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.RoomList>(CAPI.ovr_Room_GetModeratedRooms());
      }

      return null;
    }

    public static void SetUpdateNotificationCallback(Message<Models.Room>.Callback callback)
    {
        Callback.SetNotificationCallback(
          Message.MessageType.Notification_Room_RoomUpdate,
          callback
        );
    }

    public static void SetRoomInviteNotificationCallback(Message<string>.Callback callback)
    {
        Callback.SetNotificationCallback(
          Message.MessageType.Notification_Room_InviteAccepted,
          callback
        );
    }
  }

  public static partial class Matchmaking
  {
    public class CustomQuery
    {
      public Dictionary<string, object> data;
      public Criterion[] criteria;

      public struct Criterion
      {
        public Criterion(string key_, MatchmakingCriterionImportance importance_)
        {
          key = key_;
          importance = importance_;

          parameters = null;
        }

        public string key;
        public MatchmakingCriterionImportance importance;
        public Dictionary<string, object> parameters;
      }

      public IntPtr ToUnmanaged()
      {
        var customQueryUnmanaged = new CAPI.ovrMatchmakingCustomQueryData();

        if(criteria != null && criteria.Length > 0)
        {
          customQueryUnmanaged.criterionArrayCount = (uint)criteria.Length;
          var temp = new CAPI.ovrMatchmakingCriterion[criteria.Length];

          for(int i=0; i<criteria.Length; i++)
          {
            temp[i].importance_ = criteria[i].importance;
            temp[i].key_ = criteria[i].key;

            if(criteria[i].parameters != null && criteria[i].parameters.Count > 0)
            {
              temp[i].parameterArrayCount = (uint)criteria[i].parameters.Count;
              temp[i].parameterArray = CAPI.ArrayOfStructsToIntPtr(CAPI.DictionaryToOVRKeyValuePairs(criteria[i].parameters));
            }
            else
            {
              temp[i].parameterArrayCount = 0;
              temp[i].parameterArray = IntPtr.Zero;
            }
          }

          customQueryUnmanaged.criterionArray = CAPI.ArrayOfStructsToIntPtr(temp);
        }
        else
        {
          customQueryUnmanaged.criterionArrayCount = 0;
          customQueryUnmanaged.criterionArray = IntPtr.Zero;
        }


        if(data != null && data.Count > 0)
        {
          customQueryUnmanaged.dataArrayCount = (uint)data.Count;
          customQueryUnmanaged.dataArray = CAPI.ArrayOfStructsToIntPtr(CAPI.DictionaryToOVRKeyValuePairs(data));
        }
        else
        {
          customQueryUnmanaged.dataArrayCount = 0;
          customQueryUnmanaged.dataArray = IntPtr.Zero;
        }

        IntPtr res = Marshal.AllocHGlobal(Marshal.SizeOf(customQueryUnmanaged));
        Marshal.StructureToPtr(customQueryUnmanaged, res, true);
        return res;
      }
    }

    public static Request<Models.MatchmakingEnqueueResultAndRoom> CreateAndEnqueueRoom(
      string pool,
      uint maxUsers,
      bool subscribeToNotifications = false,
      CustomQuery customQuery = null)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.MatchmakingEnqueueResultAndRoom>(CAPI.ovr_Matchmaking_CreateAndEnqueueRoom(
          pool,
          maxUsers,
          subscribeToNotifications,
          customQuery != null ? customQuery.ToUnmanaged() : IntPtr.Zero
        ));
      }

      return null;
    }

    public static Request<Models.Room> CreateRoom(
      string pool,
      uint maxUsers,
      bool subscribeToNotifications = false)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Matchmaking_CreateRoom(
          pool,
          maxUsers,
          subscribeToNotifications
        ));
      }

      return null;
    }

    public static Request<Models.MatchmakingBrowseResult> Browse(string pool, CustomQuery customQuery = null)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.MatchmakingBrowseResult>(CAPI.ovr_Matchmaking_Browse(
          pool,
          customQuery != null ? customQuery.ToUnmanaged() : IntPtr.Zero
        ));
      }

      return null;
    }

    public static Request ReportResultsInsecure(UInt64 roomID, Dictionary<string, int> data)
    {
      if(Core.IsInitialized())
      {
        CAPI.ovrKeyValuePair[] kvps = new CAPI.ovrKeyValuePair[data.Count];
        int i=0;
        foreach(var item in data)
        {
          kvps[i++] = new CAPI.ovrKeyValuePair(item.Key, item.Value);
        }

        return new Request(CAPI.ovr_Matchmaking_ReportResultInsecure(roomID, kvps, (uint)kvps.Length));
      }

      return null;
    }

    public static Request Enqueue(string pool, CustomQuery customQuery = null)
    {
      if(Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Matchmaking_Enqueue(
          pool,
          customQuery != null ? customQuery.ToUnmanaged() : IntPtr.Zero
        ));
      }

      return null;
    }

    public static Request EnqueueRoom(UInt64 roomID, CustomQuery customQuery = null)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Matchmaking_EnqueueRoom(
          roomID,
          customQuery != null ? customQuery.ToUnmanaged() : IntPtr.Zero
        ));
      }

      return null;
    }

    public static Request StartMatch(UInt64 roomID)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Matchmaking_StartMatch(roomID));
      }

      return null;
    }

    public static Request Cancel(string pool, string traceID)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Matchmaking_Cancel(pool, traceID));
      }

      return null;
    }

    public static Request Cancel()
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Matchmaking_Cancel2());
      }

      return null;
    }

    public static void SetMatchFoundNotificationCallback(Message<Models.Room>.Callback callback)
    {
      Callback.SetNotificationCallback(
        Message.MessageType.Notification_Matchmaking_MatchFound,
        callback
      );
    }

    public static Request<Models.Room> JoinRoom(UInt64 roomID, bool subscribeToNotifications = false)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.Room>(CAPI.ovr_Matchmaking_JoinRoom(roomID, subscribeToNotifications));
      }

      return null;
    }

    public static Request<Models.MatchmakingStats> GetStats(string pool, uint maxLevel, MatchmakingStatApproach approach = MatchmakingStatApproach.Trailing)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.MatchmakingStats>(CAPI.ovr_Matchmaking_GetStats(pool, maxLevel, approach));
      }

      return null;
    }

    public static Request<Models.MatchmakingAdminSnapshot> GetAdminSnapshot()
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.MatchmakingAdminSnapshot>(CAPI.ovr_Matchmaking_GetAdminSnapshot());
      }

      return null;
    }
  }

  public static class Net
  {
    public static Packet ReadPacket()
    {
      if (!Core.IsInitialized())
      {
        return null;
      }

      var packetHandle = CAPI.ovr_Net_ReadPacket();

      if(packetHandle == IntPtr.Zero)
      {
        return null;
      }

      return new Packet(packetHandle);
    }

    public static bool SendPacket(UInt64 userID, byte[] bytes, SendPolicy policy)
    {
      if(Core.IsInitialized())
      {
        return CAPI.ovr_Net_SendPacket(userID, (UIntPtr)bytes.Length, bytes, policy);
      }

      return false;
    }

    public static void Connect(UInt64 userID)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Net_Connect(userID);
      }
    }

    public static void Accept(UInt64 userID)
    {
      if(Core.IsInitialized())
      {
        CAPI.ovr_Net_Accept(userID);
      }
    }

    public static void Close(UInt64 userID)
    {
      if(Core.IsInitialized())
      {
        CAPI.ovr_Net_Close(userID);
      }
    }

    public static bool IsConnected(UInt64 userID)
    {
      return Core.IsInitialized() && CAPI.ovr_Net_IsConnected(userID);
    }

    public static bool SendPacketToCurrentRoom(byte[] bytes, SendPolicy policy)
    {
      if (Core.IsInitialized())
      {
        return CAPI.ovr_Net_SendPacketToCurrentRoom((UIntPtr)bytes.Length, bytes, policy);
      }

      return false;
    }

    public static bool AcceptForCurrentRoom()
    {
      if (Core.IsInitialized())
      {
        return CAPI.ovr_Net_AcceptForCurrentRoom();
      }

      return false;
    }

    public static void CloseForCurrentRoom()
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Net_CloseForCurrentRoom();
      }
    }

    public static Request<Models.PingResult> Ping(UInt64 userID)
    {
      if(Core.IsInitialized())
      {
        return new Request<Models.PingResult>(CAPI.ovr_Net_Ping(userID));
      }

      return null;
    }

    public static void SetPeerConnectRequestCallback(Message<Models.NetworkingPeer>.Callback callback)
    {
      Callback.SetNotificationCallback(
        Message.MessageType.Notification_Networking_PeerConnectRequest,
        callback
      );
    }

    public static void SetConnectionStateChangedCallback(Message<Models.NetworkingPeer>.Callback callback)
    {
      Callback.SetNotificationCallback(
        Message.MessageType.Notification_Networking_ConnectionStateChange,
        callback
      );
    }
  }

  public static class Leaderboards
  {
    // Writes a single entry to a leaderboard
    //
    // "extraData" is a 2KB custom data field that is associated to the leaderboard entry
    // This can be a game replay or anything that give more detail about the entry to the viewer
    //
    // Set "forceUpdate" to true will have the score always update
    // even if it's not the user's personal best score that is being submitted
    public static Request WriteEntry(string leaderboardName, long score, byte[] extraData = null, bool forceUpdate = false)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Leaderboard_WriteEntry(
          leaderboardName,
          score,
          extraData,
          (extraData != null) ? (uint)extraData.Length : 0,
          forceUpdate
        ));
      }

      return null;
    }

    public static Request<Models.LeaderboardEntryList> GetEntries(string leaderboardName, int limit, LeaderboardFilterType filter, LeaderboardStartAt startAt)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.LeaderboardEntryList>(CAPI.ovr_Leaderboard_GetEntries(leaderboardName, limit, filter, startAt));
      }

      return null;
    }

    public static Request<Models.LeaderboardEntryList> GetEntriesAfterRank(string leaderboardName, int limit, ulong afterRank)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.LeaderboardEntryList>(CAPI.ovr_Leaderboard_GetEntriesAfterRank(leaderboardName, limit, afterRank));
      }

      return null;
    }


    public static Request<Models.LeaderboardEntryList> GetNextEntries(Models.LeaderboardEntryList list)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.LeaderboardEntryList>(CAPI.ovr_HTTP_GetWithMessageType(list.NextUrl, (int)Message.MessageType.Leaderboard_GetNextEntries));
      }

      return null;
    }

    public static Request<Models.LeaderboardEntryList> GetPreviousEntries(Models.LeaderboardEntryList list)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.LeaderboardEntryList>(CAPI.ovr_HTTP_GetWithMessageType(list.PreviousUrl, (int)Message.MessageType.Leaderboard_GetPreviousEntries));
      }

      return null;
    }
  }

  public static partial class Notifications
  {
    public static Request MarkAsRead(UInt64 notificationID)
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Notification_MarkAsRead(notificationID));
      }

      return null;
    }

    public static Request GetRoomInviteNotifications()
    {
      if (Core.IsInitialized())
      {
        return new Request(CAPI.ovr_Notification_GetRoomInvites());
      }

      return null;
    }
  }

  public static partial class CloudStorage
  {
    public static Request<Models.CloudStorageUpdateResponse> Delete(string bucket, string key)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageUpdateResponse>(CAPI.ovr_CloudStorage_Delete(bucket, key));
      }

      return null;
    }

    public static Request<Models.CloudStorageData> Load(string bucket, string key)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageData>(CAPI.ovr_CloudStorage_Load(bucket, key));
      }

      return null;
    }

    public static Request<Models.CloudStorageMetadataList> LoadBucketMetadata(string bucket)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageMetadataList>(CAPI.ovr_CloudStorage_LoadBucketMetadata(bucket));
      }

      return null;
    }

    public static Request<Models.CloudStorageConflictMetadata> LoadConflictMetadata(string bucket, string key)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageConflictMetadata>(CAPI.ovr_CloudStorage_LoadConflictMetadata(bucket, key));
      }

      return null;
    }

    public static Request<Models.CloudStorageData> LoadHandle(string handle)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageData>(CAPI.ovr_CloudStorage_LoadHandle(handle));
      }

      return null;
    }

    public static Request<Models.CloudStorageMetadata> LoadMetadata(string bucket, string key)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageMetadata>(CAPI.ovr_CloudStorage_LoadMetadata(bucket, key));
      }

      return null;
    }

    public static Request<Models.CloudStorageUpdateResponse> ResolveKeepLocal(string bucket, string key, string remoteHandle)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageUpdateResponse>(CAPI.ovr_CloudStorage_ResolveKeepLocal(bucket, key, remoteHandle));
      }

      return null;
    }

    public static Request<Models.CloudStorageUpdateResponse> ResolveKeepRemote(string bucket, string key, string remoteHandle)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageUpdateResponse>(CAPI.ovr_CloudStorage_ResolveKeepRemote(bucket, key, remoteHandle));
      }

      return null;
    }

    public static Request<Models.CloudStorageUpdateResponse> Save(string bucket, string key, byte[] data, long counter, string extraData)
    {
      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageUpdateResponse>(CAPI.ovr_CloudStorage_Save(bucket, key, data, (uint)data.Length, counter, extraData));
      }

      return null;
    }
  }

  public static class Voip
  {
    public static void Start(UInt64 userID)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Voip_Start(userID);
      }
    }

    public static void Accept(UInt64 userID)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Voip_Accept(userID);
      }
    }

    public static void Stop(UInt64 userID)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Voip_Stop(userID);
      }
    }

    public static void SetVoipConnectRequestCallback(Message<Models.NetworkingPeer>.Callback callback)
    {
      if (Core.IsInitialized())
      {
        Callback.SetNotificationCallback(
          Message.MessageType.Notification_Voip_ConnectRequest,
          callback
        );
      }
    }

    public static void SetVoipStateChangeCallback(Message<Models.NetworkingPeer>.Callback callback)
    {
      if (Core.IsInitialized())
      {
        Callback.SetNotificationCallback(
          Message.MessageType.Notification_Voip_StateChange,
          callback
        );
      }
    }

    public static void SetMicrophoneFilterCallback(CAPI.FilterCallback callback)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Voip_SetMicrophoneFilterCallbackWithFixedSizeBuffer(callback, (UIntPtr)CAPI.VoipFilterBufferSize);
      }
    }

    public static void SetMicrophoneMuted(VoipMuteState state)
    {
      if (Core.IsInitialized())
      {
        CAPI.ovr_Voip_SetMicrophoneMuted(state);
      }
    }
  }

  public static partial class Achievements {
    public static Request<Models.AchievementDefinitionList> GetNextAchievementDefinitionListPage(Models.AchievementDefinitionList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextAchievementDefinitionListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.AchievementDefinitionList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.Achievements_GetNextAchievementDefinitionArrayPage
          )
        );
      }

      return null;
    }

    public static Request<Models.AchievementProgressList> GetNextAchievementProgressListPage(Models.AchievementProgressList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextAchievementProgressListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.AchievementProgressList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.Achievements_GetNextAchievementProgressArrayPage
          )
        );
      }

      return null;
    }

  }

  public static partial class CloudStorage {
    public static Request<Models.CloudStorageMetadataList> GetNextCloudStorageMetadataListPage(Models.CloudStorageMetadataList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextCloudStorageMetadataListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.CloudStorageMetadataList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.CloudStorage_GetNextCloudStorageMetadataArrayPage
          )
        );
      }

      return null;
    }

  }

  public static partial class IAP {
    public static Request<Models.ProductList> GetNextProductListPage(Models.ProductList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextProductListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.ProductList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.IAP_GetNextProductArrayPage
          )
        );
      }

      return null;
    }

    public static Request<Models.PurchaseList> GetNextPurchaseListPage(Models.PurchaseList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextPurchaseListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.PurchaseList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.IAP_GetNextPurchaseArrayPage
          )
        );
      }

      return null;
    }

  }

  public static partial class Notifications {
    public static Request<Models.RoomInviteNotificationList> GetNextRoomInviteNotificationListPage(Models.RoomInviteNotificationList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextRoomInviteNotificationListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.RoomInviteNotificationList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.Notification_GetNextRoomInviteNotificationArrayPage
          )
        );
      }

      return null;
    }

  }

  public static partial class Rooms {
    public static Request<Models.RoomList> GetNextRoomListPage(Models.RoomList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextRoomListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.RoomList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.Room_GetNextRoomArrayPage
          )
        );
      }

      return null;
    }

  }

  public static partial class Users {
    public static Request<Models.UserList> GetNextUserListPage(Models.UserList list) {
      if (!list.HasNextPage)
      {
        Debug.LogWarning("Oculus.Platform.GetNextUserListPage: List has no next page");
        return null;
      }

      if (Core.IsInitialized())
      {
        return new Request<Models.UserList>(
          CAPI.ovr_HTTP_GetWithMessageType(
            list.NextUrl,
            (int)Message.MessageType.User_GetNextUserArrayPage
          )
        );
      }

      return null;
    }

  }


}
