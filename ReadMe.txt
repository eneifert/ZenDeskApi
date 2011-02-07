This is a c# wrapper for the ZenDesk Api: http://www.zendesk.com/api/rest-introduction.

95% of the wrapper methods are there (listed below), enjoy.



    bool AddEmailAddressToAUser(int userId, string email);
    bool AddTwitterHandleToUser(int userId, string twitterHandle);
    bool ApplyMacro(int macroId, int ticketId);
       
    int CreateEntry(int forumId, int submitterId, string title, string body, string currentTags, bool isLocked = false, bool isPinned = true);
    int CreateEntry(Entry entry);        
    int CreateOrUpdateForum(string name, string description, bool isLocked);
    int CreateOrUpdateForum(Forum forum);
    int CreateOrUpdateGroup(string name, List<int> userIds);
    int CreateOrUpdateGroup(Group group);       
    int CreateOrUpdateOrganization(string name, string defaultSite);
    int CreateOrUpdateOrganization(Organization newOrg);
    int CreatePost(int entryId, int userId, string body);
    int CreatePost(Post post);
    int CreateOrUpdateUser(string email, string name, string password, Role role, RestrictedTo restrictionId, int[] groupIDs, bool isVerified = true);
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