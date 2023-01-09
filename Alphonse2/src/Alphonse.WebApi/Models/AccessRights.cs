namespace Alphonse.WebApi.Models
{
    [Flags]
    public enum AccessRights : long
    {
        None = 0,

        // simple way : CRUD => 0b_durc

        // User - self rights
        UserSelfCreate = 0b_0001 << 0,
        UserSelfRead = 0b_0010 << 0,
        UserSelfUpdate = 0b_0100 << 0,
        UserSelfDelete = 0b_1000 << 0,
        UserSelfFull = UserSelfCreate | UserSelfRead | UserSelfUpdate | UserSelfDelete,

        // User - rights over others user
        UserOtherCreate = 0b_0001 << 4,
        UserOtherRead = 0b_0010 << 4,
        UserOtherUpdate = 0b_0100 << 4,
        UserOtherDelete = 0b_1000 << 4,
        UserOtherFull = UserOtherCreate | UserOtherRead | UserOtherUpdate | UserOtherDelete,

        // Call history rights
        CallHistoryCreate = 0b_0001 << 8,
        CallHistoryRead = 0b_0010 << 8,
        CallHistoryUpdate = 0b_0100 << 8,
        CallHistoryDelete = 0b_1000 << 8,
        CallHistoryFull = CallHistoryCreate | CallHistoryRead | CallHistoryUpdate | CallHistoryDelete,

        // Phonebook rights
        PhonebookCreate = 0b_0001 << 12,
        PhonebookRead = 0b_0010 << 12,
        PhonebookUpdate = 0b_0100 << 12,
        PhonebookDelete = 0b_1000 << 12,
        PhonebookFull = PhonebookCreate | PhonebookRead | PhonebookUpdate | PhonebookDelete,

        // some default user tights
        Admin = UserSelfFull | UserOtherFull | CallHistoryFull | PhonebookFull,
        Default = (UserSelfFull ^ UserSelfCreate) | CallHistoryRead | PhonebookFull,
    }
}