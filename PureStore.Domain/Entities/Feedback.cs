using PureStore.Domain.Common;

namespace PureStore.Domain.Entities;

public class Feedback : BaseEntity
{
    public required string Content { get; set; }
    public required string Username { get; set; }
    public required double Rating { get; set; }
    public required string ApplicationId { get; set; }

    public string Initials
    {
        get
        {
            if (string.IsNullOrEmpty(Username))
            {
                return string.Empty;
            }

            return Username.Remove(2, Username.Length - 2).ToUpper();
        }
    }
}
