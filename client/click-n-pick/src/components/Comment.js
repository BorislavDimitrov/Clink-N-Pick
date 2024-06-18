const Comment = ({
  comment,
  replies,
  addComment,
  parentId = null,
  currentUserId,
}) => {
  const createdAt = new Date(comment.createdOn).toLocaleDateString();
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
        <span class="text-xs text-gray-400">{createdAt}</span>
        <p class="text-sm">{comment.content}</p>
        <div className="space-y-4">
          {replies.length > 0 && (
            <div className="replies">
              {replies.map((reply) => (
                <Comment
                  comment={reply}
                  key={reply.id}
                  addComment={addComment}
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

    // <div key={comment.id} className="comment">
    //   <div className="comment-image-container">
    //     <img src={comment.creatorImageUrl} alt="user profile" />
    //   </div>
    //   <div className="comment-right-part">
    //     <div className="comment-content">
    //       <div className="comment-author">{comment.creatorUsername}</div>
    //       <div>{createdAt}</div>
    //     </div>
    //     {comment.content}
    //     <div className="comment-actions"></div>
    //     {replies.length > 0 && (
    //       <div className="replies">
    //         {replies.map((reply) => (
    //           <Comment
    //             comment={reply}
    //             key={reply.id}
    //             addComment={addComment}
    //             parentId={comment.id}
    //             replies={[]}
    //             currentUserId={currentUserId}
    //           />
    //         ))}
    //       </div>
    //     )}
    //   </div>
    // </div>
  );
};

export default Comment;
