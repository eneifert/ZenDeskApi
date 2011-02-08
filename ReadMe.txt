This is a full c# wrapper for the ZenDesk Api at: http://www.zendesk.com/api/rest-introduction.

*Note: Create and Update methods might have some unexpected behavior. It was never clear what fields could and could not be updated. So if you get an error it is probably because you are sending them to much data. I have already began a discussion about this issue which can be seen here: https://support.zendesk.com/requests/76352

Don't be afraid to fix any bugs you see and send me pull requests!

Here is a list of the public methods, enjoy.


    bool AddEmailAddressToAUser(int userId, string email);
    bool AddTwitterHandleToUser(int userId, string twitterHandle);
    bool ApplyMacro(int macroId, int ticketId)
    bool AddComment(int ticketId, Comment comment)

       
    int CreateEntry(int forumId, int submitterId, string title, 
		string body, string currentTags, bool isLocked = false, bool isPinned = true);
    int CreateEntry(Entry entry);        
    int CreateOrUpdateForum(string name, string description, bool isLocked);
    int CreateOrUpdateForum(Forum forum);
    int CreateOrUpdateGroup(string name, List<int> userIds);
    int CreateOrUpdateGroup(Group group);       
    int CreateOrUpdateOrganization(string name, string defaultSite);
    int CreateOrUpdateOrganization(Organization newOrg);
    int CreatePost(int entryId, int userId, string body);
    int CreatePost(Post post);
    int CreateOrUpdateUser(string email, string name, string password, 
		Role role, RestrictedTo restrictionId, int[] groupIDs, bool isVerified = true);
    int CreateOrUpdateUser(User newUser);
    int CreateTicketAsEndUser(string endUserEmail, string subject, string description);
    int CreateTicketAsEndUser(string endUserEmail, Ticket ticket);      

    bool DestroyOrganization(int orgId);
    bool DestroyForum(int forumId);
    bool DestroyEntry(int entryId);
    bool DestroyPost(int postId);
    bool DestroyGroup(int groupId);
    bool DestroyUser(int userId);
    bool DestroyTicket(int ticketId);
    bool DeleteUserIdentity(int userId, int identityId);
             
    T Execute<T>(ZenRestRequest request) where T : new();
    RestResponse Execute(ZenRestRequest request);

    List<User> FindUsersByEmail(string email);
      
    List<T> GetCollection<T>(string resource, string rootElement = "");
    List<View> GetViews();
    View GetViewByName(string name);
    List<Organization> GetOgranizations();
    Organization GetOgranizationById(int id);
    int GetExistingForumId(Forum forum);
    List<Forum> GetForums();
    Forum GetForumById(int id);
    List<TicketField> GetTicketFields();
    List<Macro> GetMacros();
    List<Entry> GetEntriesByForumId(int forumId);
    Entry GetEntryById(int id);
    Post GetPostByIds(int entityId, int postId);
    List<Group> GetGroups();
    Group GetGroupById(int id);
    int[] GetAllGroupIDs();
    List<User> GetUsers();
    User GetUserById(int id);
    List<UserIdentity> GetUserIdentities(int userId);
    Ticket GetTicketById(int id);
    List<Ticket> GetAllTicketsForUser(string email);
    List<Ticket> GetTicketsForUserByPage(string email, int page = 1);
    List<Ticket> GetTicketsInViewByPage(int viewId, int page = 1);
    List<Ticket> GetAllTicketsInView(int viewId);
    List<TagScore> GetAllTags();
    List<Ticket> GetAllTicketsForTag(string tagName);
    List<Ticket> GetTicktesForTagByPage(string tagName, int page = 1);

    bool MakeUserIdentityPrimary(int userId, int identityId);
       
    List<User> QueryUsers(string query);
      
    bool UpdateOrganization(Organization org);
    bool UpdateForum(Forum forum);
    bool UpdateEntry(Entry entry);
    bool UpdatePost(Post post);
    bool UpdateGroup(Group group);
    bool UpdateUser(User user);
    bool UpdateTicket(int ticketId, string description);
    bool UpdateTicket(int ticketId, Comment comment);