namespace SupportDesk.Domain.Tickets;

public class Comment
{
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Comment(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Comment cannot be empty");

        Text = text;
        CreatedAt = DateTime.Now;
    }

    public static Comment Rehydrate(
    string text,
    DateTime createdAt)
    {
        var comment = new Comment(text);

        comment.CreatedAt = createdAt;

        return comment;
    }
}
