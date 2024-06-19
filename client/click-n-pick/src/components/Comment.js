import CommentForm from "./CommentForm";

const Comment = ({
  comments,
  comment,
  replies,
  addComment,
  removeComment,
  updateComment,
  parentId = null,
  currentUserId,
  activeComment,
  setActiveComment,
}) => {
  const isEditing =
    activeComment &&
    activeComment.id === comment.id &&
    activeComment.type === "editing";

  const getReplies = (commentId) =>
    comments
      .filter((backendComment) => backendComment.parentId === commentId)
      .sort(
        (a, b) =>
          new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      );

  const fiveMinutes = 300000;
  const timePassed = new Date() - new Date(comment.createdOn) > fiveMinutes;

  const canReply = Boolean(currentUserId);
  const canEdit = currentUserId === comment.creatorId && !timePassed;
  const canDelete =
    currentUserId === comment.creatorId && replies.length === 0 && !timePassed;

  const createdOn = new Date(comment.createdOn).toLocaleDateString();

  const isReplying =
    activeComment &&
    activeComment.id === comment.id &&
    activeComment.type === "replying";
  const replyId = parentId ? parentId : comment.id;

  return (
    <div key={comment.id} className="flex">
      <div className="flex-shrink-0 mr-3">
        <img
          className="mt-2 rounded-full w-8 h-8 sm:w-10 sm:h-10"
          src={comment.creatorImageUrl}
          alt="user profile"
        />
      </div>
      <div className="flex-1 border rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
        <strong>
          <a href={`/Users/Profile/${comment.creatorId}`}>
            {comment.creatorUsername}
          </a>
        </strong>{" "}
        <span className="text-xs text-gray-400">{createdOn}</span>
        {!isEditing && <p className="text-lg">{comment.content}</p>}
        <div className="flex flex-row gap-2 mt-4">
          {isEditing && (
            <CommentForm
              submitLabel="Update"
              hasCancelButton
              initialText={comment.content}
              handleSubmit={(text) => updateComment(text, comment.id)}
              handleCancel={() => {
                setActiveComment(null);
              }}
            />
          )}
          {canReply && (
            <button
              onClick={() =>
                setActiveComment({ id: comment.id, type: "replying" })
              }
              className="text-sm"
            >
              Reply
            </button>
          )}
          {canEdit && (
            <button
              onClick={() =>
                setActiveComment({ id: comment.id, type: "editing" })
              }
              className="text-sm"
            >
              Edit
            </button>
          )}
          {canDelete && (
            <button
              onClick={() => removeComment(comment.id)}
              className="text-sm"
            >
              Delete
            </button>
          )}
        </div>
        {isReplying && (
          <CommentForm
            submitLabel="Reply"
            handleSubmit={(text) => addComment(text, replyId)}
          />
        )}
        <div className="space-y-4">
          {replies.length > 0 && (
            <div className="replies">
              {replies.map((reply) => (
                <Comment
                  comments={comments}
                  comment={reply}
                  key={reply.id}
                  setActiveComment={setActiveComment}
                  activeComment={activeComment}
                  updateComment={updateComment}
                  removeComment={removeComment}
                  addComment={addComment}
                  parentId={reply.id}
                  replies={getReplies(reply.id)}
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
