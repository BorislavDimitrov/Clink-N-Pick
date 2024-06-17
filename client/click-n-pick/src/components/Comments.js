import { useState, useEffect } from "react";
import CommentForm from "./CommentForm";
import Comment from "./Comment";
import { createComment } from "../fetch/requests/comments";

const Comments = ({ currentUserId, productId }) => {
  const [comments, setComments] = useState([]);
  const [activeComment, setActiveComment] = useState(null);
  const rootComments = comments.filter((comment) => comment.parentId === null);

  async function addComment(text, parentId) {
    console.log(text);
    var response = await createComment({
      Content: text,
      ParentId: parentId,
      ProductId: productId,
    });

    var data = await response.data;
    console.log(data);
    setComments([data, ...comments]);
    setActiveComment(null);
  }

  return (
    <div className="comments">
      <h3>Comments</h3>
      <div>Write comment</div>
      <CommentForm submitLabel="Write" handleSubmit={addComment} />
      <div className="comments-container">
        {rootComments.map((rootComment) => (
          <Comment
            key={rootComment.id}
            comment={rootComment}
            //replies={getReplies(rootComment.id)}
            activeComment={activeComment}
            setActiveComment={setActiveComment}
            addComment={addComment}
            currentUserId={currentUserId}
          />
        ))}
      </div>
    </div>
  );
};

export default Comments;
