const Comment = ({
  comment,
  replies,
  addComment,
  deleteComment,
  parentId = null,
  currentUserId,
}) => {
  const fiveMinutes = 300000;
  const timePassed = new Date() - new Date(comment.createdOn) > fiveMinutes;

  const canReply = Boolean(currentUserId);
  const canEdit = currentUserId === comment.creatorId && !timePassed;
  const canDelete =
    currentUserId === comment.creatorId && replies.length === 0 && !timePassed;
  const createdOn = new Date(comment.createdOn).toLocaleDateString();
  return (
    <div key={comment.id} class="flex">
      <div class="flex-shrink-0 mr-3">
        <img
          class="mt-2 rounded-full w-8 h-8 sm:w-10 sm:h-10"
          src={comment.creatorImageUrl}
          alt="user profile"
        />
      </div>
      <div class="flex-1 border rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
        <strong>
          <a href={`/Users/Profile/${comment.creatorId}`}>
            {comment.creatorUsername}
          </a>
        </strong>{" "}
        <span class="text-xs text-gray-400">{createdOn}</span>
        <p class="text-lg">{comment.content}</p>
        <div className="flex flex-row gap-2 mt-4">
          {canReply && <button class="text-sm">Reply</button>}
          {canEdit && <button class="text-sm">Edit</button>}
          {canDelete && (
            <button onClick={() => deleteComment(comment.id)} class="text-sm">
              Delete
            </button>
          )}
        </div>
        <div className="space-y-4">
          {replies.length > 0 && (
            <div className="replies">
              {replies.map((reply) => (
                <Comment
                  comment={reply}
                  key={reply.id}
                  addComment={addComment}
                  deleteComment={deleteComment}
                  parentId={comment.id}
                  replies={[]}
                  currentUserId={currentUserId}
                />
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Comment;
